using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Data;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interface.IRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineStore.DataAccess.Repositories
{
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository(OnlineStoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Products>> GetPaginatedAndSearchData(int pageNumber, int pageSize, string searchTerm)
        {
            IQueryable<Products> query = _dbContext.Set<Products>();

            if (searchTerm != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm) || p.Categories.Name.ToLower().Contains(searchTerm));
            }

            var paginatedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return paginatedData;
        }

        //cách này tốn công khi mở rộng projects (DRY)
        //private readonly OnlineStoreContext _context;

        //public ProductsRepository(OnlineStoreContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Products> GetAllProduct()
        //{
        //    return _context.Products.ToList();
        //}

        //public Products GetById(int productId)
        //{
        //    return _context.Products.Find(productId);
        //}

        //public void AddProduct(Products products)
        //{
        //    _context.Products.Add(products);
        //    _context.SaveChanges();
        //}

        //public void UpdateProduct(Products products)
        //{
        //    _context.Entry(products).State = EntityState.Modified;
        //    _context.SaveChanges();
        //}

        //public void DeleteProduct(int id)
        //{
        //    var products = _context.Products.Find(id);
        //    if (products != null)
        //    {
        //        _context.Products.Remove(products);
        //        _context.SaveChanges();
        //    }
        //}
    }
}