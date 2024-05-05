using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class SettingsExtensions
    {
        public static void UseOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(options => configuration.GetSection(RabbitMQSettings.Key).Bind(options));
        }
    }
}