namespace Bookshelf.Application.Events;

public sealed record BookDeleted : INotification
{
    public required BookId Id { get; init; }
}
