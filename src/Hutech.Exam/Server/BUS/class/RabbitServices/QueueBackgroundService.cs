
using Hutech.Exam.Server.BUS.RabbitServices;

namespace Hutech.Exam.Server.BUS
{
    public class QueueBackgroundService(AnswerQueueService answerQueueService) : BackgroundService
    {
        private readonly RabbitMQService _answerQueueService = answerQueueService;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _answerQueueService.ConsumeMessagesAsync(stoppingToken);
        }
    }
}
