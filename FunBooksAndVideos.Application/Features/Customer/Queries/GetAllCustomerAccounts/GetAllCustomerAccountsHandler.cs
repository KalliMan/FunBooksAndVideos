using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Queries.GetAllCustomerAccounts;

public class GetAllCustomerAccountsHandler : IRequestHandler<GetAllCustomerAccountsQuery, IEnumerable<CustomerAccountDto>>
{
    private readonly IMapper _mapper;
    private readonly ICustomerAccountRepository _customerAccountRepository;

    public GetAllCustomerAccountsHandler(IMapper mapper, ICustomerAccountRepository customerAccountRepository)
    {
        _mapper = mapper;
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task<IEnumerable<CustomerAccountDto>> Handle(GetAllCustomerAccountsQuery request, CancellationToken cancellationToken)
    {
        var customerAccounts = await _customerAccountRepository.GetAllAsync();
        if (customerAccounts == null)
        {
            return Enumerable.Empty<CustomerAccountDto>();
        }
            
        return _mapper.Map<IEnumerable<CustomerAccountDto>>(customerAccounts);
    }
}
