using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Purchase.Queries.GetPurchaseOrder;

public record GetPurchaseOrderQuery(int Id) : IQuery<PurchaseOrderDto>;
