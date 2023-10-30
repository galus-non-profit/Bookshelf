namespace Bookshelf.Infrastructure.SqlServer.Interfaces;

using Bookshelf.Application.ViewModels;

internal interface IBookReadService
{
    Task<IReadOnlyList<Book>> GetAllBooks(CancellationToken cancellationToken = default);
}
