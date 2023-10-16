namespace Bookshelf.Application.Validators;

using Bookshelf.Application.Queries;
using FluentValidation;

public sealed class GetWeatherForecastsValidator : AbstractValidator<GetWeatherForecasts>
{
    public GetWeatherForecastsValidator()
    {
        RuleFor(query => query.Count).GreaterThan(0).WithMessage("Please specify count");
    }
}
