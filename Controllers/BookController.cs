using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            List<Book>? books = await _bookService.GetAllBooks();

            if (books == null || books.Count == 0)
            {
                return NoContent();
            }

            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            Book? book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateNewBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            newBook = await _bookService.CreateNewBook(newBook);

            return CreatedAtAction(nameof(CreateNewBook), new { newBook });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            bool finished = await _bookService.DeleteBook(id);

            return Ok(finished);
        }
    }
}
