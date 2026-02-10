using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Queries.GetAllCustomerAccounts;

public record GetAllCustomerAccountsQuery: IRequest<IEnumerable<CustomerAccountDto>>;
