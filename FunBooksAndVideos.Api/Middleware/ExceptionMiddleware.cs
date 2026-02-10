using FunBooksAndVideos.Api.Models.ValidationErrors;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace FunBooksAndVideos.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message;
            ValidationProblemDetails problem;

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new ValidationProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    message = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new ValidationProblemDetails
                    {
                        Title = notFoundException.Message,
                        Status = (int)HttpStatusCode.NotFound,
                        Detail = notFoundException.InnerException?.Message,
                        Type = nameof(NotFoundException)
                    };
                    break;
                case CustomerInvariantViolationException:
                case PurchaseOrderInvariantViolationException:
                case InvalidMembershipException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new ValidationProblemDetails
                    {
                        Title = "Domain rule violation",
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = ex.Message,
                        Type = ex.GetType().Name
                    };
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    problem = new ValidationProblemDetails
                    {
                        Title = _env.IsDevelopment() ? ex.Message : "An error occurred",
                        Detail = _env.IsDevelopment() ? ex.StackTrace : null,
                        Type = nameof(HttpStatusCode.InternalServerError)
                    };
                    break;
            }

            _logger.LogError(ex, "An error occurred: {Message}", problem.Detail);

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}