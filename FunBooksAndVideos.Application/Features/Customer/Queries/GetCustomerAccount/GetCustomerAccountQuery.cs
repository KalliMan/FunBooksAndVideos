using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Queries.GetCustomerAccount;

public record GetCustomerAccountQuery(int Id) : IQuery<CustomerAccountDto>;
