using FitUpAppBackend.Application;
using FitUpAppBackend.Core;
using FitUpAppBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddCore();


var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseInfrastructure();

app.Run();