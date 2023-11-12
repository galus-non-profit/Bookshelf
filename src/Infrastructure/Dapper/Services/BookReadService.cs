namespace Bookshelf.Infrastructure.Dapper.Services;

using System.Collections.Generic;
using System.Data;
using Bookshelf.Application.ViewModels;
using Bookshelf.Infrastructure.Interfaces;
using global::Dapper;
using Microsoft.Data.SqlClient;

internal sealed class BookReadService : IBookReadService
{
    private const string QUERY = "SELECT * FROM dbo.Book";

    private readonly ILogger<BookReadService> logger;
    private readonly DapperOptions options;

    public BookReadService(ILogger<BookReadService> logger, DapperOptions options)
        => (this.logger, this.options) = (logger, options);

    public async Task<IReadOnlyList<Book>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        var result = await connection.QueryAsync<Book>(QUERY, commandType: CommandType.Text);

        return result.ToList();
    }
}
