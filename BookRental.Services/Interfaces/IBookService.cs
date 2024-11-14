using BookRental.Core.DTOs;

namespace BookRental.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> SearchBooksAsync(string name, string genre);
        Task<BookDto> GetBookByIdAsync(int bookId);
    }
}
