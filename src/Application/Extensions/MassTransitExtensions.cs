using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddMassTransitConfig(this IServiceCollection services)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                var entryAssembly = Assembly.GetExecutingAssembly();

                busConfigurator.AddConsumers(entryAssembly);

                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    busFactoryConfigurator.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    busFactoryConfigurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}