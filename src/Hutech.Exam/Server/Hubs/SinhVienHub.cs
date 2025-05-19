using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.BUS.RabbitServices;
using Microsoft.AspNetCore.SignalR;


namespace Hutech.Exam.Server.Hubs
{
    public class SinhVienHub(RedisService redisService, AnswerQueueService answerQueueService, SubmitQueueService submitQueueService) : Hub
    {
        private readonly RedisService _redisService = redisService;
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
        public async Task SelectDapAn(byte[] chiTietBaiThi)
        {
            await _answerQueueService.PublishMessageAsync(chiTietBaiThi);
        }

        // gửi thông điệp yêu cầu chi tiết bài thi từ Redis
        public async Task<Dictionary<int, int>> RequestTiepTucThi(int ma_chi_tiet_ca_thi)
        {
            return await _redisService.GetDapAnKhoanhAsync(ma_chi_tiet_ca_thi);
        }
        // gửi thông điệp yêu cầu nộp bài
        public async Task<List<bool>> RequestSubmit(int ma_chi_tiet_ca_thi, long ma_de_thi_hoan_vi)
        {
            //return await _redisService.GetDungSaiAsync(ma_chi_tiet_ca_thi, ma_de_thi_hoan_vi);
        }
    }
}
