using DevPace.Domain.Exceptions.CustomerExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DevPace.WebApi.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                CustomerIdNotFoundException => HttpStatusCode.NotFound,
                FieldNotUniqueException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(new
            {
                error = context.Exception.Message
            });

            _logger.LogError(context.Exception, context.HttpContext.Request.Path);
        }
    }
}
