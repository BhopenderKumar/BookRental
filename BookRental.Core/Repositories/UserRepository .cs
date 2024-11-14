using AutoMapper;
using BookRental.Core.DTOs;
using BookRental.Core.Interfaces;
using BookRental.Data;

namespace BookRental.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookRentalContext _context;
        private readonly IMapper _mapper;

        public UserRepository(BookRentalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var result = await _context.Users.FindAsync(id);
            return _mapper.Map<UserDto>(result);
        }
    }
}