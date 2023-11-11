using Bookshelf.Application;
using Bookshelf.Application.Commands;
using Bookshelf.Application.Queries;
using Bookshelf.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSeq(builder.Configuration.GetSection("Seq"));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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

app.MapPost("/weatherforecast", async ([FromServices] ISender mediator, [FromBody] GetWeatherForecasts query, CancellationToken cancellationToken) => await mediator.Send(query, cancellationToken))
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/books", async ([FromServices] ISender mediator, [FromBody] AddBook command, CancellationToken cancellationToken) => await mediator.Send(command, cancellationToken))
.WithName("AddBook")
.WithOpenApi();

app.MapPut("/books", async ([FromServices] ISender mediator, [FromBody] UpdateBook command, CancellationToken cancellationToken) => await mediator.Send(command, cancellationToken))
.WithName("UpdateBook")
.WithOpenApi();

app.MapDelete("/books", async ([FromServices] ISender mediator, [FromBody] DeleteBook command, CancellationToken cancellationToken) => await mediator.Send(command, cancellationToken))
.WithName("DeleteBook")
.WithOpenApi();

app.MapGet("/books", async (IMediator mediator, CancellationToken cancellationToken) => await mediator.Send(new GetBooks { }, cancellationToken))
.WithName("GetBooks")
.WithOpenApi();

await app.RunAsync();
