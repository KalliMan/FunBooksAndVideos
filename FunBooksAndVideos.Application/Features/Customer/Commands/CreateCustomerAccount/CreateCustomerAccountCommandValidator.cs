using FluentValidation;
using FunBooksAndVideos.Application.Contracts.Persistence;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;

public class CreateCustomerAccountCommandValidator: AbstractValidator<CreateCustomerAccountCommand>
{
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public CreateCustomerAccountCommandValidator(ICustomerAccountRepository customerAccountRepository)
    {
        _customerAccountRepository = customerAccountRepository;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name cannot be empty.")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        RuleFor(x => x)
            .MustAsync(CustomerAccountUnique)
            .WithMessage("A customer account with the same name already exists.");
    }

    private async Task<bool> CustomerAccountUnique(CreateCustomerAccountCommand command, CancellationToken token)    
        => await _customerAccountRepository.IsCustomerAccountUniqueAsync(command.Name, token);    
}
