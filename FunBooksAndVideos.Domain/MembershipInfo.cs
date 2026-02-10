using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Domain;

public record MembershipInfo(MembershipType Type, string Name, string Description, decimal Price);
