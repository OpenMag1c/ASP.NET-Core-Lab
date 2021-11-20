using AutoMapper;
using Business.DTO;
using DAL.Models;

namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCredentialsDTO>()
                .ForMember(dest => dest.Password, source => source.MapFrom(source => source.PasswordHash));
            CreateMap<UserCredentialsDTO, User>()
                .ForMember(dest => dest.PasswordHash, source => source.MapFrom(source => source.Password));
            CreateMap<ProductInputDTO, Product>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore())
                .ForMember(dest => dest.Background, opt => opt.Ignore());
            CreateMap<Product, ProductOutputDTO>()
                .ForMember(dest => dest.Platform, source => source.MapFrom(source => source.Platform.ToString()))
                .ForMember(dest => dest.Genre, source => source.MapFrom(source => source.Genre.ToString()))
                .ForMember(dest => dest.Rating, source => source.MapFrom(source => $"{(int)source.Rating}+"));
        }
    }
}