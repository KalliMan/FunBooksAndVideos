using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Purchase.Queries.GetPurchaseOrder;

public class GetPurchaseOrderHandler : IRequestHandler<GetPurchaseOrderQuery, PurchaseOrderDto?>
{
    private readonly IMapper _mapper;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;

    public GetPurchaseOrderHandler(IMapper mapper, IPurchaseOrderRepository purchaseOrderRepository)
    {
        _mapper = mapper;
        _purchaseOrderRepository = purchaseOrderRepository;
    }

    public async Task<PurchaseOrderDto?> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
    {
        var purchaseOrder = await _purchaseOrderRepository.GetPurchaseOrderWithItemsAsync(request.Id);
        if (purchaseOrder == null)
        {
            return null;
        }

        return _mapper.Map<PurchaseOrderDto>(purchaseOrder);
    }
}
