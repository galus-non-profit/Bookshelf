namespace Bookshelf.Infrastructure.Dapper.Services;

using System.Collections.Generic;
using Bookshelf.Application.ViewModels;
using Bookshelf.Infrastructure.Interfaces;

internal sealed class BookReadService : IBookReadService
{
    private readonly ILogger<BookReadService> logger;
    private readonly DapperOptions options;

    public BookReadService(ILogger<BookReadService> logger, DapperOptions options)
        => (this.logger, this.options) = (logger, options);

    public Task<IReadOnlyList<Book>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
