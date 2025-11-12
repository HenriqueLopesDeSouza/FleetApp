using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Serilog.Context; 

namespace FleetApp.Api.Middleware;

public static class ProblemDetailsExtensions
{
    public static void UseGlobalProblemDetails(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(a => a.Run(async ctx =>
        {
            var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
            var logger = ctx.RequestServices
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("GlobalException");

            var traceId = ctx.TraceIdentifier;

            using (LogContext.PushProperty("TraceId", traceId))
            {
                if (ex is not null)
                {
                    logger.LogError(ex,
                        """
                         Unhandled exception on {Path}
                        → {ExceptionType}: {Message}
                        → TraceId: {TraceId}
                        """,
                        ctx.Request.Path,
                        ex.GetType().Name,
                        ex.Message,
                        traceId);
                }
            }



            var (status, title, detail) = ex switch
            {
                JsonException jex => (
                    400,
                    "Invalid JSON payload",
                    jex.Path is { Length: > 0 }
                        ? $"Error at '{jex.Path}': {jex.Message}"
                        : jex.Message
                ),
                BadHttpRequestException brex => (400, "Bad Request", brex.Message),

                InvalidOperationException e => (409, "Conflict", e.Message),
                KeyNotFoundException e => (404, "Not Found", e.Message),
                ArgumentException e => (400, "Bad Request", e.Message),

                _ => (500, "Server Error", "Unexpected error")
            };

            ctx.Response.StatusCode = status;

            var problem = Results.Problem(
                title: title,
                detail: detail,
                statusCode: status,
                extensions: new Dictionary<string, object?> { ["traceId"] = traceId });

            await problem.ExecuteAsync(ctx);
        }));
    }
}
