namespace FunBooksAndVideos.Domain.Enums;

/// <summary>
/// Defines the types of purchase orders available in the system.
/// Use enum becausethis is a system-level constant tightly coupled to business logic. E.g. each type requires different processing logic.
/// </summary>
public enum ItemLineType
{
    None = 0,
    Book = 1,
    Video = 2,
    Membership = 3
}
