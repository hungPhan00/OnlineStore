using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private IStocksService _stocksService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            (IEnumerable<ProductsDTO> products, int totalProductCount) result;

            if (searchTerm != null)
            {
                result = await _productsService.GetPaginatedAndSearchData(pageNumber, pageSize, searchTerm);
            }
            else
            {
                result = await _productsService.GetPaginatedData(pageNumber, pageSize);
            }

            var productsVM = _mapper.Map<IEnumerable<ProductsViewModel>>(result.products);
            ViewBag.products = productsVM;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.totalProductCount / pageSize);

            return View();
        }

        public IActionResult View(IEnumerable<ProductsViewModel> productsVM, IEnumerable<CategoriesViewModel> categoriesVM)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> IndexWithoutPagination()
        {
            var products = await _productsService.GetProducts();
            var productVM = _mapper.Map<List<ProductsViewModel>>(products);

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsViewModel productsViewModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var product = _mapper.Map<ProductsDTO>(productsViewModel);

            if (file != null && file.Length > 0)
            {
                product.Thumbnail = Path.GetFileName(file.FileName);
                await _productsService.Create(product, productsViewModel.Quantity);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", product.Thumbnail);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Message = "Invalid file";
            }
            return RedirectToAction(nameof(Index));
        }

        //Update: Display the form to edit a product
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetProduct(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            var productVM = _mapper.Map<ProductsViewModel>(product);
            return View(productVM);
        }

        //Update: Handle the form submission for editing a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int Id, ProductsViewModel productsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productsViewModel);
            }

            var updatedProduct = _mapper.Map<ProductsDTO>(productsViewModel);
            if (productsViewModel.Thumbnail != null)
            {
                await _productsService.Update(Id, updatedProduct);
            }
            else if (file != null && file.Length > 0)
            {
                updatedProduct.Thumbnail = Path.GetFileName(file.FileName);
                await _productsService.Update(Id, updatedProduct);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", updatedProduct.Thumbnail);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //Delete: Display the confirmation page for deleting a product
        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _productsService.GetProduct(id.Value);
            var productVM = _mapper.Map<ProductsViewModel>(product);
            return View(productVM);
        }

        // Delete: Handle the confirmation and delete the product
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}