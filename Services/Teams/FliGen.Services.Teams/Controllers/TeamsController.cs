﻿using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TeamsController : ControllerBase
	{
		private readonly ILogger<TeamsController> _logger;
		private readonly IMediator _mediatr;

		public TeamsController(ILogger<TeamsController> logger, IMediator mediatr)
		{
			_logger = logger;
			_mediatr = mediatr;
		}

        [HttpGet("ToursByPlayerId")]
        [Produces(typeof(ToursByPlayerIdDto))]
        public Task<ToursByPlayerIdDto> Get([FromQuery]int playerId, int size)
        {
            return _mediatr.Send(new ToursByPlayerIdQuery(playerId, size));
        }
	}
}
