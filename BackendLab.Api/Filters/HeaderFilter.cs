using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackendLab.Api.Filters;

public class HeaderFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var hasHeader = context.HttpContext.Request.Headers.TryGetValue("Caller", out var value);
        if (hasHeader && string.Equals(value.ToString(), "Unknow", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new BadRequestObjectResult(new
            {
                message = "Invalid caller header"
            });
            
        }

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}