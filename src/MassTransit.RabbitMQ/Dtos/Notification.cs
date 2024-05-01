using Application.Common.Enums;

namespace MassTransit.RabbitMQ.Dtos
{
    public record class Notification(string Message, NotificationType Type);
}