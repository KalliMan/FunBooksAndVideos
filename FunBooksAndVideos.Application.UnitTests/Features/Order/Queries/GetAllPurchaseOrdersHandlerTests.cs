using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Purchase.Queries.GetAllPurchaseOrders;
using FunBooksAndVideos.Application.MappingProfiles;
using FunBooksAndVideos.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Order.Queries;

public class GetAllPurchaseOrdersHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPurchaseOrderRepository> _purchaseOrderRepository;

    public GetAllPurchaseOrdersHandlerTests()
    {
        _purchaseOrderRepository = MockPurchaseOrderRepository.GetMock();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PurchaseOrderProfile>();
        }, NullLoggerFactory.Instance);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_ReturnsAllPurchaseOrders()
    {
        var handler = new GetAllPurchaseOrdersHandler(_mapper, _purchaseOrderRepository.Object);
        var query = new GetAllPurchaseOrdersQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }
}
