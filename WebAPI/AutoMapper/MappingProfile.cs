using AutoMapper;
using Business.DTO;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser<int>, UserDto>();
            CreateMap<UserDto, IdentityUser<int>>();
        }
    }
}