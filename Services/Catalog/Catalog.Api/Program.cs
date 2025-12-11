using Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts => 
{
    opts.Connection(builder.Configuration.GetConnectionString("Databse")!);

}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Databse")!);

var app = builder.Build();

//Configure the HTTP request pipeline.

app.MapCarter();

app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health", 
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

//app.UseExceptionHandler(exceptionHandlerApp => 
//{
//    exceptionHandlerApp.Run(async context => 
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

//        if (exception == null)
//        {
//            return;
//        }

//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});


