using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Persistence.DatabaseContext;

public class FunBooksAndVideosDbContext: DbContext
{
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public DbSet<Product> Products { get; set; }

    public FunBooksAndVideosDbContext(DbContextOptions<FunBooksAndVideosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FunBooksAndVideosDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
            }            
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    
}
