using MassTransit;
using System.Text.Json;

namespace Application.UseCase.Command
{
    public class NotifyConsumer : IConsumer<Notify>
    {

        public async Task Consume(ConsumeContext<Notify> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message);


        }
    }
}