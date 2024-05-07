using Kolokwium.Models;
using Microsoft.Data.SqlClient;
namespace Kolokwium.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly IConfiguration _configuration;
    public BooksRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesBookExist(int id)
    {
        var query = "SELECT 1 FROM Book WHERE ID = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }



    public async Task<Book> GetBookByGenres(int id)
    {
	    var query = @"
        SELECT 
            books.PK AS BookId,
            books.title AS BookTitle,
            genres.PK AS GenresId,
            genres.name AS GenresName
        FROM books
        JOIN books_genres ON books_genres.FK_book = books.PK
        JOIN genres ON genres.PK = books_genres.FK_genre
        WHERE books.PK = @ID";

	    await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
	    await using SqlCommand command = new SqlCommand(query, connection);
    
	    command.Parameters.AddWithValue("@ID", id);

	    await connection.OpenAsync();

	    var reader = await command.ExecuteReaderAsync();

	    Book bookDto = null;

	    while (await reader.ReadAsync())
	    {
		    if (bookDto == null)
		    {
			    bookDto = new Book
			    {
				    PK = reader.GetInt32(reader.GetOrdinal("BookId")),
				    title = reader.GetString(reader.GetOrdinal("BookTitle")),
				    genres = new List<Genres>()
			    };
		    }
		    bookDto.genres.Add(new Genres
		    {
			    PK = reader.GetInt32(reader.GetOrdinal("GenresId")),
			    name = reader.GetString(reader.GetOrdinal("GenresName"))
		    });
	    }

	    if (bookDto == null)
	    {
		    throw new Exception("nie znaleziono");
	    }

	    return bookDto;
    }

}