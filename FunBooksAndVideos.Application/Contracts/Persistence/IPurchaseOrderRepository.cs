using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence;

public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrder>
{
    Task<IReadOnlyCollection<PurchaseOrder>> GetAllPurchaseOrdersWithItemsAsync();
    Task<PurchaseOrder?> GetPurchaseOrderWithItemsAsync(int id);
}
