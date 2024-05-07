using Kolokwium.Controllers;
using Kolokwium.Models;

namespace Kolokwium.Repositories;

public interface IBooksRepository
{

    Task<bool> DoesBookExist(int id);
    Task<Book> GetBookByGenres(int id);
}