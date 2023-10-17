namespace Bookshelf.Application.Queries;

using Bookshelf.Application.ViewModels;

public sealed record GetWeatherForecasts : IRequest<IList<WeatherForecast>>
{
    public required int Count { get; init; }
}
