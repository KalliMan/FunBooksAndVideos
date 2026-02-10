using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Domain;

public static class MembershipCatalog
{
    public static IReadOnlyList<MembershipInfo> All =
    [
        new MembershipInfo(MembershipType.BookClub, "Book Club Membership", "Access to book club content.", 9.99m),
        new MembershipInfo(MembershipType.VideoClub, "Video Club Membership", "Access to video library.", 14.99m)
    ];
}