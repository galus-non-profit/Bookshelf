namespace Bookshelf.Application.Queries;

using MediatR;
using Bookshelf.Application.ViewModels;
using System.Text.Json.Serialization;

public sealed record GetWeatherForecasts : IRequest<IList<WeatherForecast>>
{
    [JsonPropertyName("dupa")] public required int Count { get; init; }
}
