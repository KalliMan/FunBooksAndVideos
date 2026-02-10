using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.OrderProduct.Queries.GetAllProducts;
using FunBooksAndVideos.Application.MappingProfiles;
using FunBooksAndVideos.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Features.Product.Queries;

public class GetAllProductsHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly IMapper _mapper;

    public GetAllProductsHandlerTests()
    {
        _productRepositoryMock = MockProductRepository.GetMock();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
        }, NullLoggerFactory.Instance);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_ReturnsAllProducts()
    {
        var handler = new GetAllProductsHandler(_mapper, _productRepositoryMock.Object);
        var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(7, result.ToList().Count); // Assuming 3 products + 3 membership products in the mock
    }

    [Fact]
    public async Task Handle_ReturnsCorrectProductData()
    {
        var handler = new GetAllProductsHandler(_mapper, _productRepositoryMock.Object);
        var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        // Assert
        var products = result.ToList();
        Assert.Contains(products, p => p.Id == 1 && p.Name == "Book A" && p.Description == "Description A" && p.ItemLineType == Domain.Enums.ItemLineType.Book && p.MembershipType == null);
    }
}
