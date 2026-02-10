using AutoMapper;
using FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;
using FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.MappingProfiles;

public class PurchaseOrderProfile : Profile
{
    public PurchaseOrderProfile()
    {
        CreateMap<PurchaseOrder, PurchaseOrderDto>();
        CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>();
        CreateMap<CreatePurchaseOrderCommand, PurchaseOrder>()
            .ForMember(dest => dest.Items, opt => opt.Ignore());
    }
}
