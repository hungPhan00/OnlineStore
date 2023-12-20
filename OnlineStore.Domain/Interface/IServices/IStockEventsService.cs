using OnlineStore.Domain.DTO;

namespace OnlineStore.Domain.Interface.IServices
{
    public interface IStockEventsService
    {
        Task<IEnumerable<StockEventsDTO>> GetStockEvents();
        Task<(IEnumerable<StockEventsDTO>, int)> GetPaginatedStockEvents(string searchTerm, int pageNumber, int pageSize);
        Task<StockEventsDTO> GetStockEvents(int id);
        Task<StockEventsDTO> Create(StockEventsDTO model);
        Task Update(int Id, StockEventsDTO model);
        Task Delete(int id);
    }
}
