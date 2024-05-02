namespace Application.Settings
{
    public class RabbitMQSettings
    {
        public string Key = nameof(RabbitMQSettings);

        public string Host { get; set; } = default!;
        public string VirtualHost { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string CreateNotifyQueue { get; set; } = default!;
        public int ConcurrentMessageLimit { get; set; } = default!;
    }
}