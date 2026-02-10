namespace FunBooksAndVideos.Domain.Enums;

/// <summary>
/// Defines the types of memberships available in the system.
/// Types should be defined as a bit field to allow for combinations of membership types if needed in the future.
/// Use enum because this is a system-level constant tightly coupled to business logic. E.g. each type may require different processing logic or benefits.
/// </summary>
[Flags]
public enum MembershipType
{
    None = 0,
    BookClub = 1,
    VideoClub = 2,
    Premium = BookClub | VideoClub
}
