using Business.ExceptionMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.ErrorResponse;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        [Route("error")]
        public ActionResult<ErrorResponse.ErrorResponse> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;

            if (exception is HttpStatusException httpException)
            {
                code = (int)httpException.Status;
                Response.StatusCode = code;
                return new ErrorResponse.ErrorResponse(exception);
            }

            Response.StatusCode = code;

            return View("~/Views/Error.cshtml");
        }
    }
}