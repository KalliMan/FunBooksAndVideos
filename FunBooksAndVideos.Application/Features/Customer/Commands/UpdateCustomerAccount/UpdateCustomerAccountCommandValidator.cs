using FluentValidation;
using FunBooksAndVideos.Application.Contracts.Persistence;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.UpdateCustomerAccount;

public class UpdateCustomerAccountCommandValidator : AbstractValidator<UpdateCustomerAccountCommand>
{
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public UpdateCustomerAccountCommandValidator(ICustomerAccountRepository customerAccountRepository)
    {
        _customerAccountRepository = customerAccountRepository;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name cannot be empty.")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        RuleFor(x => x)
            .MustAsync(CustomerAccountExists)
            .WithMessage("Customer account does not exist.");
    }

    private async Task<bool> CustomerAccountExists(UpdateCustomerAccountCommand command, CancellationToken token)
        => await _customerAccountRepository.ExistsAsync(command.Id);
}
