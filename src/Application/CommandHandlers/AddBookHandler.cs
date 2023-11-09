namespace Bookshelf.Application.CommandHandlers;

using Bookshelf.Application.Commands;
using Bookshelf.Application.Events;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;

internal sealed class AddBookHandler : IRequestHandler<AddBook>
{
    private readonly ILogger<AddBookHandler> logger;
    private readonly IPublisher mediator;
    private readonly IBookRepository repository;

    public AddBookHandler(ILogger<AddBookHandler> logger, IPublisher mediator, IBookRepository repository)
        => (this.logger, this.mediator, this.repository) = (logger, mediator, repository);

    public async Task Handle(AddBook request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);
        var book = new Book(id, request.Title, request.Authors, request.Publisher, request.Isbn);

        this.logger.LogInformation("Adding {BookId} to repository", request.Id);
        await this.repository.CreateAsync(book, cancellationToken);
        this.logger.LogInformation("Added {BookId} to repository", request.Id);

        var @event = new BookAdded
        {
            Id = id,
        };

        await this.mediator.Publish(@event, cancellationToken);
    }
}
