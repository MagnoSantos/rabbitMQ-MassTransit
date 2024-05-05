using Application.Common.Enums;
using Application.Settings;
using Domain.Common;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCase.Notify.Command
{
    public class Notify(DateTime createdAt, Guid correlationId) : IRequest<Result<bool>>
    {
        public DateTime CreatedAt { get; init; } = createdAt;
        public Guid CorrelationId { get; init; } = correlationId;
        public string Message { get; init; } = default!;
        public NotificationType Type { get; init; }
    }

    public sealed class NotifyHandler(ILogger<NotifyHandler> logger, ISendEndpointProvider sendEndpointProvider, IOptions<RabbitMQSettings> options) 
        : IRequestHandler<Notify, Result<bool>>
    {
        private readonly ILogger<NotifyHandler> logger = logger;
        private readonly ISendEndpointProvider sendEndpoint = sendEndpointProvider;

        public async Task<Result<bool>> Handle(Notify request, CancellationToken cancellationToken)
        {
            var result = new Result<bool>();

            try
            {
                var endpoint = await sendEndpoint.GetSendEndpoint(new Uri(options.Value.CreateNotifyQueue));
                await endpoint.Send(request, cancellationToken);

                result.SetStatusCode(System.Net.HttpStatusCode.OK);
                result.Data = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, @"@{Handler} - An error occurred", nameof(NotifyHandler));

                result.SetStatusCode(System.Net.HttpStatusCode.InternalServerError);
                result.AddError("Erro ocorrido ao publicar mensagem na fila");
            }

            return result;
        }
    }
}