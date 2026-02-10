using AutoMapper;
using FunBooksAndVideos.Application.Features.OrderProduct.Queries.Dtos;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
    }
}
