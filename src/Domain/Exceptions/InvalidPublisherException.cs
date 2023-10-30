namespace Bookshelf.Domain.Exceptions;

using Bookshelf.Domain.Constants;

public sealed class InvalidPublisherException : Exception
{
    public string Code { get; private set; }

    public InvalidPublisherException(string publisher) : base(publisher)
    {
        this.Code = ErrorCodes.INVALID_PUBLISHER;
    }
}
