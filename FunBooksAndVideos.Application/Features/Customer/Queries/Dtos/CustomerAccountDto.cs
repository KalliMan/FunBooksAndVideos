using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;

public class CustomerAccountDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<MembershipType>? MembershipTypes { get; set; }
}
