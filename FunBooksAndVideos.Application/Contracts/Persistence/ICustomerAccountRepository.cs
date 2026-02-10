using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence;

public interface ICustomerAccountRepository : IGenericRepository<CustomerAccount>
{
    Task<bool> ExistsAsync(int id);
    Task<bool> IsCustomerAccountUniqueAsync(string name, CancellationToken token);
}
