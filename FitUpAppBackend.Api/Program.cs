using FitUpAppBackend.Api.Extensions;
using FitUpAppBackend.Application;
using FitUpAppBackend.Core;
using FitUpAppBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddApi(builder.Configuration);

var app = builder.Build();

app.UseInfrastructure().UseApi();
app.MapControllers();

app.Run();