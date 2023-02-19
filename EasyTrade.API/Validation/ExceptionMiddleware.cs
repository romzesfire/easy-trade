using System.Net;

namespace EasyTrade.API.Validation;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private Dictionary<Type, ValidationOptions> _validationOptions;
    public ExceptionMiddleware(RequestDelegate next, IValidationOptionsProvider validationOptionsProvider)
    {
        _next = next;
        _validationOptions = validationOptionsProvider.Get();
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var exceptionType = exception.GetType();
        if (_validationOptions.ContainsKey(exceptionType))
        {
            var options = _validationOptions[exceptionType];
            context.Response.StatusCode = options.StatusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
        
    }
    
}