namespace Bookshelf.Application.ViewModels;

public sealed record Book
{
    public required Guid Id { get; init; }
    public required string Authors { get; init; }
    public required string ISBN { get; init; }
    public required string Publisher { get; init; }
    public required string Title { get; init; }
}
