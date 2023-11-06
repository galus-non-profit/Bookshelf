namespace Bookshelf.Application.CommandHandlers;

using Bookshelf.Application.Commands;
using Bookshelf.Application.Events;
using Bookshelf.Application.Exceptons;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;

internal sealed class UpdateBookHandler : IRequestHandler<UpdateBook>
{
    private readonly ILogger<UpdateBookHandler> logger;
    private readonly IPublisher mediator;
    private readonly IBookRepository repository;

    public UpdateBookHandler(ILogger<UpdateBookHandler> logger, IPublisher mediator, IBookRepository repository)
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
        book.SetAuthors(request.Authors);
        book.SetPublisher(request.Publisher);

        var isbn = new ISBN(request.Isbn);
        book.SetIsbn(isbn);

        await repository.UpdateAsync(book, cancellationToken);
    }
}