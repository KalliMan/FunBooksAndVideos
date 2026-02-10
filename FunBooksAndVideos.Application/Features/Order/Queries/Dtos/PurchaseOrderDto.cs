namespace FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;

public class PurchaseOrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }

    public IEnumerable<PurchaseOrderItemDto> Items { get; set; } = new List<PurchaseOrderItemDto>();
}
