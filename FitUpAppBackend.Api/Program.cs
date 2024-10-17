using FitUpAppBackend.Api.Extensions;
using FitUpAppBackend.Application;
using FitUpAppBackend.Core;
using FitUpAppBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddApi();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseInfrastructure();

app.MapControllers();

app.Run();