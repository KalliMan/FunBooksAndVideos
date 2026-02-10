using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Product.Queries.Dtos;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Domain;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        var membershipDtos = MembershipCatalog.All.Select(m => new ProductDto
        {
            Id = (int)m.Type,
            Name = m.Name,
            Description = m.Description,
            Price = m.Price,
            ItemLineType = ItemLineType.Membership,
            MembershipType = m.Type
        });

        return productDtos.Concat(membershipDtos);
    }
}
