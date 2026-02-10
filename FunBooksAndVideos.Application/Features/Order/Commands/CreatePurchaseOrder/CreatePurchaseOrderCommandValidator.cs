using FluentValidation;
using FunBooksAndVideos.Application.Contracts.Persistence;

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
    }

    private async Task<bool> CustomerAccountAccountExists(int CustomerId, CancellationToken token)
        => await _customerAccountRepository.ExistsAsync(CustomerId);
}
