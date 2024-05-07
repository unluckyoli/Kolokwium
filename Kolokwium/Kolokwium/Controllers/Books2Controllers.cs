using Kolokwium.Repositories;


using Kolokwium.Models;
using Kolokwium.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kolokwium.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksControllers2 : ControllerBase
    {
        private readonly IBooks2Repository _booksRepository;

        public BooksControllers2(IBooks2Repository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                if (!await _booksRepository.DoesBookExist(id))
                    return NotFound($"ksiazka o {id} nie istnieje.");

                var book = await _booksRepository.GetBookByGenres(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"blad {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBookWithGenres([FromBody] BookWithGenres bookWithGenres)
        {
            try
            {
                if (bookWithGenres == null || string.IsNullOrEmpty(bookWithGenres.title) || bookWithGenres.PK == null)
                {
                    return BadRequest("blad");
                }

                var bookId = await _booksRepository.AddBookWithGenres(bookWithGenres.title, bookWithGenres.PK);

                return CreatedAtAction(nameof(GetBook), new { id = bookId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"blad {ex.Message}");
            }
        }
    }
}