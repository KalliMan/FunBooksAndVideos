using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Queries.GetAllCustomerAccounts;

public record GetAllCustomerAccountsQuery: IQuery<IEnumerable<CustomerAccountDto>>;
