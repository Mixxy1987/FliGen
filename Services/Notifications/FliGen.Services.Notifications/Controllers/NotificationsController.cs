using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FliGen.Services.Notifications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;
        private readonly IMediator _mediatr;

        public NotificationsController(
            ILogger<NotificationsController> logger,
            IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }
    }
}