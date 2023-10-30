namespace Bookshelf.Infrastructure.SqlServer.Services;

using Bookshelf.Infrastructure.SqlServer.Interfaces;
using Entity = Bookshelf.Domain.Entities.Book;
using ViewModel = Bookshelf.Application.ViewModels.Book;

internal sealed class BookReadService : IBookReadService
{
    private readonly IDictionary<Guid, Entity> dictionary;

    public BookReadService(IDictionary<Guid, Entity> dictionary)
        => this.dictionary = dictionary;

    public async Task<IReadOnlyList<ViewModel>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        var result = dictionary.Values
        .Select(book => new ViewModel
        {
            Id = book.Id.Value,
            Authors = book.Authors ?? string.Empty,
            ISBN = book.ISBN?.Value ?? string.Empty,
            Publisher = book.Publisher ?? string.Empty,
            Title = book.Title ?? string.Empty,
        })
        .ToList().AsReadOnly();

        return await Task.FromResult(result);
    }
}
