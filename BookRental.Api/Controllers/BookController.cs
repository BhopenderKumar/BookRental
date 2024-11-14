using BookRental.Infrastructure.Logging;
using BookRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// GetAllBooks
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                if (books == null || !books.Any())
                {
                    return NotFound(new { message = "No books found." });
                }
                return Ok(books);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while retrieving books.", error = ex.Message });
            }
        }

        /// <summary>
        /// Search for books by name or genre
        /// </summary>
        /// <param name="name"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search(string name, string genre)
        {
            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(genre))
                {
                    return BadRequest(new { message = "At least one search parameter (name or genre) must be provided." });
                }

                var books = await _bookService.SearchBooksAsync(name, genre);
                if (books == null || !books.Any())
                {
                    return NotFound(new { message = "No books found matching the search criteria." });
                }

                return Ok(books);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while searching for books.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get Book by Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int bookId)
        {
            try
            {
                if (bookId <= 0)
                {
                    return BadRequest(new { message = "Invalid bookId." });
                }

                var book = await _bookService.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    return NotFound(new { message = "Book not found." });
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while retrieving the book.", error = ex.Message });
            }
        }

    }
}
 
