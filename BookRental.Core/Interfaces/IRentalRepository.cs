using BookRental.Core.DTOs;
using BookRental.Data.Entities;

namespace BookRental.Core.Interfaces
{
    public interface IRentalRepository
    {
        Task RentBookAsync(Rental rental);
        Task ReturnBookAsync(int rentalId);
        Task UpdateRentalAsync(RentalDto rental);
        Task<IEnumerable<RentalDto>> GetUserRentalHistoryAsync(int userId);
        Task<IEnumerable<RentalDto>> GetOverdueRentalsAsync();
        Task<RentalStatsDto> GetRentalStatsAsync();

    }
}
