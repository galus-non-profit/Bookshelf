namespace Bookshelf.Application;

using Bookshelf.Application.Queries;
using Bookshelf.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddScoped<IValidator<GetWeatherForecasts>, GetWeatherForecastsValidator>();

        return services;
    }
}
