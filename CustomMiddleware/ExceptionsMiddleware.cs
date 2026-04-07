namespace FitnessApp.CustomMiddleware;

public class ErrorsMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorsMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch 
            {
                UserNotFoundException => StatusCodes.Status404NotFound,
                WrongDataProvidedException => StatusCodes.Status400BadRequest,
                DuplicateUserException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new
            {   
                title = "Error",
                code = context.Response.StatusCode,
                message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        
    }
}

static class Errors
{
    public static IApplicationBuilder UseErrorsMiddlware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorsMiddleware>();
    }
}

public class UserNotFoundException(string message) : Exception(message){}
public class WrongDataProvidedException(string message) : Exception(message){}
public class DuplicateUserException(string message) : Exception(message){}