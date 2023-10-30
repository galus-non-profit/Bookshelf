using Bookshelf.Domain.Exceptions;

namespace Bookshelf.Domain.Types;

public record struct ISBN
{
    public string Value { get; private set; }

    public ISBN(string value)
    {
        this.Validate(value);
        this.Value = value;
    }

    public ISBN(long value)
    {
        this.Validate(value.ToString());
        this.Value = value.ToString();
    }

    private bool Validate(string value)
    {
        return true;
        throw new InvalidIsbnException(value);
    }
}
