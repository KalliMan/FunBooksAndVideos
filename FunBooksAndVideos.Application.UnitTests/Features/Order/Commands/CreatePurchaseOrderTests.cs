//using FunBooksAndVideos.Application.Contracts.Persistence;
//using FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;
//using FunBooksAndVideos.Application.UnitTests.Mocks;
//using FunBooksAndVideos.Domain.Enums;
//using Moq;

//namespace FunBooksAndVideos.Application.UnitTests.Features.Order.Commands;

//    public class CreatePurchaseOrderTests
//    {
//        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
//        private readonly ICustomerAccountRepository _customerAccountRepository;
//        private readonly IProductRepository _productRepository;

//        public CreatePurchaseOrderTests()
//        {
//            _purchaseOrderRepository = new Mock<IPurchaseOrderRepository>().Object;
//            _customerAccountRepository = MockCustomerAccountRepository.GetMock().Object;
//            _productRepository = MockProductRepository.GetMock().Object;
//        }

//        [Fact]
//        public async Task Handle_ValidRequest_CreatesPurchaseOrder()
//        {
//            var handler = new CreatePurchaseOrderHandler(_purchaseOrderRepository, _customerAccountRepository, _productRepository);
//            var command = new CreatePurchaseOrderCommand
//            {
//                CustomerId = 1,
//                Items = new List<CreatePurchaseOrderItem>
//                {
//                    new CreatePurchaseOrderItem { ProductId = 1, ItemLineType = Domain.Enums.ItemLineType.Book },
//                    new CreatePurchaseOrderItem { ItemLineType = Domain.Enums.ItemLineType.Membership, MembershipType = MembershipType.BookClub }
//                }
//            };

//            var result = await handler.Handle(command, CancellationToken.None);

//            Assert.True(result > 0); // Assuming the repository returns a positive ID for created orders
//        }
//    }
