using Application.Settings;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Application.UseCase.Notify.Command
{
    public class NotifyConsumerDefinition : ConsumerDefinition<NotifyConsumer>
    {
        public NotifyConsumerDefinition(IOptions<RabbitMQSettings> options)
        {
            EndpointName = options.Value.CreateNotifyQueue.Split(':').Last();
        }
    }

    public class NotifyConsumer(ILogger<NotifyConsumer> logger) : IConsumer<Notify>
    {
        private readonly ILogger<NotifyConsumer> logger = logger;

        public async Task Consume(ConsumeContext<Notify> context)
        {
            try
            {
                var serializedMessage = JsonSerializer.Serialize(context.Message);

                logger.LogInformation(@"{@Consumer} Consume {@message}", nameof(NotifyConsumer), serializedMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, @"@{Consumer} An error occurred", nameof(NotifyConsumer));
                throw;
            }
        }
    }
}