using Application.Common.Enums;
using Application.Settings;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCase.Notify.Command
{
    public sealed class Notify(DateTime createdAt, Guid correlationId) : INotification
    {
        public DateTime CreatedAt { get; } = createdAt;
        public Guid CorrelationId { get; } = correlationId;

        public string Message { get; set; } = default!;
        public NotificationType Type { get; set; }
    }

    public sealed class NotifyHandler(ILogger<NotifyHandler> logger, ISendEndpointProvider sendEndpointProvider, IOptions<RabbitMQSettings> options) 
        : INotificationHandler<Notify>
    {
        private readonly ILogger<NotifyHandler> logger = logger;
        private readonly ISendEndpointProvider sendEndpoint = sendEndpointProvider;

        public async Task Handle(Notify notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await sendEndpoint.GetSendEndpoint(new Uri(options.Value.CreateNotifyQueue));
                await endpoint.Send(notification, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, @"@{Handler} - An error occurred", nameof(NotifyHandler));
                throw;
            }
        }
    }
}