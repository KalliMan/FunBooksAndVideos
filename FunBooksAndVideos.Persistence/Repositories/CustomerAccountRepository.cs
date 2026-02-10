using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Persistence.Repositories;

public class CustomerAccountRepository : GenericRepository<CustomerAccount>,
    ICustomerAccountRepository
{
    public CustomerAccountRepository(FunBooksAndVideosDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> ExistsAsync(int id)    
        => await DBContext.CustomerAccounts.AnyAsync(ca => ca.Id == id);    

    public async Task<bool> IsCustomerAccountUniqueAsync(string name, CancellationToken token)
     => await DBContext.CustomerAccounts.AllAsync(ca => ca.Name != name, token);
}
