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

            CreateMap<CardItem, CartItemDto>()
                      .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.CartItemId))
                      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                      .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                      .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Quantity/ src.Product.Price))
                      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                      .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                      .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"images/{src.Product.ImageUrl}"))
                      .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));


        }
    }
}
