using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Customer.Queries.GetAllCustomerAccounts;
using FunBooksAndVideos.Application.MappingProfiles;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Customer.Queries;

public class GetAllCustomerAccountsTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ICustomerAccountRepository> _customerAccountRepository;

    public GetAllCustomerAccountsTests()
    {
        _customerAccountRepository = MockCustomerAccountRepository.GetMock();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerAccountProfile>();
        }, NullLoggerFactory.Instance);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_ReturnsAllCustomerAccounts()
    {
        var handler = new GetAllCustomerAccountsHandler(_mapper, _customerAccountRepository.Object);
        var result = await handler.Handle(new GetAllCustomerAccountsQuery(), CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(3, result.ToList().Count);
    }
}
