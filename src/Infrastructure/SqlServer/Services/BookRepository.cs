namespace Bookshelf.Infrastructure.SqlServer.Services;

using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;

internal sealed class BookRepository : IBookRepository
{
    private readonly IDictionary<Guid, Book> dictionary;
    public BookRepository(IDictionary<Guid, Book> dictionary)
        => this.dictionary = dictionary;

    public Task CreateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        this.dictionary.Add(entity.Id.Value, entity);

        return Task.CompletedTask;
    }
}
