using BookRental.Core.DTOs;

namespace BookRental.Services.Interfaces
{
    public interface IRentalService
    {
        Task RentBookAsync(int userId, int bookId);
        Task ReturnBookAsync(int rentalId);
        Task<IEnumerable<RentalDto>> GetUserRentalHistoryAsync(int userId);
        Task MarkOverdueRentalsAsync();
        Task SendOverdueNotificationAsync();
        Task<RentalStatsDto> GetRentalStatsAsync();
    }
}
