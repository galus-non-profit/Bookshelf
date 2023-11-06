namespace Bookshelf.Application.Exceptons;

using Bookshelf.Application.Constants;

public sealed class BookNotFoundException : Exception
{
    public string Code { get; private set; }

    public BookNotFoundException(string bookId) : base(bookId)
        => this.Code = ErrorCodes.BOOK_NOT_FOUND;
}
