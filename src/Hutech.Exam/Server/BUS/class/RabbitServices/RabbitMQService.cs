using RabbitMQ.Client;

namespace Hutech.Exam.Server.BUS.RabbitServices
{
    public abstract class RabbitMQService(string username, string password, string hostname, string queue) : IAsyncDisposable
    {
        //TODO: ta có thể tạo 2 channel trong đây với 2 tham số queue khác nhau, chưa làm
        protected readonly string _hostname = hostname;
        protected readonly string _queueName = queue;
        protected readonly string _username = username;
        protected readonly string _password = password;
        protected IConnection _connection = default!;
        protected IChannel _channel = default!;

        // Initialize connection and channel once
        protected async Task InitializeAsync()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
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
        // Liên kết queue với exchange thông qua routing key
        public abstract Task PublishMessageAsync(byte[] message);
        public abstract Task ConsumeMessagesAsync(CancellationToken cancellationToken);
        // Xử lý message
        public abstract Task ProcessMessageAsync(byte[] message);
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
