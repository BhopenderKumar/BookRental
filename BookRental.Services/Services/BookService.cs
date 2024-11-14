using BookRental.Core.DTOs;
using BookRental.Core.Interfaces;
using BookRental.Services.Interfaces;

namespace BookRental.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string name, string genre)
        {
            return await _bookRepository.SearchBooksAsync(name, genre);
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.GetBookByIdAsync(bookId);
        }
    }
}