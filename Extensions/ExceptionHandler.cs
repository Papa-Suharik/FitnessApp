namespace FitnessApp.Extensions;

using FitnessApp.CustomExceptions;
using FitnessApp.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is OperationCanceledException)
        {
            logger.LogWarning(exception, "Operation was cancelled");
            return true;
        }

        if (exception is DomainException dex)
        {
            logger.LogWarning(exception, "Domain exception occured");
            httpContext.Response.StatusCode = dex.StatusCode;
            return await problemDetailsService.TryWriteAsync(CreateCustomContext(dex, httpContext));
        }

        logger.LogError(exception, "Unhandled exception occured");
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return await problemDetailsService.TryWriteAsync(CreateGenericContext(exception, httpContext));

    }
    public ProblemDetailsContext CreateCustomContext(DomainException ex, HttpContext httpContext)
    {
        return new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = ex,
            ProblemDetails = new ProblemDetails
            {
                Type = ex.GetType().Name,
                Title = ex.Title,
                Detail = ex.Message,
                Instance = httpContext.Request.Path.ToString()
            }
        };
    }
    public ProblemDetailsContext CreateGenericContext(Exception ex, HttpContext httpContext)
    {
        return new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = ex,
            ProblemDetails = new ProblemDetails
            {
                Type = ex.GetType().Name,
                Title = "Internal error occured",
                Detail = ex.Message
            }
        };
    }
}