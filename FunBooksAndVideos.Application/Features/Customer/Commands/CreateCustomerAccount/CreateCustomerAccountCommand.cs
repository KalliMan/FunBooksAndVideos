using FunBooksAndVideos.Domain.Enums;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;

public record CreateCustomerAccountCommand(string Name) : IRequest<int>;
