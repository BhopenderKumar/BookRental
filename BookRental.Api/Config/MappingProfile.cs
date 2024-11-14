using AutoMapper;
using BookRental.Core.DTOs;
using BookRental.Data.Entities;

namespace BookRental.Api.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Rental, RentalDto>();
            CreateMap<User, UserDto>();
        }
    }
}