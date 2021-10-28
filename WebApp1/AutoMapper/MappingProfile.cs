using AutoMapper;
using DAL.DTO;
using DAL.Models;

namespace WebApp1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
