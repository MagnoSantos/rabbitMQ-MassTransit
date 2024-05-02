using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class SettingsExtensions
    {
        public static void AddSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(options => configuration.GetSection("RabbitMQSettings").Bind(options));
        }
    }
}