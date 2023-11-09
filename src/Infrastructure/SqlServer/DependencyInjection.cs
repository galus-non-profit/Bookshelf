namespace Bookshelf.Infrastructure.SqlServer;

using Bookshelf.Domain.Interfaces;
using Bookshelf.Infrastructure.Interfaces;
using Bookshelf.Infrastructure.SqlServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        services.AddSingleton<IBookRepository>(new BookRepository(connectionString));
        services.AddSingleton<IBookReadService>(new BookReadService(connectionString));
    }
}
