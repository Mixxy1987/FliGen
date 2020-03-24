using FliGen.Application.Dto;
using FliGen.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Application.Commands.Tour.TourCancelCommand;
using FliGen.Application.Commands.Tour.TourForwardCommand;
using FliGen.Application.Queries.MyTours;

namespace FliGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyToursController : ControllerBase
    {
        private readonly ILogger<MyToursController> _logger;
        private readonly IMediator _mediatr;
        private readonly IIdentityService _identityService;

        public MyToursController(
            ILogger<MyToursController> logger,
            IMediator mediatr,
            IIdentityService identityService)
        {
            _logger = logger;
            _mediatr = mediatr;
            _identityService = identityService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> Get()
        {
	        var userId = _identityService.GetUserIdentity();
            return _mediatr.Send(new MyToursQuery(userId));
        }

        [HttpPost("forward")]
        public async Task TourForward([FromBody]TourForwardCommand cmd)
        {
            await _mediatr.Send(cmd);
        }

        [HttpPost("cancel")]
        public async Task TourCancel([FromBody]TourCancelCommand cmd)
        {
            await _mediatr.Send(cmd);
        }
    }
}