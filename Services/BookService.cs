using Books.Exceptions;
using Books.Models;
using Books.Repositories;

namespace Books.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task<Book> CreateNewBook(Book book);
        Task<bool> DeleteBook(int id);
    }

    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }

        public async Task<Book?> GetBookById(int id)
        {
            Book? book = await _bookRepository.GetBookById(id);

            if (book == null)
            {
                throw new NotFoundException($"Could not find book with id {id}.");
            }

            return book;
        }

        public async Task<Book> CreateNewBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new ArgumentException("Author cannot be empty.");
            }

            if (book.PublishedDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentException("Published date cannot be in the future.");
            }

            return await _bookRepository.CreateNewBook(book);
        }


        public async Task<bool> DeleteBook(int id)
        {
            bool succeeded = await _bookRepository.DeleteBook(id);

            if(!succeeded)
            {
                throw new NotFoundException($"Could not find book with id {id}, delete failed.");
            }

            return succeeded;
        }
    }
}
