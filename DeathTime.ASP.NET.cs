using System;
using DeathTime.ASP.NET.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DeathTime.ASP.NET.MIddleware
{
    public class DeathTimerMid
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DeathTimerMid> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public DeathTimerMid(
            RequestDelegate next,
            ILogger<DeathTimerMid> logger,
            IServiceScopeFactory scopeFactory
            )
        {
            this._next = next;
            this._logger = logger;
            this._scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                var currenTime = DateTime.Now;
                var deathTimer = DateTime.ParseExact("0000-00-00T00:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);

                if (currenTime > deathTimer)
                {
                    using (var scope = this._scopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        await context.UserModel.ExecuteDeleteAsync();
                    }

                    this._logger.LogInformation("the deadline has passed, clearing users.");
                    ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await ctx.Response.WriteAsJsonAsync(new { message = "Time hab aspired" });
                    return;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ah error ocurred");
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await ctx.Response.WriteAsJsonAsync(new { message = $"Server {ex.Message}" });
            }
            await this._next(ctx);
        }
    }
}
