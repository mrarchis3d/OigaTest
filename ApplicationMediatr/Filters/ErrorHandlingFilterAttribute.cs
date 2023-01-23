using ApplicationMediatr.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ApplicationMediatr.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorHandlingFilterAttribute> _logger;

        public ErrorHandlingFilterAttribute(ILogger<ErrorHandlingFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is CustomException customException)
            {
                context.HttpContext.Response.StatusCode = customException.StatusCode;
                context.Result = new JsonResult(customException.Error);
            }
            else
            {
                context.HttpContext.Response.StatusCode = 500; // Internal Server Error
                context.Result = new JsonResult(new { error = "An error occurred while processing your request :: " + context.Exception.Message });
            }
            _logger.LogError("Exception occurred while executing request: {ex}", context.Exception);
        }
    }
}
