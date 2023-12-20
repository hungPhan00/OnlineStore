using AutoMapper;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;
using OnlineStore.Domain.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly OrdersRepository _OrdersRepository;
        private readonly IMapper _mapper;

        public OrdersService(OrdersRepository OrdersRepository, IMapper mapper)
        {
            _OrdersRepository = OrdersRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrdersDTO>> GetOrders()
        {
            return _mapper.Map<List<OrdersDTO>>(await _OrdersRepository.GetAll());
        }

        public async Task<(IEnumerable<OrdersDTO>, int)> GetPaginatedOrders(string searchTerm, int pageNumber, int pageSize)
        {
            //Get peginated data
            var Orders = await _OrdersRepository.GetPaginatedData(pageNumber, pageSize);
            //search
            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    Orders = Orders.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            //}

            //map data with dto
            var OrdersDTOs = _mapper.Map<IEnumerable<OrdersDTO>>(Orders);
            //var mappedData = _OrdersViewModelMapper.MapList(paginatedData.Data);            
            var totalOrdersCount = await _OrdersRepository.GetTotalCount();
            return (OrdersDTOs, totalOrdersCount);
        }

        public async Task<OrdersDTO> GetOrders(int id)
        {
            return _mapper.Map<OrdersDTO>(await _OrdersRepository.GetById(id));
        }

        public async Task<OrdersDTO> Create(OrdersDTO OrdersDTO)
        {
            var Orders = _mapper.Map<Orders>(OrdersDTO);
            Orders.IsDeleted = false;
            await _OrdersRepository.Create(Orders);

            return _mapper.Map<OrdersDTO>(Orders);
        }

        public async Task Update(int Id, OrdersDTO OrdersDTO)
        {
            var existingOrders = await _OrdersRepository.GetById(Id);
            _mapper.Map(OrdersDTO, existingOrders);
            await _OrdersRepository.Update(existingOrders);
        }

        public async Task Delete(int id)
        {
            var entity = await _OrdersRepository.GetById(id);
            await _OrdersRepository.Delete(entity);
        }
    }
}
