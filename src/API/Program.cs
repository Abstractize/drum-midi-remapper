using Services;
using Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();
builder.Services.AddManagers();

var app = builder.Build();

app.MapControllers();

app.Run();
