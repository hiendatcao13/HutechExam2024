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
    public class SubmitService(ChiTietBaiThiService chiTietBaiThiService, ChiTietCaThiService chiTietCaThiService, RedisService redisService, IHubContext<SinhVienHub> sinhVienHub, IHubContext<AdminHub> adminHub, ILogger<SubmitService> logger, IMapper mapper)
    {
        #region Private Fields
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

                Dictionary<int, int> dapAns = await _redisService.GetDapAnAsync(submitRequest.MaDeThiHoanVi);

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
                Dictionary<int, ChiTietBaiThiRequest> data = await _redisService.GetChiTietBaiThiAsync(submitRequest.MaChiTietCaThi);

                // xử lí cho recovery xảy ra
                if (submitRequest.IsRecoverySubmit)
                {
                    await HandleRecoverySubmit(submitRequest, dapAns);
                    await _redisService.RemoveSubmitAsync(submitRequest.MaChiTietCaThi);
                    return;
                }

                int length = submitRequest.DapAnKhoanhs.Count(p => p.Value != null);
                // Kiểm tra xem số lượng bài trong Redis nhận đủ hay chưa, nếu chưa nhận đủ thì throw để vào hàng đợi lại (cơ chế FIFO, nó đứng cuối hàng đợi lại)
                // Trường hợp may thì được xử lí ngay tại chỗ
                if (data.Count != length)
                {
                    int so_lan_fail = await _redisService.GetFailSubmitAsync(submitRequest.MaChiTietCaThi);
                    if (so_lan_fail == 3)
                    {
                        await NotifySVFailedCacheRedis(submitRequest.MaSinhVien);

                        // xóa dữ liệu khi đã báo cho client
                        await _redisService.RemoveSubmitAsync(submitRequest.MaChiTietCaThi);
                        return;
                    }

                    await _redisService.SetFailSubmitAsync(submitRequest.MaChiTietCaThi, so_lan_fail + 1); // tăng số lần bị fail
                    _logger.LogWarning("[CONFLICT] Data count mismatch: Redis {dataCount} vs Message {dapAnsCount} of maSV {maSV} failed {fail}", data.Count, length, submitRequest.MaSinhVien, so_lan_fail);
                    throw new Exception();
                }

                //cập nhật đúng sai cho từng câu
                _chiTietBaiThiService.UpdateDungSai_SelectByListCTBT_DapAn(data, dapAns);

                // tiến hành lưu vào database TVP chiTietBaiThi
                await _chiTietBaiThiService.Insert_Batch(_mapper.Map<List<ChiTietBaiThiDto>>(data.Values.ToList()));

                // xóa dữ liệu khi đã lưu xong thành công
                await _redisService.RemoveSubmitAsync(submitRequest.MaChiTietCaThi);

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
        // hàm sử dụng khi thí sinh bị mất bài nộp hoặc thiếu bài nộp
        private async Task HandleRecoverySubmit(SubmitRequest request, Dictionary<int, int> dapAns)
        {
            if (request.DsDapAnDuPhong != null && request.DsDapAnDuPhong.Count != 0)
            {
                //cập nhật đúng sai cho từng câu
                _chiTietBaiThiService.UpdateDungSai_SelectByListCTBT_DapAn(request.DsDapAnDuPhong, dapAns);

                // tiến hành lưu vào database TVP chiTietBaiThi
                await _chiTietBaiThiService.Insert_Batch(_mapper.Map<List<ChiTietBaiThiDto>>(request.DsDapAnDuPhong.Values.ToList()));

                // xóa dữ liệu khi đã lưu xong thành công
                await _redisService.RemoveChiTietBaiThiAsync(request.MaChiTietCaThi);
            }
        }

        // hàm hỗ trợ cho việc redis cache dữ liệu bị thiếu sót hoặc có vấn đề xảy ra khi vượt quá số lần bị fail
        private async Task NotifySVFailedCacheRedis(long ma_sinh_vien)
        {
            var connectionId = await _redisService.GetConnectionIdAsync(ma_sinh_vien);
            await _sinhVienHub.Clients.Client(connectionId).SendAsync("RequestRecoverySubmit");
        }


        private async Task NotifSVStatusThiToAdmin(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian)
        {
            // 0: bắt đầu thi, 1: kết thúc thi
            await _adminHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian);
        }
        #endregion

    }
}
