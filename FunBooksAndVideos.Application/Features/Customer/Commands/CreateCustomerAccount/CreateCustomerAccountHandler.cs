using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Domain;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;

public class CreateCustomerAccountHandler : IRequestHandler<CreateCustomerAccountCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public CreateCustomerAccountHandler(IMapper mapper, ICustomerAccountRepository customerAccountRepository)
    {
        _mapper = mapper;
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task<int> Handle(CreateCustomerAccountCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerAccountCommandValidator(_customerAccountRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Customer Account", validationResult);
        }

        var customerAccount = _mapper.Map<CustomerAccount>(request);
        var created = await _customerAccountRepository.CreateAsync(customerAccount);

        return created.Id;
    }
}
