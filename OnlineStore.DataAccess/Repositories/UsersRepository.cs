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
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(OnlineStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
