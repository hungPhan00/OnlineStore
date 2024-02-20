using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Interface.IServices
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductsDTO>> GetProducts();

        Task<(IEnumerable<ProductsDTO>, int totalProductCount)> GetPaginatedData(int pageNumber, int pageSize);

        Task<(IEnumerable<ProductsDTO>, int totalProductCount)> GetPaginatedAndSearchData(int pageNumber, int pageSize, string searchTerm);

        Task<ProductsDTO> GetProduct(int id);

        Task<ProductsDTO> Create(ProductsDTO model, int? quantity);

        Task Update(int Id, ProductsDTO model);

        Task Delete(int id);

        //IEnumerable<ProductsDTO> GetAllProducts();
        //ProductsDTO GetById(int id);
        //ProductsDTO AddProduct(ProductsDTO productDto);
        //ProductsDTO UpdateProduct(int productId, ProductsDTO productDto);
        //bool DeleteProduct(int id);
    }
}