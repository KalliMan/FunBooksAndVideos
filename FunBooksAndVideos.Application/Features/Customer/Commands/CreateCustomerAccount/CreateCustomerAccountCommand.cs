using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Domain.Enums;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;

public record CreateCustomerAccountCommand(string Name) : ICommand<int>;
