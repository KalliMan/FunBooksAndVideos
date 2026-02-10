using FunBooksAndVideos.Application.Contracts.Persistence;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.DeleteCustomerAccount;

public class DeleteCustomerAccountHandler : IRequestHandler<DeleteCustomerAccountCommand, Unit>
{
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public DeleteCustomerAccountHandler(ICustomerAccountRepository customerAccountRepository)
    {
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task<Unit> Handle(DeleteCustomerAccountCommand request, CancellationToken cancellationToken)
    {
        await _customerAccountRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
