using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Exceptions;

namespace Product.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
                $"An error occurred while processing your request: {exception.Message}");


            var problemDetails = new ProblemDetails()
            {
                Instance = httpContext.TraceIdentifier,
                Detail = exception.Message
            };

            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Validation Error";
                    problemDetails.Extensions["errors"] = validationException.Errors;
                    break;

                case NotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Resource Not Found";
                    break;

                case BadRequestException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Bad Request";
                    break;

                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    break;
            }

            // Set response status code and write ProblemDetails as JSON
            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
