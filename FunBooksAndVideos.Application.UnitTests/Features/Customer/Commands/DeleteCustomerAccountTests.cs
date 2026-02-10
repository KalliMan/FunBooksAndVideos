using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Customer.Commands.DeleteCustomerAccount;
using MediatR;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Customer.Commands;

public class DeleteCustomerAccountTests
{
    private readonly Mock<ICustomerAccountRepository> _customerAccountRepository;

    public DeleteCustomerAccountTests()
    {
        _customerAccountRepository = MockCustomerAccountRepository.GetMock();
    }

    [Fact]
    public async Task Handle_ExistingCustomer_DeletesSuccessfully()
    {
        var handler = new DeleteCustomerAccountHandler(_customerAccountRepository.Object);
        var command = new DeleteCustomerAccountCommand(1);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task Handle_ExistingCustomer_RemovesFromRepository()
    {
        var handler = new DeleteCustomerAccountHandler(_customerAccountRepository.Object);
        var command = new DeleteCustomerAccountCommand(2);

        await handler.Handle(command, CancellationToken.None);
        var deletedCustomer = await _customerAccountRepository.Object.GetByIdAsync(2);

        Assert.Null(deletedCustomer);
    }
}
