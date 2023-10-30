namespace Bookshelf.Application.CommandHandlers;

using Bookshelf.Application.Commands;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;

internal sealed class AddBookHandler : IRequestHandler<AddBook>
{
    private readonly ILogger<AddBookHandler> logger;
    private readonly IBookRepository repository;

    public AddBookHandler(ILogger<AddBookHandler> logger, IBookRepository repository)
        => (this.logger, this.repository) = (logger, repository);

    public async Task Handle(AddBook request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);
        var book = new Book(id, request.Title, request.Authors, request.Publisher, request.Isbn);

        this.logger.LogInformation("Add {BookId} to repository", request.Id);
        await this.repository.CreateAsync(book, cancellationToken);
    }
}