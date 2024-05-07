using Kolokwium.Models;
using Kolokwium.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Kolokwium.Controllers;




[ApiController]
[Route("api/books/{id}/genres")]
public class BooksControllers : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public BooksControllers(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksGenres(int id)
    {
        try
        {
            var book = await _booksRepository.GetBookByGenres(id);
            return Ok(book);
        }
        catch (Exception)
        {
            return NotFound($"ksiazka z ID {id} nie znaleziono.");
        }
    }

}


