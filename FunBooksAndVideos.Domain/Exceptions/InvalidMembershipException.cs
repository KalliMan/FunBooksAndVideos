using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Domain.Exceptions;

public class InvalidMembershipException : Exception
{
    public MembershipType MembershipType { get; }

    public InvalidMembershipException(MembershipType membershipType, string reason)
        : base($"Invalid membership operation: {reason}")
    {
        MembershipType = membershipType;
    }
}
