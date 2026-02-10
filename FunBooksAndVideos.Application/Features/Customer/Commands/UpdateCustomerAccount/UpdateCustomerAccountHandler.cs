using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.UpdateCustomerAccount;

public class UpdateCustomerAccountHandler : IRequestHandler<UpdateCustomerAccountCommand, Unit>
{
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public UpdateCustomerAccountHandler(ICustomerAccountRepository customerAccountRepository)
    {
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task<Unit> Handle(UpdateCustomerAccountCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCustomerAccountCommandValidator(_customerAccountRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Customer Account", validationResult);
        }

        var customerAccount = await _customerAccountRepository.GetByIdAsync(request.Id);
        
        if (customerAccount == null)
        {
            throw new NotFoundException(nameof(Domain.CustomerAccount), request.Id);
        }

        customerAccount.UpdateName(request.Name);        

        await _customerAccountRepository.UpdateAsync(customerAccount);

        return Unit.Value;
    }
}
