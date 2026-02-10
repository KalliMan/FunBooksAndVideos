using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Persistence.DatabaseContext;

namespace FunBooksAndVideos.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(FunBooksAndVideosDbContext dbContext) : base(dbContext)
    {
    }
}
