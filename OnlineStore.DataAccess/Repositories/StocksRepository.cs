using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Data;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Repositories
{
    public class StocksRepository : BaseRepository<Stocks>, IStocksRepository
    {
        public StocksRepository(OnlineStoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Stocks>> GetPaginatedAndSearchData(int pageNumber, int pageSize, string searchTerm)
        {
            IQueryable<Stocks> query = _dbContext.Set<Stocks>();

            if (searchTerm != null)
            {
                query = query.Where(p => p.ProductsStocks.Name.ToLower().Contains(searchTerm) || p.ProductsStocks.Categories.Name.ToLower().Contains(searchTerm));
            }

            var paginatedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return paginatedData;
        }
    }
}