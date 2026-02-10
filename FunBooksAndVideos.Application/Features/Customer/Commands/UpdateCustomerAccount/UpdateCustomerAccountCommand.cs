using FunBooksAndVideos.Domain.Enums;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.UpdateCustomerAccount;

public record UpdateCustomerAccountCommand(
    int Id,
    string Name
)
: IRequest<Unit>;
