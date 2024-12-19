using AutoMapper;
using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"images/{src.ImageUrl}"))
                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        }
    }
}
