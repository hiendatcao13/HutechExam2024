using AutoMapper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.DTO.Request.Custom;
using Hutech.Exam.Shared.DTO;
using MessagePack;
using Microsoft.AspNetCore.SignalR;
using System.Data.SqlClient;
using Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi;

namespace Hutech.Exam.Server.BUS
{
    public class SubmitService(DeThiService deThiService, ChiTietBaiThiService chiTietBaiThiService, ChiTietCaThiService chiTietCaThiService, RedisService redisService, IHubContext<SinhVienHub> sinhVienHub, IHubContext<AdminHub> adminHub, ILogger<SubmitService> logger, IMapper mapper)
    {
        #region Private Fields
        private readonly DeThiService _deThiService = deThiService;
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;
        private readonly ChiTietCaThiService _chiTietCaThiService = chiTietCaThiService;
        private readonly IHubContext<SinhVienHub> _sinhVienHub = sinhVienHub;
        private readonly IHubContext<AdminHub> _adminHub = adminHub;
        private readonly RedisService _redisService = redisService;
        private readonly IMapper _mapper = mapper;

        private readonly ILogger _logger = logger;
        #endregion

        #region Public Methods
        public async Task HandleSubmit(byte[] message)
        {
            SubmitRequest submitRequest = default!;
            try
            {
                submitRequest = MessagePackSerializer.Deserialize<SubmitRequest>(message);

                var deThi = await _deThiService.SelectOne(submitRequest.MaDeThi);
                Dictionary<Guid, Guid> dapAns = await _redisService.GetDapAnAsync(deThi.Guid);

                if (submitRequest.IsLanDau) // chỉ thực hiện cho lần đầu
                {
                    // xử lí chỉ trả ds đúng sai và gửi đáp án cho sinh viên về ngay
                    (List<bool?> listDungSai, int so_cau_dung, double diem) = _chiTietBaiThiService.GetDungSai_SelectBy_DapAnKhoanh(submitRequest.DapAnKhoanhs, dapAns);

                    _logger.LogInformation("[SUCCESS] SVMa: {ma_sinh_vien} has score {diem}, total right answers {so_cau_dung} on {tong_so_cau}", submitRequest.MaSinhVien, diem, so_cau_dung, dapAns.Count);

                    // gửi thông điệp cho thí sinh
                    var connectionId = await _redisService.GetConnectionIdAsync(submitRequest.MaSinhVien);

                    if (!string.IsNullOrEmpty(connectionId))
                    {
                        await _sinhVienHub.Clients.Client(connectionId).SendAsync("DeliverDapAn", listDungSai, so_cau_dung, diem);
                    }
                    else
                    {
                        _logger.LogWarning("[Redis] No connectionId found for ma_sinh_vien: {ma_sinh_vien}", submitRequest.MaSinhVien);
                    }

                    // tiến hành lưu đáp án vào database
                    var chiTietCaThiUpdateKTThiRequest = new ChiTietCaThiUpdateKTThiRequest { ThoiGianKetThuc = submitRequest.ThoiGianNopBai, Diem = diem, SoCauDung = so_cau_dung, TongSoCau = listDungSai.Count };
                    await _chiTietCaThiService.UpdateKetThuc(submitRequest.MaChiTietCaThi, chiTietCaThiUpdateKTThiRequest);

                    // thông báo cho người giám sát thí sinh đã nộp bài
                    await NotifSVStatusThiToAdmin(submitRequest.MaChiTietCaThi, false, submitRequest.ThoiGianNopBai);

                    submitRequest.IsLanDau = false;
                }

                //////////////////////////////////Xử lí cho background nền lưu bài\\\\\\\\\\\\\\\\\\\\\\\

                // lấy toàn bộ bài thi của sinh viên từ Redis
                Dictionary<Guid, ChiTietBaiThiRequest> data = await _redisService.GetChiTietBaiThiAsync(submitRequest.MaChiTietCaThi);

                int length = submitRequest.DapAnKhoanhs.Count(p => p.Value != null);
                // Kiểm tra xem số lượng bài trong Redis nhận đủ hay chưa, nếu chưa nhận đủ thì throw để vào hàng đợi lại (cơ chế FIFO, nó đứng cuối hàng đợi lại)
                // Trường hợp may thì được xử lí ngay tại chỗ
                if (data.Count != length)
                {
                    _logger.LogWarning("[CONFLICT] Dữ liệu không trùng nhau: Redis {dataCount} vs thông tin nhận từ RabbitMQ {dapAnsCount} của maSV {maSV} failed", data.Count, length, submitRequest.MaSinhVien);
                    throw new Exception();
                }

                //cập nhật đúng sai cho từng câu
                _chiTietBaiThiService.UpdateDungSai_SelectByListCTBT_DapAn(data, dapAns);

                // tiến hành lưu vào database TVP chiTietBaiThi
                await _chiTietBaiThiService.Insert_Batch(_mapper.Map<List<ChiTietBaiThiDto>>(data.Values.ToList()));

                // xóa dữ liệu khi đã lưu xong thành công
                await _redisService.RemoveChiTietBaiThiAsync(submitRequest.MaChiTietCaThi);

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "[SQL] Error while saving ChiTietBaiThi for MaChiTietCaThi: {MaChiTietCaThi}", submitRequest.MaChiTietCaThi);
                throw; // ném ra để rabbitMQ đẩy lại vào hàng đợi, xử lí lại
            }
            catch (Exception ex) // có thể catch SqlException ở đây
            {
                _logger.LogError(ex, "[ERROR] Error while saving ChiTietBaiThi for MaChiTietCaThi: {MaChiTietCaThi}", submitRequest.MaChiTietCaThi);
                throw; // ném ra để rabbitMQ đẩy lại vào hàng đợi, xử lí lại
            }
        }
        #endregion

        #region Private Methods


        private async Task NotifSVStatusThiToAdmin(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian)
        {
            // 0: bắt đầu thi, 1: kết thúc thi
            await _adminHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian);
        }
        #endregion

    }
}
