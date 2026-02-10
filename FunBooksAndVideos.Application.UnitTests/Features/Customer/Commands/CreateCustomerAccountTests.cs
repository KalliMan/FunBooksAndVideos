using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;
using FunBooksAndVideos.Application.MappingProfiles;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Customer.Commands;

public class CreateCustomerAccountTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ICustomerAccountRepository> _customerAccountRepository;

    public CreateCustomerAccountTests()
    {
        _customerAccountRepository = MockCustomerAccountRepository.GetMock();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerAccountProfile>();
        }, NullLoggerFactory.Instance);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsNewCustomerId()
    {
        var handler = new CreateCustomerAccountHandler(_mapper, _customerAccountRepository.Object);
        var command = new CreateCustomerAccountCommand("New Customer");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesCustomerInRepository()
    {
        var handler = new CreateCustomerAccountHandler(_mapper, _customerAccountRepository.Object);
        var command = new CreateCustomerAccountCommand("Test Customer");

        var customerId = await handler.Handle(command, CancellationToken.None);
        var createdCustomer = await _customerAccountRepository.Object.GetByIdAsync(customerId);

        Assert.NotNull(createdCustomer);
        Assert.Equal("Test Customer", createdCustomer.Name);
    }
}
