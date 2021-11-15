using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebAPI.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGamesService _gamesService;

        public GamesController(ILogger logger, IGamesService gamesService) : base(logger)
        {
            _gamesService = gamesService;
        }

        /// <summary>
        /// Represent three top platforms by number of games
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpGet("top-platforms")]
        [AllowAnonymous]
        public IEnumerable<PlatformDTO> GetTopPlatforms()
        {
            var result = _gamesService.GetTopThreePlatforms();

            return result;
        }

        /// <summary>
        /// Search products by name
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpGet("search")]
        [AllowAnonymous]
        public IEnumerable<ProductDTO> SearchProducts(string term, int limit, int offset)
        {
            var result = _gamesService.SearchProductsByTerm(term, limit, offset);

            return result;
        }
    }
}
