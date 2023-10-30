namespace Bookshelf.Domain.Exceptions;

using Bookshelf.Domain.Constants;

public sealed class InvalidIsbnException : Exception
{
    public string Code { get; private set; }

    public InvalidIsbnException(string isbn) : base(isbn)
        => this.Code = ErrorCodes.INVALID_ISBN;
}
