using OnlineStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interface.IServices
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrdersDTO>> GetOrders();
        Task<(IEnumerable<OrdersDTO>, int)> GetPaginatedOrders(string searchTerm, int pageNumber, int pageSize);
        Task<OrdersDTO> GetOrders(int id);
        Task<OrdersDTO> Create(OrdersDTO model);
        Task Update(int Id, OrdersDTO model);
        Task Delete(int id);
    }
}
