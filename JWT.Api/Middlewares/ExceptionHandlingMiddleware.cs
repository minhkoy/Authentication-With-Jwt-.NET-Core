using System.Net;
using JWT.Manager.Bases;

namespace JWT.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError($"{e}");
            ProblemDetails problemDetails = null;
            if (e is ValidationException validationException)
            {
                problemDetails = new ProblemDetails (
                    Status : StatusCodes.Status400BadRequest,
                    Type : "ValidationError",
                    Title : "Validation error",
                    Detail : "One or more validation errors has occured",
                    Errors : validationException.Errors
                );
            }
            else
            {
                problemDetails = new ProblemDetails(
                    Status: StatusCodes.Status500InternalServerError,
                    Type: "ServerError",
                    Title: "Server Error",
                    Detail: "An unexpected error has occured",
                    null
                );
            }

            context.Response.StatusCode = problemDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    internal record ProblemDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object> Errors);
}