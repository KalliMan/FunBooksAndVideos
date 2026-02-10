using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Application.Features.OrderProduct.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.OrderProduct.Queries.GetAllProducts;

public record GetAllProductsQuery : ICommand<IEnumerable<ProductDto>>;
