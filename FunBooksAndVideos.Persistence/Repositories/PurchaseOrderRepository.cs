using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Persistence.Repositories;

public class PurchaseOrderRepository : GenericRepository<PurchaseOrder>,
    IPurchaseOrderRepository
{
    public PurchaseOrderRepository(FunBooksAndVideosDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<PurchaseOrder>> GetAllPurchaseOrdersWithItemsAsync() =>
        await DBContext.PurchaseOrders
            .Include(p => p.Items)
            .ToListAsync();

    public async Task<PurchaseOrder?> GetPurchaseOrderWithItemsAsync(int id) =>
        await DBContext.PurchaseOrders
            .Include(p => p.Items)
            .FirstOrDefaultAsync(p => p.Id == id);
}
