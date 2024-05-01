using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.UseCase.Notify.Command
{
    public class NotifyConsumer(ILogger<NotifyConsumer> logger) : IConsumer<Notify>
    {
        private readonly ILogger<NotifyConsumer> logger = logger;

        public async Task Consume(ConsumeContext<Notify> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message);

            logger.LogInformation(@"{@Consumer} Consume {@message}", nameof(NotifyConsumer), serializedMessage);
        }
    }
}