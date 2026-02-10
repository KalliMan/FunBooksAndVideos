using FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Purchase.Queries.GetAllPurchaseOrders;

public record GetAllPurchaseOrdersQuery : IRequest<IEnumerable<PurchaseOrderDto>>;
