namespace Bookshelf.Domain.Interfaces;

using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Types;

public interface IBookRepository
{
    Task CreateAsync(Book entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(BookId id, CancellationToken cancellationToken = default);
    Task<Book?> ReadAsync(BookId id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Book entity, CancellationToken cancellationToken = default);
}
