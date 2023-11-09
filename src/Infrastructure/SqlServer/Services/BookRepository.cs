namespace Bookshelf.Infrastructure.SqlServer.Services;

using System.Data;
using Bookshelf.Domain.Entities;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Domain.Types;
using Microsoft.Data.SqlClient;

internal sealed class BookRepository : IBookRepository
{
    private readonly string connectionString;

    public BookRepository(string connectionString)
        => this.connectionString = connectionString;

    public async Task CreateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(this.connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "INSERT INTO dbo.Book (BookId, Title, Authors, Publisher, Isbn) VALUES (@BookId, @Title, @Authors, @Publisher, @Isbn);";

        command.Parameters.Add("@BookId", SqlDbType.UniqueIdentifier).Value = entity.Id.Value;

        if (string.IsNullOrEmpty(entity.Title) is false)
        {
            command.Parameters.Add("@Title", SqlDbType.NVarChar, entity.Title.Length).Value = entity.Title;
        }
        else
        {
            command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = DBNull.Value;
        }

        if (string.IsNullOrEmpty(entity.Authors) is false)
        {
            command.Parameters.Add("@Authors", SqlDbType.NVarChar, entity.Authors.Length).Value = entity.Authors;
        }
        else
        {
            command.Parameters.Add("@Authors", SqlDbType.NVarChar).Value = DBNull.Value;
        }

        if (string.IsNullOrEmpty(entity.Publisher) is false)
        {
            command.Parameters.Add("@Publisher", SqlDbType.NVarChar, entity.Publisher.Length).Value = entity.Publisher;
        }
        else
        {
            command.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = DBNull.Value;
        }

        if (entity.ISBN is not null)
        {
            command.Parameters.Add("@Isbn", SqlDbType.NVarChar, entity.ISBN?.Value.Length ?? 0).Value = entity.Authors;
        }
        else
        {
            command.Parameters.Add("@Isbn", SqlDbType.NVarChar).Value = DBNull.Value;
        }

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public Task DeleteAsync(BookId id, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public async Task<Book?> ReadAsync(BookId id, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult<Book?>(default);
    }

    public Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
