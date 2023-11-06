namespace Bookshelf.Infrastructure.SqlServer.Services;

using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;

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

    public Task DeleteAsync(BookId id, CancellationToken cancellationToken)
    {
        if (this.dictionary.ContainsKey(id.Value) is false)
        {
            throw new ArgumentException();
        }

        this.dictionary.Remove(id.Value);

        return Task.CompletedTask;
    }

    public async Task<Book?> ReadAsync(BookId id, CancellationToken cancellationToken)
    {
        if (this.dictionary.TryGetValue(id.Value, out var book))
        {
            return book;
        }

        return default;
    }

    public Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        if (this.dictionary.ContainsKey(book.Id.Value) is false)
        {
            throw new ArgumentException();
        }

        this.dictionary[book.Id.Value] = book;

        return Task.CompletedTask;
    }
}
