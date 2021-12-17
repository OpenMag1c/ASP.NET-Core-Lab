using Business.ExceptionMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ActionResult<ErrorResponse> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;

            if (exception is HttpStatusException httpException)
            {
                code = (int)httpException.Status;
                Response.StatusCode = code;
                return new ErrorResponse(exception);
            }

            Response.StatusCode = code;

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}