using OnlineStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interface.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersDTO>> GetUsers();
        Task<(IEnumerable<UsersDTO>, int)> GetPaginatedUsers(string searchTerm, int pageNumber, int pageSize);
        Task<UsersDTO> GetUsers(int id);
        Task<UsersDTO> Create(UsersDTO model);
        Task Update(int Id, UsersDTO model);
        Task Delete(int id);
    }
}
