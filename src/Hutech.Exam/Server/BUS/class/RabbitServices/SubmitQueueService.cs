using Hutech.Exam.Server.Configurations;
using Microsoft.Extensions.Options;

namespace Hutech.Exam.Server.BUS.RabbitServices
{
    public class SubmitQueueService(IOptions<RabbitMQConfiguration> config) : RabbitMQService(config.Value.HostName, config.Value.QueueName)
    {
        public override Task ConsumeMessagesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task PublishMessageAsync(byte[] message)
        {
            throw new NotImplementedException();
        }
    }
}
