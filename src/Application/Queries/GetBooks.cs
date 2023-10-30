namespace Bookshelf.Application.Queries;

using Bookshelf.Application.ViewModels;

public sealed record GetBooks : IRequest<IReadOnlyList<Book>>
{
    private const uint DEFAULT_RESPONSE_SIZE = 50;

    public uint Page { get; init; } = default;
    public uint Size { get; init; } = DEFAULT_RESPONSE_SIZE;
}
