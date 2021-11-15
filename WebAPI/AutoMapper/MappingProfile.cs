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
            CreateMap<User, UserCredentialsDTO>()
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.Password, source => source.MapFrom(source => source.PasswordHash));
            CreateMap<UserCredentialsDTO, User>()
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.PasswordHash, source => source.MapFrom(source => source.Password));
        }
    }
}