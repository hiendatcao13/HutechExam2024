using Hutech.Exam.Server.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace Hutech.Exam.Server.BUS.RabbitServices
{
    public class AnswerQueueService(IOptions<RabbitMQConfiguration> config, RedisService redisService) : RabbitMQService(config.Value.UserName, config.Value.Password, config.Value.HostName, config.Value.QueueName)
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
                if (_channel != null)
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
                if (_channel != null)
                {
                    //await _channel.BasicQosAsync(0, 10, false); // Fair dispatch
                    var consumer = new AsyncEventingBasicConsumer(_channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        if (cancellationToken.IsCancellationRequested) return;

                        try
                        {
                            var body = ea.Body.ToArray();

                            if (body != null)
                            {
                                await ProcessMessageAsync(body);
                            }

                            // Acknowledge the message after successful processing
                            await _channel.BasicAckAsync(ea.DeliveryTag, false);
                        }
                        catch (Exception ex)
                        {
                            // Log the error and Nack the message to requeue it
                            Console.Error.WriteLine($"Error processing message: {ex.Message}");
                            await _channel.BasicNackAsync(ea.DeliveryTag, false, true); // requeue = true
                        }
                    };

                    await _channel.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);

                    // Keep consuming messages until cancellation is requested
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }

            }
            catch (TaskCanceledException)
            {
                await base.DisposeAsync();
            }
            catch (Exception ex)
            {
                // Log error and rethrow or handle accordingly
                Console.Error.WriteLine($"Error during message consumption: {ex.Message}");
            }
        }


        public override async Task ProcessMessageAsync(byte[] message)
        {
            try
            {
                await _redisService.SetChiTietBaiLamAsync(message);
            }
            catch(Exception ex)
            {
                // Log error and rethrow or handle accordingly
                Console.Error.WriteLine($"Error processing message: {ex.Message}");
                throw;
            }
        }


    }
}
