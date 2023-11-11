namespace Bookshelf.Application.CommandHandlers;

using Bookshelf.Application.Commands;
using Bookshelf.Application.Events;
using Bookshelf.Application.Exceptons;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;
using Microsoft.Extensions.DependencyInjection;

internal sealed class UpdateBookHandler : IRequestHandler<UpdateBook>
{
    private readonly ILogger<UpdateBookHandler> logger;
    private readonly IPublisher mediator;
    private readonly IBookRepository repository;

    public UpdateBookHandler(ILogger<UpdateBookHandler> logger, IPublisher mediator, [FromKeyedServices("SQLServer")] IBookRepository repository)
        => (this.logger, this.mediator, this.repository) = (logger, mediator, repository);

    public async Task Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);

        var book = await this.repository.ReadAsync(id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException($"{id.Value}");
        }

        book.SetTitle(request.Title);

        if (string.IsNullOrWhiteSpace(request.Authors) is false)
        {
            book.SetAuthors(request.Authors);
        }

        if (string.IsNullOrWhiteSpace(request.Publisher) is false)
        {
            book.SetPublisher(request.Publisher);
        }

        if (string.IsNullOrWhiteSpace(request.Isbn) is false)
        {
            var isbn = new ISBN(request.Isbn);
            book.SetIsbn(isbn);
        }

        this.logger.LogInformation("Updating {BookId} in repository", request.Id);
        await repository.UpdateAsync(book, cancellationToken);
        this.logger.LogInformation("Updated {BookId} in repository", request.Id);

        var @event = new BookUpdated
        {
            Id = id,
        };

        await this.mediator.Publish(@event, cancellationToken);
    }
}
