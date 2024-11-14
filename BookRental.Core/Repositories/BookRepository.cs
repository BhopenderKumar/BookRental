using AutoMapper;
using BookRental.Core.DTOs;
using BookRental.Core.Interfaces;
using BookRental.Data;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Core.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookRentalContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookRentalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var result = await _context.Books.ToListAsync();
            return _mapper.Map<IEnumerable<BookDto>>(result);
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string name, string genre)
        {
            var result = await _context.Books
                .Where(b => (string.IsNullOrEmpty(name) || b.Title.Contains(name)) &&
                            (string.IsNullOrEmpty(genre) || b.Genre == genre))
                .ToListAsync();
            return _mapper.Map<IEnumerable<BookDto>>(result);
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            var result = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookDto>(result);
        }
    }
}