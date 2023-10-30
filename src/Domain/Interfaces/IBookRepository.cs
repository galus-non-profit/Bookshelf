namespace Bookshelf.Domain.Interfaces;

using Bookshelf.Domain.Entities;

public interface IBookRepository
{
    Task CreateAsync(Book entity, CancellationToken cancellationToken = default);
}
