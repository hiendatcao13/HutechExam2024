using RabbitMQ.Client;

namespace Hutech.Exam.Server.BUS.RabbitServices
{
    public abstract class RabbitMQService(string hostname, string queue) : IAsyncDisposable
    {
        //TODO: ta có thể tạo 2 channel trong đây với 2 tham số queue khác nhau, chưa làm
        protected readonly string _hostname = hostname;
        protected readonly string _queueName = queue;
        protected IConnection _connection = default!;
        protected IChannel _channel = default!;

        // Initialize connection and channel once
        protected async Task InitializeAsync()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostname, UserName = "datcao", Password = "123456" };
                _connection = await factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
                await _channel.BasicQosAsync(0, 1, false);

                await DeclareQueueAsync();
            }
            catch (Exception ex)
            {
                // Log error and rethrow or handle accordingly
                Console.Error.WriteLine($"Error during RabbitMQ initialization: {ex.Message}");
                throw;
            }
        }
        // Declare Queue to ensure it exists
        protected async Task DeclareQueueAsync()
        {
            if (_channel == null) throw new InvalidOperationException("Channel is not initialized.");

            await _channel.QueueDeclareAsync(queue: _queueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);
        }
        public abstract Task PublishMessageAsync(byte[] message);
        public abstract Task ConsumeMessagesAsync(CancellationToken cancellationToken);
        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_channel != null && _channel.IsOpen)
                {
                    await _channel.CloseAsync();
                }

                if (_connection != null && _connection.IsOpen)
                {
                    await _connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                // Log error during resource cleanup
                Console.Error.WriteLine($"Error during cleanup: {ex.Message}");
            }
        }
    }
}
