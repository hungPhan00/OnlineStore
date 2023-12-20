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
    public class StockEventsRepository : BaseRepository<StockEvents>, IStockEventsRepository
    {
        public StockEventsRepository(OnlineStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
