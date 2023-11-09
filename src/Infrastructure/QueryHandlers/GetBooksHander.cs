namespace Bookshelf.Infrastructure.QueryHandlers;

using Bookshelf.Application.Queries;
using Bookshelf.Application.ViewModels;
using Bookshelf.Infrastructure.Interfaces;

internal sealed class GetBooksHandler : IRequestHandler<GetBooks, IReadOnlyList<Book>>
{
    private readonly IBookReadService readService;

    public GetBooksHandler(IBookReadService readService)
        => this.readService = readService;

    public async Task<IReadOnlyList<Book>> Handle(GetBooks request, CancellationToken cancellationToken)
    {
        return await this.readService.GetAllBooks(cancellationToken);
    }
}
