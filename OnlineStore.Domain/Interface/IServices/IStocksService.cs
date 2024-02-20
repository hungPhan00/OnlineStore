using OnlineStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interface.IServices
{
    public interface IStocksService
    {
        Task<IEnumerable<StocksDTO>> GetStocks();

        Task<(IEnumerable<StocksDTO>, int)> GetPaginatedStocks(int pageNumber, int pageSize);

        Task<(IEnumerable<StocksDTO>, int totalProductCount)> GetPaginatedAndSearchData(int pageNumber, int pageSize, string searchTerm);

        Task<StocksDTO> GetStocks(int id);

        Task<StocksDTO> Create(StocksDTO model);

        Task Update(int Id, StocksDTO model);

        Task Delete(int id);
    }
}