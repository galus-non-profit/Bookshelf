namespace Bookshelf.Application.Commands;

public sealed record UpdateBook : IRequest
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Authors { get; init; }
    public string? Publisher { get; init; }
    public string? Isbn { get; init; }
}
