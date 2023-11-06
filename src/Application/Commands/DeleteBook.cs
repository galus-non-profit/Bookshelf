namespace Bookshelf.Application.Commands;

public sealed record DeleteBook : IRequest
{
    public required Guid Id { get; init; }
}
