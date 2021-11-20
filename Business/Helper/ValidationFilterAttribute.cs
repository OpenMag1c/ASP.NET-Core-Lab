using System;
using Business.Models;
using DAL.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Business.Helper
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var productFilters = context.ActionArguments["ProductFilters"];
            if (productFilters is not ProductFilters)
            {
                context.Result = new BadRequestObjectResult("Wrong params!");
            }

            var pagination = context.ActionArguments["Pagination"] as Pagination;
            if (pagination == null)
            {
                context.Result = new BadRequestObjectResult("Wrong pagination!");
            }
            else
            {
                var pages = pagination;
                if (pages.PageNumber < 1 || pages.PageSize < 1)
                {
                    context.Result = new BadRequestObjectResult("Wrong pagination params!");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}