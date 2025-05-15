using Hutech.Exam.Server.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace Hutech.Exam.Server.BUS.RabbitServices
{
    public class AnswerQueueService(IOptions<RabbitMQConfiguration> config, RedisService redisService) : RabbitMQService(config.Value.HostName, config.Value.QueueName)
    {
        private readonly RedisService _redisService = redisService;

        // Publish a message to the queue
        public override async Task PublishMessageAsync(byte[] message)
        {
            try
            {
                if (_channel == null)
                {
                    await base.InitializeAsync(); // Ensure initialization if channel is not available
                }
                else
                {

                    await _channel.BasicPublishAsync(exchange: "",
                                                     routingKey: _queueName,
                                                     body: message);
                }
            }
            catch (Exception ex)
            {
                // Log error and rethrow or handle accordingly
                Console.Error.WriteLine($"Error while publishing message: {ex.Message}");
                throw;
            }
        }

        // Consume messages from the queue
        public override async Task ConsumeMessagesAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_channel == null)
                {
                    await base.InitializeAsync(); // Ensure initialization if channel is not available
                }
                else
                {
                    var consumer = new AsyncEventingBasicConsumer(_channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        if (cancellationToken.IsCancellationRequested) return;

                        var body = ea.Body.ToArray();

                        if (body != null)
                        {
                            await ProcessMessageAsync(body);
                        }

                        // Acknowledge the message after processing
                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    };

                    await _channel.BasicConsumeAsync(queue: _queueName,
                                                     autoAck: false,
                                                     consumer: consumer);

                    // Keep consuming messages until cancellation is requested
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }

            }
            catch (Exception ex)
            {
                // Log error and rethrow or handle accordingly
                Console.Error.WriteLine($"Error during message consumption: {ex.Message}");
                throw;
            }
        }


        private async Task ProcessMessageAsync(byte[] chiTietBaiThis)
        {
            await _redisService.SetChiTietBaiLamAsync(chiTietBaiThis);
        }
    }
}
