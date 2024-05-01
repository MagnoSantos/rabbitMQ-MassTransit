using Application.UseCase.Command;
using MassTransit.RabbitMQ.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.RabbitMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator mediator;

        public NotificationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Send([FromBody] Notification notificationDto, CancellationToken cancellationToken)
        {
            await mediator.Publish(new Notify
            {
                CorrelationId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Message = notificationDto.Message,
                Type = notificationDto.Type
            }, cancellationToken);

            return Ok();
        }
    }
}