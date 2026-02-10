using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using FunBooksAndVideos.Domain;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Queries.GetCustomerAccount;

public class GetCustomerAccountHandler : IRequestHandler<GetCustomerAccountQuery, CustomerAccountDto?>
{
    private readonly IMapper _mapper;
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public GetCustomerAccountHandler(IMapper mapper, ICustomerAccountRepository customerAccountRepository)
    {
        _mapper = mapper;
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task<CustomerAccountDto?> Handle(GetCustomerAccountQuery request, CancellationToken cancellationToken)
    {
        var customerAccount = await _customerAccountRepository.GetByIdAsync(request.Id);
        if (customerAccount == null)
        {
            throw new NotFoundException(nameof(CustomerAccount), request.Id);
        }

        return _mapper.Map<CustomerAccountDto>(customerAccount);
    }
}

