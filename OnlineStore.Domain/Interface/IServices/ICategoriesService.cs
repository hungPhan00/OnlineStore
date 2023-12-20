using OnlineStore.Domain.DTO;

namespace OnlineStore.Domain.Interface.IServices
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoriesDTO>> GetCategories();
        Task<(IEnumerable<CategoriesDTO>, int)> GetPaginatedCategories(string searchTerm, int pageNumber, int pageSize);
        Task<CategoriesDTO> GetCategories(int id);
        Task<CategoriesDTO> Create(CategoriesDTO model);
        Task Update(int Id, CategoriesDTO model);
        Task Delete(int id);
    }
}
