using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace WebAPI.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService, ILogger logger) : base(logger)
        {
            _ordersService = ordersService;
        }

        /// <summary>
        /// Add product to the order, you must be authenticated 
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderOutputDTO>> AddProductToOrder([Required] int productId, [Required] int amount)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var order = await _ordersService.AddProductToOrderAsync(userId, productId, amount);

            return order;
        }

        /// <summary>
        /// Get products of the order, you must be authenticated 
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="204">Not found</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<OrderOutputDTO>> GetProductsOfOrder(int orderId = 0)
        {
            OrderOutputDTO order;
            if (orderId == 0)
            {
                var userId = UserHelpers.GetUserIdByClaim(User.Claims);
                order = await _ordersService.GetProductsOfOrderAsync(userId);
            }
            else
            {
                order = await _ordersService.GetProductsOfOrderAsync(orderId);
            }

            return order;
        }

        /// <summary>
        /// Update products of the order, you must be authenticated 
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="204">Not found</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<OrderOutputDTO>> UpdateProductsOfOrder([FromForm][Required] OrderItemDTO orderItemDto)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var order = await _ordersService.UpdateProductsOfOrderAsync(userId, orderItemDto);

            return order;
        }

        /// <summary>
        /// Delete product from the order, you must be authenticated 
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<OrderOutputDTO>> DeleteProductsFromOrder([Required] int productId)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            await _ordersService.DeleteProductsFromOrderAsync(userId, productId);

            return NoContent();
        }

        /// <summary>
        /// Add product to the order, you must be authenticated 
        /// </summary>
        /// <response code="201">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpPost("buy")]
        [Authorize]
        public async Task<ActionResult<OrderOutputDTO>> BuyProducts()
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            await _ordersService.BuyProductsAsync(userId);

            return NoContent();
        }
    }
}