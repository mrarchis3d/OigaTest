using Application;
using Domain.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfiguringControllers();
builder.Services.AddServices();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.ConfiguringSwagger();

var app = builder.Build();

app.ConfiguringApplication();

app.Run();