namespace Application.Settings
{
    public class RabbitMQSettings
    {
        public static readonly string Key = nameof(RabbitMQSettings);

        public string Host { get; set; } = default!;
        public string VHost { get; set; } = default!;
        public string User { get; set; } = default!;
        public string Pass { get; set; } = default!;
        public int ConcurrentMessageLimit { get; set; } = default!;

        #region Queues

        public string CreateNotifyQueue { get; set; } = default!;

        #endregion Queues
    }
}