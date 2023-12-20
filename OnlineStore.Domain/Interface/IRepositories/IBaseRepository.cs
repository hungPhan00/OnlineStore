using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Interface.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetPaginatedData(int pageNumber, int pageSize);
        Task<int> GetTotalCount();
        Task<T> GetById<Tid>(Tid id);
        Task<T> Create(T model);
        Task Update(T model);
        Task Delete(T model);
        Task SaveChangeAsync();
    }
}
