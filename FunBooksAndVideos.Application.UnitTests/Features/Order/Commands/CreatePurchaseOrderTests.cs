using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;
using FunBooksAndVideos.Application.Features.Purchase.Processing;
using FunBooksAndVideos.Application.UnitTests.Mocks;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.UnitTests.Features.Order.Commands;

public class CreatePurchaseOrderTests
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly ICustomerAccountRepository _customerAccountRepository;
    private readonly IProductRepository _productRepository;
    private readonly PurchaseOrderProcessor _purchaseOrderProcessor;

    public CreatePurchaseOrderTests()
    {
        _purchaseOrderRepository = MockPurchaseOrderRepository.GetMock().Object;
        _customerAccountRepository = MockCustomerAccountRepository.GetMock().Object;
        _productRepository = MockProductRepository.GetMock().Object;

        _purchaseOrderProcessor = new PurchaseOrderProcessor(
            [
                new ActivateMembershipRule(_customerAccountRepository),
                new GenerateShippingSlipRule()
            ]);
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesPurchaseOrder()
    {
        var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository, _purchaseOrderProcessor);
        var command = new CreatePurchaseOrderCommand
        {
            CustomerId = 1,
            Items = new List<CreatePurchaseOrderItem>
               {
                   new CreatePurchaseOrderItem { ProductId = 1, ItemLineType = ItemLineType.Book }
               }
        };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task Handle_MembershipPurchase_ActivatesMembership()
    {
        var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository, _purchaseOrderProcessor);
        var command = new CreatePurchaseOrderCommand
        {
            CustomerId = 1,
            Items = new List<CreatePurchaseOrderItem>
               {
                   new CreatePurchaseOrderItem { ItemLineType = ItemLineType.Membership, MembershipType = MembershipType.BookClub }
               }
        };

        await handler.Handle(command, CancellationToken.None);

        var customer = await _customerAccountRepository.GetByIdAsync(1);
        Assert.True(customer?.MembershipType.HasFlag(MembershipType.BookClub));
    }

    [Fact]
    public async Task Handle_MultipleMembershipPurchase_ActivatesAllMemberships()
    {
        var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository, _purchaseOrderProcessor);
        var command = new CreatePurchaseOrderCommand
        {
            CustomerId = 1,
            Items = new List<CreatePurchaseOrderItem>
               {
                   new CreatePurchaseOrderItem { ItemLineType = ItemLineType.Membership, MembershipType = MembershipType.BookClub },
                   new CreatePurchaseOrderItem { ItemLineType = ItemLineType.Membership, MembershipType = MembershipType.VideoClub }
               }
        };

        await handler.Handle(command, CancellationToken.None);

        var customer = await _customerAccountRepository.GetByIdAsync(1);
        Assert.NotNull(customer);
        Assert.True(customer.MembershipType.HasFlag(MembershipType.BookClub));
        Assert.True(customer.MembershipType.HasFlag(MembershipType.VideoClub));
        Assert.True(customer.MembershipType.HasFlag(MembershipType.Premium));
    }

    [Fact]
    public async Task Handle_MixedPurchase_ActivatesMembershipAndGeneratesShippingSlip()
    {
        var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository, _purchaseOrderProcessor);
        var command = new CreatePurchaseOrderCommand
        {
            CustomerId = 1,
            Items = new List<CreatePurchaseOrderItem>
               {
                   new CreatePurchaseOrderItem { ProductId = 1, ItemLineType = ItemLineType.Book },
                   new CreatePurchaseOrderItem { ItemLineType = ItemLineType.Membership, MembershipType = MembershipType.BookClub }
               }
        };

        var orderId = await handler.Handle(command, CancellationToken.None);

        var customer = await _customerAccountRepository.GetByIdAsync(1);
        
        Assert.NotNull(customer);
        Assert.True(customer.MembershipType.HasFlag(MembershipType.BookClub));

        var order = await _purchaseOrderRepository.GetByIdAsync(orderId);
        Assert.NotNull(order);
        Assert.NotNull(order.ShippingSlipData);
    }

    [Fact]
    public async Task Handle_PhysicalProductsOnly_GeneratesShippingSlip()
    {
        var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository, _purchaseOrderProcessor);
        var command = new CreatePurchaseOrderCommand
        {
            CustomerId = 1,
            Items = new List<CreatePurchaseOrderItem>
               {
                   new CreatePurchaseOrderItem { ProductId = 1, ItemLineType = ItemLineType.Book },
                   new CreatePurchaseOrderItem { ProductId = 3, ItemLineType = ItemLineType.Video }
               }
        };

        var orderId = await handler.Handle(command, CancellationToken.None);

        var order = await _purchaseOrderRepository.GetByIdAsync(orderId);

        Assert.NotNull(order);
        Assert.NotNull(order.ShippingSlipData);
        Assert.Contains("Book A", order.ShippingSlipData);
    }

    [Fact]
    public async Task Handle_MembershipOnly_NoShippingSlip()
    {
        var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository, _purchaseOrderProcessor);
        var command = new CreatePurchaseOrderCommand
        {
            CustomerId = 2,
            Items = new List<CreatePurchaseOrderItem>
               {
                   new CreatePurchaseOrderItem { ItemLineType = ItemLineType.Membership, MembershipType = MembershipType.VideoClub }
               }
        };

        var orderId = await handler.Handle(command, CancellationToken.None);
        var order = await _purchaseOrderRepository.GetByIdAsync(orderId);

        Assert.NotNull(order);
        Assert.Null(order.ShippingSlipData);
    }
}
