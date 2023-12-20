using AutoMapper;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Services
{
    public class StockEventsService : IStockEventsService
    {
        private readonly StockEventsRepository _StockEventsRepository;
        private readonly IMapper _mapper;

        public StockEventsService(StockEventsRepository StockEventsRepository, IMapper mapper)
        {
            _StockEventsRepository = StockEventsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockEventsDTO>> GetStockEvents()
        {
            return _mapper.Map<List<StockEventsDTO>>(await _StockEventsRepository.GetAll());
        }

        public async Task<(IEnumerable<StockEventsDTO>, int)> GetPaginatedStockEvents(string searchTerm, int pageNumber, int pageSize)
        {
            //Get peginated data
            var StockEvents = await _StockEventsRepository.GetPaginatedData(pageNumber, pageSize);
            //search

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    StockEvents = StockEvents.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            //}
            //map data with dto
            var StockEventsDTOs = _mapper.Map<IEnumerable<StockEventsDTO>>(StockEvents);
            //var mappedData = _StockEventsViewModelMapper.MapList(paginatedData.Data);            
            var totalStockEventsCount = await _StockEventsRepository.GetTotalCount();
            return (StockEventsDTOs, totalStockEventsCount);
        }

        public async Task<StockEventsDTO> GetStockEvents(int id)
        {
            return _mapper.Map<StockEventsDTO>(await _StockEventsRepository.GetById(id));
        }

        public async Task<StockEventsDTO> Create(StockEventsDTO StockEventsDTO)
        {
            var StockEvents = _mapper.Map<StockEvents>(StockEventsDTO);
            await _StockEventsRepository.Create(StockEvents);

            return _mapper.Map<StockEventsDTO>(StockEvents);
        }

        public async Task Update(int Id, StockEventsDTO StockEventsDTO)
        {
            var existingStockEvents = await _StockEventsRepository.GetById(Id);
            _mapper.Map(StockEventsDTO, existingStockEvents);
            await _StockEventsRepository.Update(existingStockEvents);
        }

        public async Task Delete(int id)
        {
            var entity = await _StockEventsRepository.GetById(id);
            await _StockEventsRepository.Delete(entity);
        }
    }
}
