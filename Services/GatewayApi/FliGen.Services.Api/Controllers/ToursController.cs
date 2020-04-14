using System;
using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Tours;
using FliGen.Services.Api.Models.Tours;
using FliGen.Services.Api.Queries.Tours;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace FliGen.Services.Api.Controllers
{
    public class ToursController : BaseController
    {
        private readonly IToursService _toursService;

        public ToursController(
            IBusPublisher busPublisher,
            ITracer tracer,
            IToursService toursService) : base(busPublisher, tracer)
        {
            _toursService = toursService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Single(await _toursService.GetAsync(id));
        }

        [HttpGet]
        public async Task<IEnumerable<Tour>> GetAsync([FromQuery]ToursByPlayerIdQuery toursByPlayerIdQuery)
        {
            return await _toursService.GetAsync(toursByPlayerIdQuery);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Post(TourCancel command)
        {
            return await SendAsync(command);
        }

        [HttpPost("forward")]
        public async Task<IActionResult> Post(TourForward command)
        {
            return await SendAsync(command);
        }

        [HttpPost("back")]
        public async Task<IActionResult> Post(TourBack command)
        {
            return await SendAsync(command);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(PlayerRegisterOnTour command)
        {
            if (string.IsNullOrWhiteSpace(command.RegistrationDate))
            {
                command.RegistrationDate = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
            }
            return await SendAsync(command);
        }
    }
}