using AutoMapper;
using BookRental.Core.DTOs;
using BookRental.Core.Interfaces;
using BookRental.Data.Entities;
using BookRental.Infrastructure.Email;
using BookRental.Services.Interfaces;

namespace BookRental.Services.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly EmailHelper _emailHelper;

        public RentalService(IRentalRepository rentalRepository, IUserRepository userRepository, IBookRepository bookRepository, IMapper mapper, EmailHelper emailHelper)
        {
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _emailHelper = emailHelper;
        }

        public async Task RentBookAsync(int userId, int bookId)
        {

            var rental = new Rental { BookId = bookId, UserId = userId, RentalDate = DateTime.UtcNow };
            await _rentalRepository.RentBookAsync(rental);
        }

        public async Task ReturnBookAsync(int rentalId)
        {
            await _rentalRepository.ReturnBookAsync(rentalId);
        }

        public async Task<IEnumerable<RentalDto>> GetUserRentalHistoryAsync(int userId)
        {
            var rentals = await _rentalRepository.GetUserRentalHistoryAsync(userId);
            return _mapper.Map<IEnumerable<RentalDto>>(rentals);
        }

        public async Task MarkOverdueRentalsAsync()
        {
            var overdueRentals = await _rentalRepository.GetOverdueRentalsAsync();
            foreach (var rental in overdueRentals)
            {
                rental.IsOverdue = true;
                await _rentalRepository.UpdateRentalAsync(rental);
            }
        }

        public async Task SendOverdueNotificationAsync()
        {
            var overdueRentals = await _rentalRepository.GetOverdueRentalsAsync();
            foreach (var rental in overdueRentals)
            {
                var user = await _userRepository.GetUserByIdAsync(rental.UserId);
                var book = await _bookRepository.GetBookByIdAsync(rental.BookId);
                await NotifyUserAboutOverdueRentalAsync(user.Email, book.Title);
            }
        }

        private async Task NotifyUserAboutOverdueRentalAsync(string userEmail, string bookDetails)
        {
            var subject = "Overdue Rental Notification";
            var body = $"Dear User,\n\nYour rental for the following book is overdue:\n\n{bookDetails}\n\nPlease return it as soon as possible.";

            await _emailHelper.SendEmailAsync(userEmail, subject, body);
        }

        public async Task<RentalStatsDto> GetRentalStatsAsync()
        {
            return await _rentalRepository.GetRentalStatsAsync();
        }
    }
}