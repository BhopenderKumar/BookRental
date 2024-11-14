using BookRental.Core.DTOs;

namespace BookRental.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByIdAsync(int id);
    }
}
