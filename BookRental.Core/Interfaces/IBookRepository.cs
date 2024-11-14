using BookRental.Core.DTOs;

namespace BookRental.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> SearchBooksAsync(string name, string genre);
        Task<BookDto> GetBookByIdAsync(int bookId);
    }
}
