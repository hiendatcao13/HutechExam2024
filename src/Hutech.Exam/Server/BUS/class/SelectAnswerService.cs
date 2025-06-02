using Hutech.Exam.Shared.DTO.Request;
using MessagePack;

namespace Hutech.Exam.Server.BUS
{
    public class SelectAnswerService(RedisService redisService, ILogger<SelectAnswerService> logger)
    {
        private readonly RedisService _redisService = redisService;
        private readonly ILogger<SelectAnswerService> _logger = logger;

        public async Task SetChiTietBaiLamAsync(byte[] message)
        {
            try
            {
                if (message == null || message.Length == 0)
                {
                    _logger.LogError("[Redis] Received empty or null message.");
                    throw new ArgumentException("Message cannot be null or empty.");
                }

                // Deserialize vào đối tượng chiTietBaiThi
                var chiTietBaiThi = MessagePackSerializer.Deserialize<ChiTietBaiThiRequest>(message);

                if (chiTietBaiThi == null)
                {
                    _logger.LogError("[Redis] Error deserializing message: {Message}", message);
                    throw new Exception("Error deserializing message.");
                }

                await _redisService.SetChiTietBaiThi(chiTietBaiThi.MaCauHoi, chiTietBaiThi, chiTietBaiThi.MaChiTietCaThi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while processing the message.");
                throw; // ném ngoại lệ để rabbitMQ bắt và đẩy thông điệp vào hàng đợi
            }
        }
    }
}
