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
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumersConfig(config);

                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    busFactoryConfigurator.ConfigureHost(config);
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

        private static void ConfigureHost(this IRabbitMqBusFactoryConfigurator busFactoryConfigurator, IConfiguration config)
        {
            var host = config.GetValue<string>("RabbitMQSettings:Host");
            var virtualHost = config.GetValue<string>("RabbitMQSettings:VirtualHost");
            var username = config.GetValue<string>("RabbitMQSettings:Username");
            var password = config.GetValue<string>("RabbitMQSettings:Password");

            busFactoryConfigurator.Host(host, virtualHost, h =>
            {
                h.Username(username ?? throw new ArgumentNullException(username));
                h.Password(password ?? throw new ArgumentNullException(password));
            });
        }
    }
}