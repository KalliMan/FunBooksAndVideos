using FunBooksAndVideos.Application.Features.Product.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Product.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
