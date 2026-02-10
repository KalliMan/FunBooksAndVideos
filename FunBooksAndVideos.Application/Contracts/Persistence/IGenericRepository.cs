namespace FunBooksAndVideos.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<T> CreateAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);
}
