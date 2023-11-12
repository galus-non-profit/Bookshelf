namespace Bookshelf.Infrastructure.Dapper.Services;

using System.Data;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;
using global::Dapper;
using Microsoft.Data.SqlClient;

internal sealed class BookRepository : IBookRepository
{
    private readonly ILogger<BookRepository> logger;
    private readonly DapperOptions options;

    public BookRepository(ILogger<BookRepository> logger, DapperOptions options)
        => (this.logger, this.options) = (logger, options);

    public async Task CreateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "INSERT INTO [dbo].[Book](BookId, Title, Authors, Publisher, Isbn) VALUES (@BookId, @Title, @Authors, @Publisher, @Isbn);";

        var parameters = new DynamicParameters();
        parameters.Add("@Title", value: entity.Title, dbType: DbType.String);
        parameters.Add("@Authors", value: entity.Authors, dbType: DbType.String);
        parameters.Add("@Publisher", value: entity.Publisher, dbType: DbType.String);
        parameters.Add("@Isbn", value: entity.ISBN?.Value, dbType: DbType.String);
        parameters.Add("@BookId", value: entity.Id.Value, dbType: DbType.Guid);

        _ = await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(BookId id, CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "DELETE FROM [dbo].[Book] WHERE [BookId] = @BookId";

        var parameters = new DynamicParameters();
        parameters.Add("@BookId", value: id.Value, dbType: DbType.Guid);

        _ = await connection.ExecuteAsync(query, parameters);
    }

    public async Task<Book?> ReadAsync(BookId id, CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        var query = "SELECT * FROM [dbo].[Book] WHERE [BookId] = @BookId";

        var parameters = new DynamicParameters();
        parameters.Add("@BookId", value: id.Value, dbType: DbType.Guid);

        return await connection.QuerySingleAsync<Book>(query, parameters);
    }

    public async Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "UPDATE [dbo].[Book] SET Title=@Title, Authors=@Authors, Publisher=@Publisher, Isbn=@Isbn WHERE [BookId] = @BookId";

        var parameters = new DynamicParameters();
        parameters.Add("@Title", value: entity.Title, dbType: DbType.String);
        parameters.Add("@Authors", value: entity.Authors, dbType: DbType.String);
        parameters.Add("@Publisher", value: entity.Publisher, dbType: DbType.String);
        parameters.Add("@Isbn", value: entity.ISBN?.Value, dbType: DbType.String);
        parameters.Add("@BookId", value: entity.Id.Value, dbType: DbType.Guid);

        _ = await connection.ExecuteAsync(query, parameters);
    }
}
