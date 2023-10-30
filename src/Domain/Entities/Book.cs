namespace Bookshelf.Domain.Entities;

using Bookshelf.Domain.Exceptions;
using Bookshelf.Domain.Types;

public sealed class Book
{
    public Book(BookId id, string title, string? authors = null, string? publisher = null, string? isbn = null)
    {
        this.Id = id;
        this.SetTitle(title);

        if (string.IsNullOrWhiteSpace(authors) is false)
        {
            this.SetAuthors(authors);
        }

        if (string.IsNullOrWhiteSpace(publisher) is false)
        {
            this.SetPublisher(publisher);
        }

        if (string.IsNullOrWhiteSpace(isbn) is false)
        {
            var value = new ISBN(isbn);
            this.SetIsbn(value);
        }
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidTitleException(title);
        }

        this.Title = title;
    }

    public void SetIsbn(ISBN? isbn)
    {
        this.ISBN = isbn;
    }

    public void SetAuthors(string authors)
    {
        if (string.IsNullOrWhiteSpace(authors))
        {
            throw new InvalidTitleException(authors);
        }

        this.Authors = authors;
    }

    public void SetPublisher(string publisher)
    {
        if (string.IsNullOrWhiteSpace(publisher))
        {
            throw new InvalidTitleException(publisher);
        }

        this.Publisher = publisher;
    }

    public BookId Id { get; private set; }
    public string? Title { get; private set; }
    public string? Authors { get; private set; }
    public string? Publisher { get; private set; }
    public ISBN? ISBN { get; private set; }
}
