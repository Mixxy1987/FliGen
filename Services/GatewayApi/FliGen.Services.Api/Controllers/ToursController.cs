using System.Threading.Tasks;
using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Tours;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace FliGen.Services.Api.Controllers
{
    public class ToursController : BaseController
    {
        private readonly IToursService _toursService;

        public ToursController(IBusPublisher busPublisher, ITracer tracer,
            IToursService toursService) : base(busPublisher, tracer)
        {
            _toursService = toursService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Single(await _toursService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(TourCancel command)
        {
            return await SendAsync(command);
        }
    }
}