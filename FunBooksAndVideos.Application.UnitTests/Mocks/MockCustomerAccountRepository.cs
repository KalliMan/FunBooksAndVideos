using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using Moq;

internal class MockCustomerAccountRepository
{
    public static Mock<ICustomerAccountRepository> GetMock()
    {
        var mockRepo = new Mock<ICustomerAccountRepository>();

        var customerAccounts = new List<CustomerAccount>
        {
            new CustomerAccount("John Doe") { Id = 1 },
            new CustomerAccount("Jane Smith") { Id = 2 },
            new CustomerAccount("Bob Johnson") { Id = 3 },
        };
        
        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customerAccounts);
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => customerAccounts.FirstOrDefault(c => c.Id == id));
        mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CustomerAccount>()))
            .ReturnsAsync((CustomerAccount customer) =>
            {
                customer.Id = customerAccounts.Max(c => c.Id) + 1;
                customerAccounts.Add(customer);
                return customer;
            });
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .Callback<int>(id =>
            {
                var customer = customerAccounts.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    customerAccounts.Remove(customer);
                }
            })
            .Returns(Task.CompletedTask);
        mockRepo.Setup(repo => repo.IsCustomerAccountUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string name, CancellationToken token) => 
                !customerAccounts.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
        mockRepo.Setup(repo => repo.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => customerAccounts.Any(c => c.Id == id));
        
        return mockRepo;
    }
}