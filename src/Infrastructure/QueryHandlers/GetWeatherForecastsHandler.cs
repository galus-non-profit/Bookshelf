namespace Bookshelf.Infrastructure.QueryHandlers;

using Bookshelf.Application.Queries;
using Bookshelf.Application.ViewModels;
using FluentValidation;
using MediatR;

internal sealed class GetWeatherForecastsHandler : IRequestHandler<GetWeatherForecasts, IList<WeatherForecast>>
{
    private readonly IValidator<GetWeatherForecasts> validator;

    public GetWeatherForecastsHandler(IValidator<GetWeatherForecasts> validator)
    {
        this.validator = validator;
    }

    public async Task<IList<WeatherForecast>> Handle(GetWeatherForecasts request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request);

        if (validationResult.IsValid is false)
        {
            var errors = string.Join(Environment.NewLine, validationResult.Errors);
            throw new Exception(errors);
        }

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, request.Count).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();

        return await Task.FromResult(forecast);
    }
}
