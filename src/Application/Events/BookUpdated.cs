namespace Bookshelf.Application.Events;

public sealed record BookUpdated : INotification
{
    public required BookId Id { get; init; }
}
