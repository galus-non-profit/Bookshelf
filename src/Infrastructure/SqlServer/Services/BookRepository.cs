namespace Bookshelf.Infrastructure.SqlServer.Services;

using System.Data;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;
using Microsoft.Data.SqlClient;

internal sealed class BookRepository : IBookRepository
{
    private readonly ILogger<BookRepository> logger;
    private readonly SqlServerOptions options;

    public BookRepository(ILogger<BookRepository> logger, SqlServerOptions options)
        => (this.logger, this.options) = (logger, options);

    public async Task CreateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "INSERT INTO dbo.Book (BookId, Title, Authors, Publisher, Isbn) VALUES (@BookId, @Title, @Authors, @Publisher, @Isbn);";

        command.Parameters.Add("@BookId", SqlDbType.UniqueIdentifier).Value = entity.Id.Value;

        if (string.IsNullOrEmpty(entity.Title))
        {
            command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            command.Parameters.Add("@Title", SqlDbType.NVarChar, entity.Title.Length).Value = entity.Title;
        }

        if (string.IsNullOrEmpty(entity.Authors))
        {
            command.Parameters.Add("@Authors", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            command.Parameters.Add("@Authors", SqlDbType.NVarChar, entity.Authors.Length).Value = entity.Authors;
        }

        if (string.IsNullOrEmpty(entity.Publisher))
        {
            command.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            command.Parameters.Add("@Publisher", SqlDbType.NVarChar, entity.Publisher.Length).Value = entity.Publisher;
        }

        if (entity.ISBN is null)
        {
            command.Parameters.Add("@Isbn", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            string value = $"{entity.ISBN?.Value ?? string.Empty}";
            command.Parameters.Add("@Isbn", SqlDbType.VarChar, value.Length).Value = value;
        }

        _ = await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(BookId id, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "DELETE FROM dbo.Book WHERE BookId = @BookId;";

        command.Parameters.Add("@BookId", SqlDbType.UniqueIdentifier).Value = id.Value;

        _ = await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<Book?> ReadAsync(BookId id, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT * FROM dbo.Book WHERE BookId = @BookId;";

        command.Parameters.Add("@BookId", SqlDbType.UniqueIdentifier).Value = id.Value;

        await using var dataReader = await command.ExecuteReaderAsync(cancellationToken);

        if (await dataReader.ReadAsync(cancellationToken) is true)
        {
            var title = dataReader.IsDBNull(dataReader.GetOrdinal("Title"))
                ? string.Empty
                : dataReader.GetString(dataReader.GetOrdinal("Title"));

            var authors = dataReader.IsDBNull(dataReader.GetOrdinal("Authors"))
                ? string.Empty
                : dataReader.GetString(dataReader.GetOrdinal("Authors"));

            var publisher = dataReader.IsDBNull(dataReader.GetOrdinal("Publisher"))
                ? string.Empty
                : dataReader.GetString(dataReader.GetOrdinal("Publisher"));

            var isbn = dataReader.IsDBNull(dataReader.GetOrdinal("Isbn"))
                ? string.Empty
                : dataReader.GetString(dataReader.GetOrdinal("Isbn"));

            var book = new Book(id, title, authors, publisher, isbn);

            return await Task.FromResult(book);
        }

        return await Task.FromResult<Book?>(null);
    }

    public async Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(this.options.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "UPDATE dbo.Book SET Title = @Title, Authors = @Authors, Publisher = @Publisher, Isbn = @Isbn WHERE BookId = @BookId;";

        command.Parameters.Add("@BookId", SqlDbType.UniqueIdentifier).Value = entity.Id.Value;

        if (string.IsNullOrEmpty(entity.Title))
        {
            command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            command.Parameters.Add("@Title", SqlDbType.NVarChar, entity.Title.Length).Value = entity.Title;
        }

        if (string.IsNullOrEmpty(entity.Authors))
        {
            command.Parameters.Add("@Authors", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            command.Parameters.Add("@Authors", SqlDbType.NVarChar, entity.Authors.Length).Value = entity.Authors;
        }

        if (string.IsNullOrEmpty(entity.Publisher))
        {
            command.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            command.Parameters.Add("@Publisher", SqlDbType.NVarChar, entity.Publisher.Length).Value = entity.Publisher;
        }

        if (entity.ISBN is null)
        {
            command.Parameters.Add("@Isbn", SqlDbType.VarChar).Value = DBNull.Value;
        }
        else
        {
            string value = $"{entity.ISBN?.Value ?? string.Empty}";
            command.Parameters.Add("@Isbn", SqlDbType.VarChar, value.Length).Value = value;
        }

        _ = await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
