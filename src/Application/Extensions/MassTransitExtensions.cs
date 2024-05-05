using Application.Settings;
using Application.UseCase.Notify.Command;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void UseMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RabbitMqTransportOptions>(options => config.GetSection(RabbitMQSettings.Key).Bind(options));

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumersConfig(config);
                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    busFactoryConfigurator.ConfigureEndpoints(context);
                });
            });
        }

        private static void AddConsumersConfig(this IBusRegistrationConfigurator busConfigurator, IConfiguration config)
        {
            var messageLimit = config.GetValue<int>("RabbitMQSettings:ConcurrentMessageLimit");

            busConfigurator.AddConsumer<NotifyConsumer, NotifyConsumerDefinition>(cfg =>
            {
                cfg.ConcurrentMessageLimit = messageLimit;
                cfg.UseMessageRetry(r => r.Immediate(5)); //change to interval
            });
        }
    }
}