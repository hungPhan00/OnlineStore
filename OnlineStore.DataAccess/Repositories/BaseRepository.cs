using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Data;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.Interface.IRepositories;

namespace OnlineStore.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly OnlineStoreContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public BaseRepository(OnlineStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPaginatedData(int pageNumber, int pageSize)
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(T));

            if (entityType.FindProperty("IsDelete") != null)
            {
                var query = _dbContext.Set<T>()
                    .Where(item => !EF.Property<bool>(item, "IsDelete"))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking().ToListAsync();

                return await query;
            }
            else
            {
                var query = _dbContext.Set<T>()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking().ToListAsync();

                return await query;
            }
        }

        public async Task<int> GetTotalCount()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<T> GetById<Tid>(Tid id)
        {
            var data = await _dbContext.Set<T>().FindAsync(id);
            if (data == null)
                throw new NotFoundException("No data found");
            return data;
        }

        public async Task<T> Create(T model)
        {
            await _dbContext.Set<T>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task Update(T model)
        {
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task Delete(T? model)
        //{
        //    _dbContext.Set<T>().Remove(model);
        //    await _dbContext.SaveChangesAsync();
        //}
        public async Task Delete(T? model)
        {
            if (model != null)
            {
                // Assuming your entity has an IsDeleted property
                var softDeleteProperty = typeof(T).GetProperty("IsDelete");

                if (softDeleteProperty != null)
                {
                    softDeleteProperty.SetValue(model, true);
                    _dbContext.Entry(model).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    // Handle the case where the entity doesn't have an IsDeleted property
                    throw new InvalidOperationException("Soft deletion is not supported for this entity.");
                }
            }
        }

        //public async Task List<T> GetAllActive()
        //{
        //    return _dbContext.Set<T>()
        //        .Where(e => !(bool)typeof(T).GetProperty("IsDeleted")?.GetValue(e))
        //        .ToList();
        //}

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}