using Bookshelf.Application;
using Bookshelf.Application.Queries;
using Bookshelf.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(loggingBuilder => loggingBuilder.AddSeq(apiKey: "BwG5tK11R4G9jAnuoldS"));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGet("/weatherforecast", async (IMediator mediator, [FromQuery] int count = 5) => await mediator.Send(new GetWeatherForecasts { Count = count, }))
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.MapPost("/weatherforecast", async (IMediator mediator, [FromBody] GetWeatherForecasts query) => await mediator.Send(query))
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
