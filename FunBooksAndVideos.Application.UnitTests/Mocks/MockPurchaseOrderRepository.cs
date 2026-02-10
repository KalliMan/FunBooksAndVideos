using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Domain.Enums;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Mocks;

internal class MockPurchaseOrderRepository
{
    public static Mock<IPurchaseOrderRepository> GetMock()
    {
        var mock = new Mock<IPurchaseOrderRepository>();

        var purchaseOrders = new List<PurchaseOrder>();

        var order1 = new PurchaseOrder(1) { Id = 1 };
        order1.AddItem(new PurchaseOrderItem(ItemLineType.Book, "Dune", 10.99m, 1));
        order1.AddItem(new PurchaseOrderItem(ItemLineType.Video, "Matrix", 15.99m, 3));
        order1.SetShippingSlipData("{\"OrderId\": 1, \"Items\": [\"Dune\", \"Matrix\"]}");
        purchaseOrders.Add(order1);

        var order2 = new PurchaseOrder(2) { Id = 2 };
        order2.AddItem(new PurchaseOrderItem(ItemLineType.Membership, "Book Club Membership", 9.99m, MembershipType.BookClub));
        purchaseOrders.Add(order2);

        var order3 = new PurchaseOrder(1) { Id = 3 };
        order3.AddItem(new PurchaseOrderItem(ItemLineType.Book, "Foundation", 12.99m, 2));
        order3.AddItem(new PurchaseOrderItem(ItemLineType.Membership, "Premium Membership", 19.99m, MembershipType.Premium));
        order3.SetShippingSlipData("{\"OrderId\": 3, \"Items\": [\"Foundation\"]}");
        purchaseOrders.Add(order3);

        mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(purchaseOrders);
        mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => purchaseOrders.FirstOrDefault(p => p.Id == id));
        mock.Setup(repo => repo.GetAllPurchaseOrdersWithItemsAsync())
            .ReturnsAsync(purchaseOrders);
        mock.Setup(repo => repo.GetPurchaseOrderWithItemsAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => purchaseOrders.FirstOrDefault(p => p.Id == id));
        mock.Setup(repo => repo.CreateAsync(It.IsAny<PurchaseOrder>()))
            .ReturnsAsync((PurchaseOrder order) =>
            {
                order.Id = purchaseOrders.Max(p => p.Id) + 1;
                purchaseOrders.Add(order);
                return order;
            });

        return mock;
    }
}