using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Purchase.Queries.GetAllPurchaseOrders;

public class GetAllPurchaseOrdersHandler : IRequestHandler<GetAllPurchaseOrdersQuery, IEnumerable<PurchaseOrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;

    public GetAllPurchaseOrdersHandler(IMapper mapper, IPurchaseOrderRepository purchaseOrderRepository)
    {
        _mapper = mapper;
        _purchaseOrderRepository = purchaseOrderRepository;
    }

    public async Task<IEnumerable<PurchaseOrderDto>> Handle(GetAllPurchaseOrdersQuery request, CancellationToken cancellationToken)
    {
        var purchaseOrders = await _purchaseOrderRepository.GetAllPurchaseOrdersWithItemsAsync();
        if (purchaseOrders == null)
        {
            return Enumerable.Empty<PurchaseOrderDto>();
        }

        return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
    }
}
