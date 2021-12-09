using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace WebAPI.ActionFilters
{
    public class LogActionFilterAttribute : ActionFilterAttribute

        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                Log("OnActionExecuting", context.RouteData);
            }

            public override void OnActionExecuted(ActionExecutedContext context)
            {
                Log("OnActionExecuted", context.RouteData);
            }

            public override void OnResultExecuting(ResultExecutingContext context)
            {
                Log("OnResultExecuting", context.RouteData);
            }

            public override void OnResultExecuted(ResultExecutedContext context)
            {
                Log("OnResultExecuted", context.RouteData);
            }

            private void Log(string methodName, RouteData routeData)
            {
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];
                var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
                Serilog.Log.Information(message, "Action Filter Log");
            }
        }
}