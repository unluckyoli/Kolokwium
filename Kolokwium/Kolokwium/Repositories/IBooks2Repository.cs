using Kolokwium.Models;
using Microsoft.Data.SqlClient;

namespace Kolokwium.Repositories;

public interface IBooks2Repository
{
    Task<bool> DoesBookExist(int id);
    Task<Book> GetBookByGenres(int id);
    Task<int> AddBookWithGenres(string title, List<int> genreIds);
}


