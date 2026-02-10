using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Mocks;

internal class MockProductRepository
{
    public static Mock<IProductRepository> GetMock()
    {
        var mockRepo = new Mock<IProductRepository>();

        var products = new List<Product>
        {
            new Product ("Book A", "Description A", 10.99m, Domain.Enums.ItemLineType.Book ) { Id = 1 },
            new Product ("Book B", "Description B", 15.99m, Domain.Enums.ItemLineType.Book ) { Id = 2 },
            new Product ("Video A", "Description C", 20.99m, Domain.Enums.ItemLineType.Video ) { Id = 3 },
        };

        var membershipProducts = MembershipCatalog.All.Select((m, index) => 
            new Product(m.Name, m.Description, m.Price, Domain.Enums.ItemLineType.Membership, m.Type) 
            { 
                Id = 4 + index  // Start IDs from 4
            });
        products.AddRange(membershipProducts);

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => products.FirstOrDefault(p => p.Id == id));

        return mockRepo;
    }
}