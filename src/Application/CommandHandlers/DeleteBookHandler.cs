namespace Bookshelf.Application.CommandHandlers;

using Bookshelf.Application.Commands;
using Bookshelf.Application.Events;
using Bookshelf.Application.Exceptons;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;

internal sealed class DeleteBookHandler : IRequestHandler<DeleteBook>
{
    private readonly ILogger<DeleteBookHandler> logger;
    private readonly IPublisher mediator;
    private readonly IBookRepository repository;

    public DeleteBookHandler(ILogger<DeleteBookHandler> logger, IPublisher mediator, IBookRepository repository)
        => (this.logger, this.mediator, this.repository) = (logger, mediator, repository);

    public async Task Handle(DeleteBook request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);

        var book = await this.repository.ReadAsync(id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException($"{id.Value}");
        }

        await repository.DeleteAsync(id, cancellationToken);
    }
}