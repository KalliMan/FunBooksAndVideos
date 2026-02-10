using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FunBooksAndVideos.Persistence;

public class UnitOfWork: IUnitOfWork, IAsyncDisposable
{
    private readonly FunBooksAndVideosDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(FunBooksAndVideosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch(DbUpdateException ex)
        {
            throw new PersistenceException(ex.Message, ex);
        }
    }    

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction already started");
        }

        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);    
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await RollbackTransactionAsync(default);
    }
}
