namespace Bookshelf.Infrastructure.SqlServer.Services;

using System.Data;
using Bookshelf.Infrastructure.Interfaces;
using Bookshelf.Application.ViewModels;
using Microsoft.Data.SqlClient;

internal sealed class BookReadService : IBookReadService
{
    private readonly string connectionString;
    private const string QUERY = "SELECT * FROM dbo.Book";

    public BookReadService(string connectionString)
        => this.connectionString = connectionString;

    public async Task<IReadOnlyList<Book>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = QUERY;

        var result = new List<Book>();

        using var dataReader = await command.ExecuteReaderAsync(cancellationToken);

        while (await dataReader.ReadAsync(cancellationToken))
        {
            var bookId = dataReader.GetGuid(dataReader.GetOrdinal("BookId"));

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

            var book = new Book
            {
                Authors = authors,
                Id = bookId,
                ISBN = isbn,
                Publisher = publisher,
                Title = title,
            };

            result.Add(book);
        }

        return await Task.FromResult(result);
    }
}
