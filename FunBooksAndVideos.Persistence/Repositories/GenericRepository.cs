using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected FunBooksAndVideosDbContext DBContext { get; private set; }

        public GenericRepository(FunBooksAndVideosDbContext dbContext)        
            => DBContext = dbContext;        

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
           => await DBContext.Set<T>().ToListAsync();


        public async Task<T?> GetByIdAsync(int id)
            => await DBContext.Set<T>().FindAsync(id);

        public async Task<T> CreateAsync(T entity)
        {
            await DBContext.Set<T>().AddAsync(entity);
            await DBContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            DBContext.Update(entity);
            await DBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DBContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            DBContext.Remove(entity);
            await DBContext.SaveChangesAsync();
        }
    }
}
