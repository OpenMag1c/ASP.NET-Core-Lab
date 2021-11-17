using AutoMapper;
using Business.DTO;
using DAL.Enum;
using DAL.Models;

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
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.PasswordHash, source => source.MapFrom(source => source.Password));
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.UserName))
                .ForMember(dest => dest.Age, source => source.MapFrom(source => source.Age))
                .ForMember(dest => dest.AddressDelivery, source => source.MapFrom(source => source.AddressDelivery))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(source => source.PhoneNumber));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.UserName))
                .ForMember(dest => dest.Age, source => source.MapFrom(source => source.Age))
                .ForMember(dest => dest.AddressDelivery, source => source.MapFrom(source => source.AddressDelivery))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(source => source.PhoneNumber));
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Name, source => source.MapFrom(source => source.Name))
                .ForMember(dest => dest.Platform, source => source.MapFrom(source => (Platforms)source.Platform));

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(source => source.Name))
                .ForMember(dest => dest.Platform, source => source.MapFrom(source => source.Platform));
        }
    }
}