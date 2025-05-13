using Hutech.Exam.Server.Controllers;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
namespace Hutech.Exam.Server.BUS
{
    public class RabbitMqCTBTService(ChiTietBaiThiService chiTietBaiThiService)
    {
        private readonly string _hostname = "localhost"; // Thay bằng hostname của RabbitMQ nếu dùng từ xa
        private readonly string _queueName = "Exam.Hutech.ChiTietBaiThi";
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;

        public async Task PublishMessage(List<ChiTietBaiThi> messages)
        {
            // Tạo kết nối đến RabbitMQ
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // Đảm bảo hàng đợi tồn tại
            await channel.QueueDeclareAsync(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Chuyển đổi message thành JSON
            var messageJson = JsonSerializer.Serialize(messages);
            var body = Encoding.UTF8.GetBytes(messageJson);

            // Gửi message lên hàng đợi
            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: _queueName,
                                 body: body);

            //Console.WriteLine($"[x] Sent: {message}");
        }

        public async Task ConsumeMessages()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                // Tạo kết nối đến RabbitMQ
                var factory = new ConnectionFactory() { HostName = _hostname };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                // Đảm bảo hàng đợi tồn tại
                await channel.QueueDeclareAsync(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //await channel.BasicQosAsync(0, 250, false);

                // Lắng nghe hàng đợi
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);
                    // Chuyển đổi JSON thành đối tượng kiểu T
                    var messages = JsonSerializer.Deserialize<List<ChiTietBaiThiDto>>(messageJson);
                    if (messages != null)
                    {
                        // Gọi hàm xử lý message
                        ProcessMessage(messages);
                    }

                    // Xác nhận message đã được xử lý thành công
                    await Task.CompletedTask;
                };

                await channel.BasicConsumeAsync(queue: _queueName,
                                     autoAck: false,
                                     consumer: consumer);

                //Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
            
        }
        private async Task ProcessMessage(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            // Lưu message vào database hoặc thực hiện một hành động cụ thể
            if (chiTietBaiThis.Count != 0)
            {
                // nếu thứ tự là 0 là đã insert trước đó, chỉ update và ngược lại thì insert và update
                foreach (var item in chiTietBaiThis)
                {
                    if (item.ThuTu != 0)
                        await _chiTietBaiThiService.Insert(item.MaChiTietCaThi, item.MaDeHv, item.MaNhom, item.MaCauHoi, item.MaClo, DateTime.Now, item.ThuTu);
                    await _chiTietBaiThiService.Update(item.MaChiTietBaiThi, item.CauTraLoi ?? -1, DateTime.Now, item.KetQua ?? false);
                }
            }
            

        }
    }
}
