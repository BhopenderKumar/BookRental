using AutoMapper;
using BookRental.Core.DTOs;
using BookRental.Core.Interfaces;
using BookRental.Data;
using BookRental.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Core.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly BookRentalContext _context;
        private readonly IMapper _mapper;

        public RentalRepository(BookRentalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task RentBookAsync(Rental rental)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var book = await _context.Books.Where(x => x.Id == rental.BookId).FirstOrDefaultAsync();
                    if (book == null)
                        throw new InvalidOperationException("Book not found.");

                    if (book.AvailableCopies <= 0)
                        throw new InvalidOperationException("Book not available.");

                    // Concurrency handling - decrement the available copies
                    book.AvailableCopies--;
                    await _context.SaveChangesAsync();

                    await _context.Rentals.AddAsync(rental);
                    await _context.SaveChangesAsync();

                     await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while renting the book.", ex);
                }
            }
        }

        public async Task ReturnBookAsync(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.ReturnDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRentalAsync(RentalDto rental)
        {
            var existingRental = await _context.Rentals.FindAsync(rental.Id);
            if (existingRental != null)
            {
                existingRental.BookId = rental.BookId;
                existingRental.UserId = rental.UserId;
                existingRental.ReturnDate = rental.ReturnDate;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RentalDto>> GetUserRentalHistoryAsync(int userId)
        {
            var rentals = await _context.Rentals
                .Where(r => r.UserId == userId)
                .ToListAsync();

            // Using AutoMapper to map the rental entity to rental DTO
            return _mapper.Map<IEnumerable<RentalDto>>(rentals);
        }

        // Get overdue rentals and map to RentalDto
        public async Task<IEnumerable<RentalDto>> GetOverdueRentalsAsync()
        {
            // Query the rentals without using the IsOverdue property
            var overdueRentals = await _context.Rentals
                .Where(r => !r.ReturnDate.HasValue && r.RentalDate.AddDays(14) < DateTime.UtcNow)
                .ToListAsync();

            // Using AutoMapper to map the rental entity to rental DTO
            return _mapper.Map<IEnumerable<RentalDto>>(overdueRentals);
        }


        // Get rental stats
        public async Task<RentalStatsDto> GetRentalStatsAsync()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Book)  // Include the Book entity to prevent lazy loading issues
                .ToListAsync();

            var mostOverdueBook = rentals
                .Where(r => r.IsOverdue)  // Filter overdue rentals in memory
                .GroupBy(r => r.Book)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var mostPopularBook = rentals
                .GroupBy(r => r.Book)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var leastPopularBook = rentals
                .GroupBy(r => r.Book)
                .OrderBy(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return new RentalStatsDto
            {
                MostOverdueBook = _mapper.Map<BookDto>(mostOverdueBook),
                MostPopularBook = _mapper.Map<BookDto>(mostPopularBook),
                LeastPopularBook = _mapper.Map<BookDto>(leastPopularBook)
            };
        }

    }
}