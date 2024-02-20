using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Controllers
{
    public class StocksController : Controller
    {
        private IStocksService _StocksService;
        private readonly IMapper _mapper;
        private readonly ILogger<StocksController> _logger;

        public StocksController(ILogger<StocksController> logger, IStocksService StocksService, IMapper mapper)
        {
            _StocksService = StocksService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            (IEnumerable<StocksDTO> stocks, int totalProductCount) result;

            if (searchTerm != null)
            {
                result = await _StocksService.GetPaginatedAndSearchData(pageNumber, pageSize, searchTerm);
            }
            else
            {
                result = await _StocksService.GetPaginatedStocks(pageNumber, pageSize);
            }

            var stocksVM = _mapper.Map<IEnumerable<StocksViewModel>>(result.stocks);
            ViewBag.stocks = stocksVM;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.totalProductCount / pageSize);

            return View();
        }

        public async Task<IActionResult> IndexWithoutPagination()
        {
            var Stocks = await _StocksService.GetStocks();
            var StocksVM = _mapper.Map<List<StocksViewModel>>(Stocks);

            return View(StocksVM);
        }

        // Create: Display the form to add a new Stocks
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Create: Handle the form submission for adding a new Stocks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, StocksViewModel StocksViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(StocksViewModel);
            }

            var Stocks = _mapper.Map<StocksDTO>(StocksViewModel);
            await _StocksService.Create(Stocks);

            return RedirectToAction(nameof(Index));
        }

        //Update: Display the form to edit a Stocks
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Stocks = await _StocksService.GetStocks(id.Value);

            if (Stocks == null)
            {
                return NotFound();
            }

            var StocksVM = _mapper.Map<StocksViewModel>(Stocks);
            return View(StocksVM);
        }

        //Update: Handle the form submission for editing a Stocks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, StocksViewModel StocksViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(StocksViewModel);
            }
            var updatedStocks = _mapper.Map<StocksDTO>(StocksViewModel);
            await _StocksService.Update(Id, updatedStocks);

            return RedirectToAction(nameof(Index));
        }

        //Delete: Display the confirmation page for deleting a Stocks
        public async Task<IActionResult> Delete(int? id)
        {
            var Stocks = await _StocksService.GetStocks(id.Value);
            var StocksVM = _mapper.Map<StocksViewModel>(Stocks);
            return View(StocksVM);
        }

        // Delete: Handle the confirmation and delete the Stocks
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _StocksService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}