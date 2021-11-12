using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAPI.Pages;

namespace WebAPI.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGamesService _gamesService;

        public GamesController(ILogger logger, IGamesService gamesService) : base(logger)
        {
            _gamesService = gamesService;
        }

        [HttpGet("top-platforms")]
        [AllowAnonymous]
        public IEnumerable<PlatformDTO> GetTopPlatforms()
        {
            return _gamesService.GetTopThreePlatforms();
        }
    }
}
