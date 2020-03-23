using FliGen.Application.Dto;
using FliGen.Application.Queries.Tour.MyTours;
using FliGen.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToursController : ControllerBase
    {
        private readonly ILogger<ToursController> _logger;
        private readonly IMediator _mediatr;
        private readonly IIdentityService _identityService;

        public ToursController(
            ILogger<ToursController> logger,
            IMediator mediatr,
            IIdentityService identityService)
        {
            _logger = logger;
            _mediatr = mediatr;
            _identityService = identityService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<MyTour>))]
        public Task<IEnumerable<MyTour>> Get()
        {
	        var userId = _identityService.GetUserIdentity();
            return _mediatr.Send(new MyToursQuery(userId));
        }
    }
}