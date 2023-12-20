using OnlineStore.DataAccess.Data;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interface.IRepositories;

namespace OnlineStore.DataAccess.Repositories
{
    public class CategoriesRepository : BaseRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(OnlineStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
