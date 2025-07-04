using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.BUS.RabbitServices;
using Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi;
using Hutech.Exam.Shared.DTO.Request.Custom;
using MessagePack;
using Microsoft.AspNetCore.SignalR;


namespace Hutech.Exam.Server.Hubs
{
    public class SinhVienHub(RedisService redisService, ExamRecoveryService examRecoveryService, AnswerQueueService answerQueueService, SubmitQueueService submitQueueService) : Hub
    {
        private readonly RedisService _redisService = redisService;
        private readonly ExamRecoveryService _examRecoveryService = examRecoveryService;
        private readonly RabbitMQService _answerQueueService = answerQueueService;
        private readonly RabbitMQService _submitQueueService = submitQueueService;


        // cho thí sinh tham gia và rời tham gia nhóm theo mã ca thi - đóng băng ca thi
        public async Task JoinGroupMaCaThi(int ma_ca_thi)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, ma_ca_thi + "");
        }
        public async Task LeaveGroupMaCaThi(int ma_ca_thi)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ma_ca_thi + "");
        }

        // lưu các connectionId vào Redis
        public async Task SetConnectionId(long ma_sinh_vien)
        {
            await _redisService.SetConnectionIdAsync(ma_sinh_vien, Context.ConnectionId);
        }

        // gửi thông điệp cập nhật chi tiết bài thi cho RabbitMQ
        public async Task SelectDapAn(ChiTietBaiThiRequest chiTietBaiThi)
        {
            byte[] message = MessagePackSerializer.Serialize(chiTietBaiThi);
            await _answerQueueService.PublishMessageAsync(message);
        }

        // gửi thông điệp yêu cầu chi tiết bài thi từ Redis
        public async Task<Dictionary<int, ChiTietBaiThiRequest>> RequestTiepTucThi(int ma_chi_tiet_ca_thi)
        {
            return await _examRecoveryService.GetDapAnKhoanhAsync(ma_chi_tiet_ca_thi);
        }

        // gửi thông điệp yêu cầu nộp bài, hãy cho biết bạn là ai (MSV), bạn thi ca nào (chi_tiet_ca_thi), đề thi nào (de_thi_hoan_vi)
        public async Task RequestSubmit(SubmitRequest request)
        {
            byte[] message = MessagePackSerializer.Serialize(request);
          
            await _submitQueueService.PublishMessageAsync(message);
        }
        public async Task DeliverDapAn(string connectionId, List<bool> dapAns, double diem)
        {
            await Clients.Client(connectionId).SendAsync("DeliverDapAn", dapAns, diem);
        }

        public async Task RecoverySubmit(SubmitRequest request)
        {
            byte[] message = MessagePackSerializer.Serialize(request);

            await _submitQueueService.PublishMessageAsync(message);
        }
        

        ////////////////CÁC YÊU CẦU KHÁC TỪ SERVER XUỐNG - nó bị phân tán nên không control được//////////////
    }
}
