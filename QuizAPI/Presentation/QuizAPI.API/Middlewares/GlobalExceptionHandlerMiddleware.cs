using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace QuizAPI.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {

        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Global Exception Handler: {ErrorMessage}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var problemDetails = new ProblemDetails
                {
                    Status = context.Response.StatusCode,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
        }
    }
}

