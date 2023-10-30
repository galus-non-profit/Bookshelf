namespace Bookshelf.Domain.Types;

public record struct BookId
{
    public Guid Value { get; init; }

    public BookId(Guid value) => this.Value = value;
}
