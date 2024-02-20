using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Controllers
{
    public class StockEventsController : Controller
    {
        private IStockEventsService _StockEventsService;
        private readonly IMapper _mapper;
        private readonly ILogger<StockEventsController> _logger;

        public StockEventsController(ILogger<StockEventsController> logger, IStockEventsService StockEventsService, IMapper mapper)
        {
            _StockEventsService = StockEventsService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var (StockEvents, totalStockEventsCount) = await _StockEventsService.GetPaginatedStockEvents(searchTerm, pageNumber, pageSize);
                var StockEventsVM = _mapper.Map<IEnumerable<StockEventsViewModel>>(StockEvents);
                // Convert the list of StockEvents to an instance of StaticPagedList<StockEventsViewModel>>
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalStockEventsCount / pageSize);

                return View(StockEventsVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving StockEvents");
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> IndexWithoutPagination()
        {
            var StockEvents = await _StockEventsService.GetStockEvents();
            var StockEventsVM = _mapper.Map<List<StockEventsViewModel>>(StockEvents);

            return View(StockEventsVM);
        }

        // Create: Display the form to add a new StockEvents
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Create: Handle the form submission for adding a new StockEvents
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, StockEventsViewModel StockEventsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(StockEventsViewModel);
            }

            var StockEvents = _mapper.Map<StockEventsDTO>(StockEventsViewModel);
            await _StockEventsService.Create(StockEvents);

            return RedirectToAction(nameof(Index));
        }

        //Update: Display the form to edit a StockEvents
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var StockEvents = await _StockEventsService.GetStockEvents(id.Value);

            if (StockEvents == null)
            {
                return NotFound();
            }

            var StockEventsVM = _mapper.Map<StockEventsViewModel>(StockEvents);
            return View(StockEventsVM);
        }

        //Update: Handle the form submission for editing a StockEvents
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, StockEventsViewModel StockEventsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(StockEventsViewModel);
            }
            var updatedStockEvents = _mapper.Map<StockEventsDTO>(StockEventsViewModel);
            await _StockEventsService.Update(Id, updatedStockEvents);

            return RedirectToAction(nameof(Index));
        }

        //Delete: Display the confirmation page for deleting a StockEvents
        public async Task<IActionResult> Delete(int? id)
        {
            var StockEvents = await _StockEventsService.GetStockEvents(id.Value);
            var StockEventsVM = _mapper.Map<StockEventsViewModel>(StockEvents);
            return View(StockEventsVM);
        }

        // Delete: Handle the confirmation and delete the StockEvents
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _StockEventsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}