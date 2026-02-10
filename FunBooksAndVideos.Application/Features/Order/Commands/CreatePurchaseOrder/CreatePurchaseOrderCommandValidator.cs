using FluentValidation;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public CreatePurchaseOrderCommandValidator(ICustomerAccountRepository customerAccountRepository)
    {
        _customerAccountRepository = customerAccountRepository;

        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .MustAsync(CustomerAccountAccountExists)
            .WithMessage("Customer account does not exist.");

        RuleFor(x => x.Items)
            .NotNull()
            .NotEmpty()
            .WithMessage("Purchase order must contain at least one item.");

        RuleFor(x => x.Items)
            .NotNull()
            .NotEmpty()
            .Must(ValidItemType)
            .Must(ValidItemMembershipType)
            .Must(ValidItemProduct)
            .WithMessage("Invalid item.");
    }

    private async Task<bool> CustomerAccountAccountExists(int CustomerId, CancellationToken token) =>
        await _customerAccountRepository.ExistsAsync(CustomerId);

    private bool ValidItemType(IEnumerable<CreatePurchaseOrderItem> enumerable) =>
        enumerable.All(e => Enum.IsDefined(typeof(ItemLineType), e.ItemLineType));

    private bool ValidItemMembershipType(IEnumerable<CreatePurchaseOrderItem> enumerable)
    {
        return enumerable.All(e =>
        {
            if (e.ItemLineType != ItemLineType.Membership)
            {
                return true;
            }
            return e.MembershipType.HasValue && Enum.IsDefined(typeof(MembershipType), e.MembershipType);
        });
    }

    private bool ValidItemProduct(IEnumerable<CreatePurchaseOrderItem> enumerable)
    {
        return enumerable.All(e =>
        {
            if (e.ItemLineType != ItemLineType.Membership)
            {
                return e.ProductId.HasValue && e.ProductId > 0;
            }

            return true;
        });
    }
}
