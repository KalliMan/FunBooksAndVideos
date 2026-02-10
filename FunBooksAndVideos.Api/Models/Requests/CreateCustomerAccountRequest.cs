using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Api.Models.Requests;

public record CreateCustomerAccountRequest(
    string Name,
    IEnumerable<MembershipType>? MembershipTypes
);

