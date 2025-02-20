using Books.Data;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task<Book> CreateNewBook(Book book);
        Task<bool> DeleteBook(int id);
    }

    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;

        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> CreateNewBook(Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return newBook;
        }

        public async Task<bool> DeleteBook(int id)
        {
            Book? book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
