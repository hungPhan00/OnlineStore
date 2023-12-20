using AutoMapper;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interface.IServices;


namespace OnlineStore.BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(CategoriesRepository CategoriesRepository, IMapper mapper)
        {
            _categoriesRepository = CategoriesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriesDTO>> GetCategories()
        {
            return _mapper.Map<List<CategoriesDTO>>(await _categoriesRepository.GetAll());
        }

        public async Task<(IEnumerable<CategoriesDTO>, int)> GetPaginatedCategories(string searchTerm, int pageNumber, int pageSize)
        {
            //Get peginated data
            var Categories = await _categoriesRepository.GetPaginatedData(pageNumber, pageSize);
            //search

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                Categories = Categories.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }
            //map data with dto
            var CategoriesDTOs = _mapper.Map<IEnumerable<CategoriesDTO>>(Categories);
            //var mappedData = _CategoriesViewModelMapper.MapList(paginatedData.Data);            
            var totalCategoriesCount = await _categoriesRepository.GetTotalCount();
            return (CategoriesDTOs, totalCategoriesCount);
        }

        public async Task<CategoriesDTO> GetCategories(int id)
        {
            return _mapper.Map<CategoriesDTO>(await _categoriesRepository.GetById(id));
        }

        public async Task<CategoriesDTO> Create(CategoriesDTO CategoriesDTO)
        {
            var Categories = _mapper.Map<Categories>(CategoriesDTO);
            Categories.IsDelete = false;
            await _categoriesRepository.Create(Categories);

            return _mapper.Map<CategoriesDTO>(Categories);
        }

        public async Task Update(int Id, CategoriesDTO CategoriesDTO)
        {
            var existingCategories = await _categoriesRepository.GetById(Id);
            _mapper.Map(CategoriesDTO, existingCategories);
            await _categoriesRepository.Update(existingCategories);
        }

        public async Task Delete(int id)
        {
            var entity = await _categoriesRepository.GetById(id);
            await _categoriesRepository.Delete(entity);
        }
    }
}
