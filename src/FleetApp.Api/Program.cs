using FleetApp.Api.Composition;
using FleetApp.Api.Endpoints;
using FleetApp.Api.Middleware;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()                         
    .Enrich.WithProperty("App", "FleetApp.Api")      
    .WriteTo.File(
        path: "logs/fleetapp-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate:
        "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] TraceId={TraceId} {Message:lj}{NewLine}{Exception}",
        shared: true)                           
    .CreateLogger();



builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFleetApp();


CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(0);
    c.DefaultModelExpandDepth(0);

});

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms (traceId={TraceId})";

    options.EnrichDiagnosticContext = (diag, ctx) =>
    {
        diag.Set("TraceId", ctx.TraceIdentifier);
        diag.Set("UserAgent", ctx.Request.Headers["User-Agent"].ToString());
    };
});

app.UseGlobalProblemDetails();

app.MapVehicleEndpoints();

app.Run();

public partial class Program { }