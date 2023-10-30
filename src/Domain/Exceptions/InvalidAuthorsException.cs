namespace Bookshelf.Domain.Exceptions;

using Bookshelf.Domain.Constants;

public sealed class InvalidAuthorsException : Exception
{
    public string Code { get; private set; }

    public InvalidAuthorsException(string authors) : base(authors)
        => this.Code = ErrorCodes.INVALID_AUTHORS;
}
