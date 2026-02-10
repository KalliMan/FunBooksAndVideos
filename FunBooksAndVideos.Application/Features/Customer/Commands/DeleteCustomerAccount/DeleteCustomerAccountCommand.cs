using FunBooksAndVideos.Application.Contracts.Messaging;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.DeleteCustomerAccount;

public record DeleteCustomerAccountCommand(int Id) : ICommand<Unit>;
