using Business.ExceptionMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using WebAPI.Pages;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public MyErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var code = 500; // Internal Server Error by default

            if (exception is HttpStatusException httpException)
            {
                code = (int)httpException.Status;
            }

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new MyErrorResponse(exception); // Your error model
        }
    }
}
