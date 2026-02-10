using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.Purchase.Processing;

/// <summary>
/// Rule to activate membership for the customer when a membership item is purchased.
/// </summary>
public class ActivateMembershipRule : IPurchaseOrderRule
{
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public ActivateMembershipRule(ICustomerAccountRepository customerAccountRepository)
    {
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task ApplyAsync(PurchaseOrderProcessingContext context, CancellationToken token)
    {
        var membershipItems = context.MembershipItems;
        if (!membershipItems.Any())
        {
            return;
        }

        var customer = await _customerAccountRepository.GetByIdAsync(context.CustomerId, token);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID {context.CustomerId} not found");
        }

        foreach (var item in membershipItems)
        {
            if (!item.MembershipType.HasValue)
            {
                throw new BadRequestException("Membership type must be specified for membership items");
            }

            customer.ActivateMembership(item.MembershipType.Value);
        }

        _customerAccountRepository.Update(customer);

    }

    public bool IsApplicable(PurchaseOrderProcessingContext context) =>
        context.PurchaseOrder.Items.Any(i => i.ItemLineType == ItemLineType.Membership);
}
