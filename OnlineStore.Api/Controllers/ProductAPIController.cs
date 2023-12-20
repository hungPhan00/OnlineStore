using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Interface.IServices;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;

namespace OnlineStore.cms.Controllers.APIController
{
    [Route("productapi")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private IProductsService _productsService;
        private readonly IMapper _mapper;
        public ProductAPIController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsService.GetProducts();
            var productVM = _mapper.Map<List<ProductsViewModel>>(products);

            return Ok(productVM);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productsService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            var productVM = _mapper.Map<ProductsViewModel>(product);
            return Ok(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductsViewModel productsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<ProductsDTO>(productsViewModel);
            var createdProductVM = await _productsService.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = createdProductVM.Id }, createdProductVM);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductsViewModel productsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<ProductsDTO>(productsViewModel);
            var updatedProductVM = _productsService.Update(id, product);

            if (updatedProductVM == null)
            {
                return NotFound();
            }

            return  Ok(updatedProductVM);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productsService.Delete(id);

            return NoContent();
        }
    }
}