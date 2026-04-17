namespace FitnessApp.Extensions;

using FitnessApp.CustomExceptions;
using FitnessApp.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<UserService> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            UserNotFoundException => StatusCodes.Status404NotFound,
            WrongDataProvidedException => StatusCodes.Status400BadRequest,
            DuplicateUserException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        if(exception is not DomainException)
        {
            logger.LogError(exception, "Unhandled exception occured");
        }

        if(exception is OperationCanceledException)
        {
            logger.LogError(exception, "Operation was cancelled");
        }

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occured",
                Detail = exception.Message
            }
        });
    }
}