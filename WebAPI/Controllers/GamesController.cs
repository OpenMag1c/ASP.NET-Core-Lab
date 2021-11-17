using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Business.Interfaces;
using DAL.Enum;
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

        /// <summary>
        /// Find product by Id
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpGet("id")]
        [AllowAnonymous]
        public ActionResult<ProductDTO> FindProductById(int id)
        {
            var result = _gamesService.FindProductById(id);

            return result;
        }

        /// <summary>
        /// Add product, only for admin
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ProductDTO>> AddNewProduct([FromBody] ProductDTO productDto)
        {
            var result = await _gamesService.AddNewProductAsync(productDto);
            Response.StatusCode = 201;

            return result;
        }

        /// <summary>
        /// Update product, only for admin
        /// </summary>
        /// <response code="200">Product updated</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">No access</response>
        /// <response code="500">Oops!</response>
        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ProductDTO>> UpdateProduct([FromBody] ProductDTO productDto)
        {
            var updatedProductDto = await _gamesService.UpdateProductAsync(productDto);

            return updatedProductDto;
        }

        /// <summary>
        /// Delete product, only for admin
        /// </summary>
        /// <response code="204">Product deleted</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">No access</response>
        /// <response code="500">Oops!</response>
        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteProduct(int id)
        {
            _gamesService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
