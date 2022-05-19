using kr.bbon.AspNetCore.Filters;
using kr.bbon.Core;
using kr.bbon.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FormBuilderApp.Infrastructure.Filters;

public class ApiExceptionHandlerWithLoggingFilter : ApiExceptionHandlerFilter
{
    public ApiExceptionHandlerWithLoggingFilter(
        ILogger<ApiExceptionHandlerWithLoggingFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        Logging(context);

        base.OnException(context);
    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        Logging(context);

        return base.OnExceptionAsync(context);
    }

    private void Logging(ExceptionContext context)
    {
        if (context.Exception != null)
        {
            _logger.LogError(context.Exception, $"{context.Exception.Message}");
        }
    }

    private readonly ILogger _logger;
}