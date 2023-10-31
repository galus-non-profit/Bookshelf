namespace Bookshelf.Infrastructure.SqlServer;

using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Infrastructure.SqlServer.Interfaces;
using Bookshelf.Infrastructure.SqlServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var dictionary = new Dictionary<Guid, Book>();

        services.AddSingleton<IBookRepository>(new BookRepository(dictionary));
        services.AddSingleton<IBookReadService>(new BookReadService(dictionary));
    }
}
