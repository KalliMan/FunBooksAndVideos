using FunBooksAndVideos.Domain.Common;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Domain.Exceptions;

namespace FunBooksAndVideos.Domain;

public class CustomerAccount: BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public MembershipType MembershipType { get; private set; } = MembershipType.None;

    public CustomerAccount()
    {        
    }

    public CustomerAccount(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CustomerInvariantViolationException("Name cannot be empty");
        }

        Name = name;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CustomerInvariantViolationException("Name cannot be empty");
        }

        Name = name;
    }

    public void ActivateMembership(MembershipType membershipType)
    {
        if (membershipType == MembershipType.None)
        {
            throw new InvalidMembershipException(membershipType, "Cannot activate 'None' membership");
        }

        MembershipType |= membershipType;
    }

    public bool HasMembership(MembershipType type) =>
        MembershipType.HasFlag(type);
    public bool IsPremiumMember() =>
        (MembershipType & MembershipType.Premium) == MembershipType.Premium;
}
