using MyStocks.Api;
using MyStocks.Api.Middlewares;
using MyStocks.Application;
using MyStocks.Application.Abstractions;
using MyStocks.Infrastructure;
using MyStocks.Infrastructure.Authentication;
using System.Configuration;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseMiddleware<GlobalHandleException>();
app.Run();
