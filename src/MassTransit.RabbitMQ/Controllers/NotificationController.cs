using Application.UseCase.Notify.Command;
using MassTransit.RabbitMQ.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.RabbitMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Send([FromBody] Notification notificationDto, CancellationToken cancellationToken)
        {
            await mediator.Publish(new Notify(DateTime.UtcNow, Guid.NewGuid())
            {
                Message = notificationDto.Message,
                Type = notificationDto.Type
            }, cancellationToken);

            return Ok();
        }
    }
}