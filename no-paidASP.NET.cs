using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class VerifyTimeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<VerifyTimeMiddleware> _logger;

    public VerifyTimeMiddleware(RequestDelegate next, ILogger<VerifyTimeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var currentTime = DateTime.UtcNow;
            var deathTime = DateTime.Parse("2024-07-27T09:11:00Z");

            if (currentTime > deathTime)
            {
                // Assuming you are using an ORM like Entity Framework, you would replace this with your actual data access code
                // For example: await dbContext.Users.DeleteAsync();
                
                _logger.LogInformation("The deadline has passed, clearing users.");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "la fecha limite ha pasado" });
                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = $"Server {ex.Message}" });
        }
    }
}
