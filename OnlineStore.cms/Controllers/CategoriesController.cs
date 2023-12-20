using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoriesService _CategoriesService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoriesService CategoriesService, IMapper mapper)
        {
            this._CategoriesService = CategoriesService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var (Categories, totalCategoriesCount) = await _CategoriesService.GetPaginatedCategories(searchTerm, pageNumber, pageSize);
                var CategoriesVM = _mapper.Map<IEnumerable<CategoriesViewModel>>(Categories);
                // Convert the list of Categories to an instance of StaticPagedList<CategoriesViewModel>>
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalCategoriesCount / pageSize);

                return View(CategoriesVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Categories");
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> IndexWithoutPagination()
        {
            var Categories = await _CategoriesService.GetCategories();
            var CategoriesVM = _mapper.Map<List<CategoriesViewModel>>(Categories);

            return View(CategoriesVM);
        }

        // Create: Display the form to add a new Categories
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Create: Handle the form submission for adding a new Categories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, CategoriesViewModel CategoriesViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(CategoriesViewModel);
            }

            var Categories = _mapper.Map<CategoriesDTO>(CategoriesViewModel);

            if (file != null && file.Length > 0)
            {
                Categories.Image = Path.GetFileName(file.FileName);
                await _CategoriesService.Create(Categories);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Categories.Image);
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

        //Update: Display the form to edit a Categories
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Categories = await _CategoriesService.GetCategories(id.Value);

            if (Categories == null)
            {
                return NotFound();
            }

            var CategoriesVM = _mapper.Map<CategoriesViewModel>(Categories);
            return View(CategoriesVM);
        }

        //Update: Handle the form submission for editing a Categories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, CategoriesViewModel CategoriesViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(CategoriesViewModel);
            }
            var updatedCategories = _mapper.Map<CategoriesDTO>(CategoriesViewModel);
            await _CategoriesService.Update(Id, updatedCategories);

            return RedirectToAction(nameof(Index));
        }

        //Delete: Display the confirmation page for deleting a Categories
        public async Task<IActionResult> Delete(int? id)
        {

            var Categories = await _CategoriesService.GetCategories(id.Value);
            var CategoriesVM = _mapper.Map<CategoriesViewModel>(Categories);
            return View(CategoriesVM);
        }

        // Delete: Handle the confirmation and delete the Categories
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _CategoriesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
