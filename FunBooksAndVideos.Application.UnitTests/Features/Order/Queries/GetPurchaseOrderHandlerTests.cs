using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Purchase.Queries.GetPurchaseOrder;
using FunBooksAndVideos.Application.MappingProfiles;
using FunBooksAndVideos.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Order.Queries;

public class GetPurchaseOrderHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPurchaseOrderRepository> _purchaseOrderRepository;

    public GetPurchaseOrderHandlerTests()
    {
        _purchaseOrderRepository = MockPurchaseOrderRepository.GetMock();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PurchaseOrderProfile>();
        }, NullLoggerFactory.Instance);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_ExistingOrder_ReturnsPurchaseOrder()
    {
        var handler = new GetPurchaseOrderHandler(_mapper, _purchaseOrderRepository.Object);
        var query = new GetPurchaseOrderQuery(1);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.CustomerId);
        Assert.NotEmpty(result.Items);
    }
}
