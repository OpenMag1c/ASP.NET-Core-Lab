using AutoMapper;
using Business.DTO;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Login, source => source.MapFrom(source => source.UserName))
                .ForMember(dest => dest.Password, source => source.MapFrom(source => source.PasswordHash));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.Login))
                .ForMember(dest => dest.PasswordHash, source => source.MapFrom(source => source.Password));
        }
    }
}