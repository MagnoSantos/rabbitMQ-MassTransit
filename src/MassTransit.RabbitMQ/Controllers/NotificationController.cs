using Microsoft.AspNetCore.Mvc;

namespace MassTransit.RabbitMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> logger;

        public NotificationController(ILogger<NotificationController> logger)
        {
            this.logger = logger;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task Send()
        {
            throw new NotImplementedException();
        }
    }
}
