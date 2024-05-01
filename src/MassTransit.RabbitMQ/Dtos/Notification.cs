namespace MassTransit.RabbitMQ.Dtos
{
    public record class Notification(DateTime CreatedAt, string Message, NotificationType Type);
}
