using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogActionFilter]
    public class BaseController : ControllerBase
    {
        public readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}