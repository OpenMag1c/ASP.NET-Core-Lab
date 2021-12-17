using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using DAL.Enum;
using DAL.FilterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGamesService _gamesService;
        private readonly IRatingService _ratingService;

        public GamesController(ILogger logger, IGamesService gamesService, IRatingService ratingService) : base(logger)
        {
            _gamesService = gamesService;
            _ratingService = ratingService;
        }

        /// <summary>
        /// Represent three top platforms by number of games
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="404">No products were found</response>
        [HttpGet("top-platforms")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PlatformDTO>>> GetTopPlatforms()
        { 
            var result = await _gamesService.GetTopThreePlatformsAsync();

            return result is null 
                ? NotFound(Messages.ProductNotFound) 
                : new ActionResult<IEnumerable<PlatformDTO>>(result);
        }

        /// <summary>
        /// Search products by name
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="404">No products were found</response>
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductOutputDTO>>> SearchProducts([Required]string term, [Required] int limit, [Required] int offset)
        {
            var result = await _gamesService.SearchProductsByTermAsync(term, limit, offset);

            return result is null 
                ? NotFound(Messages.ProductNotFound) 
                : new ActionResult<IEnumerable<ProductOutputDTO>>(result);
        }

        /// <summary>
        /// Find product by Id
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="404">Зroduct not found</response>
        [HttpGet("id")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductOutputDTO>> FindProductById([Required][Range(0,100)] int id)
        {
            var result = await _gamesService.FindProductByIdAsync(id);

            return result is null
                ? NotFound(Messages.ProductNotFound)
                : result;
        }

        /// <summary>
        /// Add product, only for admin
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="400">Not Completed</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">No access</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ProductOutputDTO>> AddProduct([FromForm] ProductInputDTO productInputDto)
        {
            var result = await _gamesService.AddProductAsync(productInputDto);

            return result is null
                ? BadRequest(Messages.NotCompleted)
                : result;
        }

        /// <summary>
        /// Update product, only for admin
        /// </summary>
        /// <response code="200">Product updated</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">No access</response>
        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ProductOutputDTO>> UpdateProduct([FromForm] ProductInputDTO productInputDto)
        {
            var updatedProductDto = await _gamesService.UpdateProductAsync(productInputDto);

            return updatedProductDto is null
                ? BadRequest(Messages.NotCompleted)
                : updatedProductDto;
        }

        /// <summary>
        /// Delete product, only for admin
        /// </summary>
        /// <response code="204">Product deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">No access</response>
        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _gamesService.DeleteProductAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Edit product rating, you must be authenticated 
        /// </summary>
        /// <response code="201">Rating has been created</response>
        /// <response code="400">Not Completed</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("rating")]
        [Authorize]
        public async Task<ActionResult<ProductOutputDTO>> EditRating([FromForm] int productId, [FromForm] int rating)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var productOutputDto = await _ratingService.EditProductRatingAsync(userId, productId, rating);

            return productOutputDto is null
                ? BadRequest(Messages.NotCompleted)
                : productOutputDto;
        }

        /// <summary>
        /// Delete product rating, you must be authenticated 
        /// </summary>
        /// <response code="204">Rating has been deleted</response>
        /// <response code="400">Not completed</response>
        /// <response code="401">Unauthorized</response>
        [HttpDelete("rating")]
        [Authorize]
        public async Task<IActionResult> DeleteRating(int productId)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var result = await _ratingService.DeleteRatingAsync(userId, productId);

            return result ? NoContent() : BadRequest(Messages.NotCompleted);
        }

        /// <summary>
        /// Get products with sorting and filtering
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="404">No products were found</response>
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductOutputDTO>>> GetProducts([FromQuery] Pagination pagination, [FromQuery]ProductFilters productFilters)
        {
            var products = await _gamesService.GetProductsAsync(pagination, productFilters);

            return products is null
                ? NotFound(Messages.ProductNotFound)
                : new ActionResult<IEnumerable<ProductOutputDTO>>(products);
        }
    }
}