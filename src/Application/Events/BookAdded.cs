namespace Bookshelf.Application.Events;

public sealed record BookAdded : INotification
{
    public required BookId Id { get; init; }
}
