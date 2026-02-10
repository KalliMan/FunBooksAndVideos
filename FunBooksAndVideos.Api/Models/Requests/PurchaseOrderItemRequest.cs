using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Api.Models.Requests;

public record PurchaseOrderItemRequest
{
    public ItemLineType ItemLineType { get; set; }    
    public int? ProductId { get; set; }
    public MembershipType? MembershipType { get; set; }
}
