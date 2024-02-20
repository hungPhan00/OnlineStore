using AutoMapper;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsRepository _productsRepository;
        private readonly StockEventsRepository _stockEventsRepository;
        private readonly IMapper _mapper;

        public ProductsService(ProductsRepository productsRepository, StockEventsRepository stockEventsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _stockEventsRepository = stockEventsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductsDTO>> GetProducts()
        {
            return _mapper.Map<List<ProductsDTO>>(await _productsRepository.GetAll());
        }

        public async Task<(IEnumerable<ProductsDTO>, int)> GetPaginatedAndSearchData(int pageNumber, int pageSize, string searchTerm)
        {
            //Get peginated data
            var products = await _productsRepository.GetPaginatedAndSearchData(pageNumber, pageSize, searchTerm);

            var productsDTOs = _mapper.Map<IEnumerable<ProductsDTO>>(products);

            var totalProductCount = await _productsRepository.GetTotalCount();
            return (productsDTOs, totalProductCount);
        }

        public async Task<(IEnumerable<ProductsDTO>, int totalProductCount)> GetPaginatedData(int pageNumber, int pageSize)
        {
            //Get peginated data
            var products = await _productsRepository.GetPaginatedData(pageNumber, pageSize);

            var productsDTOs = _mapper.Map<IEnumerable<ProductsDTO>>(products);

            var totalProductCount = await _productsRepository.GetTotalCount();
            return (productsDTOs, totalProductCount);
        }

        public async Task<ProductsDTO> GetProduct(int id)
        {
            return _mapper.Map<ProductsDTO>(await _productsRepository.GetById(id));
        }

        public async Task<ProductsDTO> Create(ProductsDTO productsDTO, int? quantity)
        {
            //Mapping through AutoMapper
            var products = _mapper.Map<Products>(productsDTO);

            //Console.WriteLine(products.ToString());
            if (quantity == null)
            {
                quantity = 0;
            }
            Stocks stocks = new Stocks()
            {
                Quantity = quantity.Value
            };
            StockEvents stockEvent = new StockEvents()
            {
                Quantity = quantity.Value,
                Type = StockEventTypes.IN,
                Stocks = stocks,
                Reason = "Create product",
                CreateAt = DateTime.Now
            };
            products.Stock = stocks;
            products.CreateAt = DateTime.Now;
            await _productsRepository.Create(products);
            await _stockEventsRepository.Create(stockEvent);
            return _mapper.Map<ProductsDTO>(products);
        }

        public async Task Update(int Id, ProductsDTO productsDTO)
        {
            var existingProduct = await _productsRepository.GetById(Id);

            //Manual mapping
            //productmap.Name = productsDTO.Name;
            //productmap.Description = productsDTO.Description;
            //productmap.Thumbnail = productsDTO.Thumbnail;
            //productmap.UnitPrice = productsDTO.UnitPrice;
            //productmap.CreatedBy = productsDTO.CreatedBy;
            //productmap.CreateAt = productsDTO.CreateAt;
            //productmap.CategoryId = productsDTO.CategoryId;
            //productmap.IsDelete = productsDTO.IsDelete;
            _mapper.Map(productsDTO, existingProduct);
            await _productsRepository.Update(existingProduct);
        }

        public async Task Delete(int id)
        {
            var entity = await _productsRepository.GetById(id);
            await _productsRepository.Delete(entity);
        }

        //simple way
        //private readonly ProductsRepository _productsRepository;
        //private readonly IMapper _mapper;
        //public ProductsService(ProductsRepository productsRepository, IMapper mapper)
        //{
        //    _productsRepository = productsRepository;
        //    _mapper = mapper;
        //}

        //public IEnumerable<ProductsDTO> GetAllProducts()
        //{
        //    // Assume GetAll retrieves List<Product>
        //    var products = _productsRepository.GetAllProduct();
        //    return _mapper.Map<List<ProductsDTO>>(products);
        //}

        //public ProductsDTO GetById(int id)
        //{
        //    var product = _productsRepository.GetById(id);
        //    return _mapper.Map<ProductsDTO>(product);
        //}

        //public ProductsDTO AddProduct(ProductsDTO productDto)
        //{
        //    var product = _mapper.Map<Products>(productDto);
        //    _productsRepository.AddProduct(product);
        //    // You might want to return the created entity or DTO with the updated ID.
        //    return _mapper.Map<ProductsDTO>(product);
        //}

        //public ProductsDTO UpdateProduct(int Id, ProductsDTO productDto)
        //{
        //    var existingProduct = _productsRepository.GetById(Id);
        //    if (existingProduct == null)
        //    {
        //        // Handle not found scenario
        //        return null;
        //    }

        //    // Update existingProduct with values from productDto
        //    _mapper.Map(productDto, existingProduct);
        //    // Save changes
        //    _productsRepository.UpdateProduct(existingProduct);
        //    // Return updated DTO
        //    return _mapper.Map<ProductsDTO>(existingProduct);
        //}

        //public bool DeleteProduct(int id)
        //{
        //    var existingProduct = _productsRepository.GetById(id);
        //    if (existingProduct == null)
        //    {
        //        // Handle not found scenario
        //        return false;
        //    }

        //    _productsRepository.DeleteProduct(id);
        //    return true;
        //}
    }
}