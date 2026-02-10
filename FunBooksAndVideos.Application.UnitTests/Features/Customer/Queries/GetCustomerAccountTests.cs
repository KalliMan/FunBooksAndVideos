using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Customer.Queries.GetCustomerAccount;
using FunBooksAndVideos.Application.MappingProfiles;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Customer.Queries;

public class GetCustomerAccountTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ICustomerAccountRepository> _customerAccountRepository;

    public GetCustomerAccountTests()
    {
        _customerAccountRepository = MockCustomerAccountRepository.GetMock();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerAccountProfile>();
        }, NullLoggerFactory.Instance);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_ExistingCustomer_ReturnsCustomerAccount()
    {
        var handler = new GetCustomerAccountHandler(_mapper, _customerAccountRepository.Object);
        var result = await handler.Handle(new GetCustomerAccountQuery(1), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
    }
}
