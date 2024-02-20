using AutoMapper;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;
using OnlineStore.Domain.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public class StocksService : IStocksService
    {
        private readonly StocksRepository _StocksRepository;
        private readonly IMapper _mapper;

        public StocksService(StocksRepository StocksRepository, IMapper mapper)
        {
            _StocksRepository = StocksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StocksDTO>> GetStocks()
        {
            return _mapper.Map<List<StocksDTO>>(await _StocksRepository.GetAll());
        }

        public async Task<(IEnumerable<StocksDTO>, int)> GetPaginatedAndSearchData(int pageNumber, int pageSize, string searchTerm)
        {
            //Get peginated data
            var stocks = await _StocksRepository.GetPaginatedAndSearchData(pageNumber, pageSize, searchTerm);

            var stocksDTOs = _mapper.Map<IEnumerable<StocksDTO>>(stocks);

            var totalStocksCount = await _StocksRepository.GetTotalCount();
            return (stocksDTOs, totalStocksCount);
        }

        public async Task<(IEnumerable<StocksDTO>, int)> GetPaginatedStocks(int pageNumber, int pageSize)
        {
            //Get peginated data
            var Stocks = await _StocksRepository.GetPaginatedData(pageNumber, pageSize);
            //search

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    Stocks = Stocks.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            //}
            //map data with dto
            var StocksDTOs = _mapper.Map<IEnumerable<StocksDTO>>(Stocks);
            //var mappedData = _StocksViewModelMapper.MapList(paginatedData.Data);
            var totalStocksCount = await _StocksRepository.GetTotalCount();
            return (StocksDTOs, totalStocksCount);
        }

        public async Task<StocksDTO> GetStocks(int id)
        {
            return _mapper.Map<StocksDTO>(await _StocksRepository.GetById(id));
        }

        public async Task<StocksDTO> Create(StocksDTO StocksDTO)
        {
            var Stocks = _mapper.Map<Stocks>(StocksDTO);

            await _StocksRepository.Create(Stocks);

            return _mapper.Map<StocksDTO>(Stocks);
        }

        public async Task Update(int Id, StocksDTO StocksDTO)
        {
            var existingStocks = await _StocksRepository.GetById(Id);
            _mapper.Map(StocksDTO, existingStocks);
            await _StocksRepository.Update(existingStocks);
        }

        public async Task Delete(int id)
        {
            var entity = await _StocksRepository.GetById(id);
            await _StocksRepository.Delete(entity);
        }
    }
}