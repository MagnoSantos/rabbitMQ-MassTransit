using Application.UseCase.Notify.Command;
using Domain.Common;
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
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Send([FromBody] Notification notificationDto, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new Notify(DateTime.UtcNow, Guid.NewGuid())
            {
                Message = notificationDto.Message,
                Type = notificationDto.Type
            }, cancellationToken));
        }
    }
}