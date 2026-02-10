using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.Purchase.Queries.Dtos;

public class PurchaseOrderItemDto
{
    public int Id { get; set; }
    public ItemLineType ItemLineType { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
