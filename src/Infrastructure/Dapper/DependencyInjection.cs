namespace Bookshelf.Infrastructure.Dapper;

using Bookshelf.Domain.Interfaces;
using Bookshelf.Infrastructure.Interfaces;
using Bookshelf.Infrastructure.SqlServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    private const string KEY = "Dapper";

    public static void AddDapper(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        var options = new DapperOptions
        {
            ConnectionString = connectionString,
        };

        services.AddSingleton<DapperOptions>(options);

        services.AddKeyedSingleton<IBookRepository, BookRepository>(KEY);
        services.AddKeyedSingleton<IBookReadService, BookReadService>(KEY);
    }
}
