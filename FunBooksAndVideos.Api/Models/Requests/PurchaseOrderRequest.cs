namespace FunBooksAndVideos.Api.Models.Requests;

public class PurchaseOrderRequest
{
    public int CustomerId { get; set; }

    public IEnumerable<PurchaseOrderItemRequest> Items { get; set; } = new List<PurchaseOrderItemRequest>();
}
