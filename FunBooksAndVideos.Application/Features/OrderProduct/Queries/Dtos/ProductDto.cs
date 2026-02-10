using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.OrderProduct.Queries.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemLineType ItemLineType { get; set; }
    public MembershipType? MembershipType { get; set; }
}
