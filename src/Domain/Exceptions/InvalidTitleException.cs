namespace Bookshelf.Domain.Exceptions;

using Bookshelf.Domain.Constants;

public sealed class InvalidTitleException : Exception
{
    public string Code { get; private set; }

    public InvalidTitleException(string title) : base(title)
    {
        this.Code = ErrorCodes.INVALID_TITLE;
    }
}
