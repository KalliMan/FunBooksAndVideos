using FunBooksAndVideos.Application.Contracts.Messaging;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.UpdateCustomerAccount;

public record UpdateCustomerAccountCommand(
    int Id,
    string Name
)
: ICommand<Unit>;
