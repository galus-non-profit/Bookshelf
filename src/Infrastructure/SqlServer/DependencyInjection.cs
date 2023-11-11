namespace Bookshelf.Infrastructure.SqlServer;

using Bookshelf.Domain.Interfaces;
using Bookshelf.Infrastructure.Interfaces;
using Bookshelf.Infrastructure.SqlServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    private const string KEY = "SQLServer";

    public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        var options = new SqlServerOptions
        {
            ConnectionString = connectionString,
        };

        services.AddSingleton<SqlServerOptions>(options);

        services.AddKeyedSingleton<IBookRepository, BookRepository>(KEY);
        services.AddKeyedSingleton<IBookReadService, BookReadService>(KEY);
    }
}
