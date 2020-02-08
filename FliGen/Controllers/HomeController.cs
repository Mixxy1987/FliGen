using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FliGen.Models;
using FliGen.Application.Commands;
using MediatR;

namespace FliGen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediatr;

        public HomeController(ILogger<HomeController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PlayersAdministration()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> PlayersAdministration(Player player)
        {
            await _mediatr.Send(new AddPlayerCommand()
            {
                FirstName = player.FirstName,
                LastName = player.LastName
            });

            return "Игрок, " + player.FirstName + " добавлен!";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
