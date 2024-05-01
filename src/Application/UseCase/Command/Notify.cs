using Application.Common.Enums;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Command
{
    public sealed class Notify : INotification
    {
        public DateTime CreatedAt { get; set; }
        public Guid CorrelationId { get; set; }
        public string Message { get; set; } = default!;
        public NotificationType Type { get; set; }
    }

    public sealed class NotifyHandler : INotificationHandler<Notify>
    {
        private readonly ILogger<NotifyHandler> logger;
        private readonly IPublishEndpoint publishEndpoint;

        public NotifyHandler(ILogger<NotifyHandler> logger, IPublishEndpoint publishEndpoint)
        {
            this.logger = logger;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Handle(Notify notification, CancellationToken cancellationToken)
        {
            try
            {
                await publishEndpoint.Publish(notification, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, @"@{Handler} - An error occurred", nameof(NotifyHandler));
                throw;
            }
        }
    }
}