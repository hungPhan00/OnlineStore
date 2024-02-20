using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Controllers
{
    public class OrdersController : Controller
    {
        private IOrdersService _OrdersService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger, IOrdersService OrdersService, IMapper mapper)
        {
            _OrdersService = OrdersService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var (Orders, totalOrdersCount) = await _OrdersService.GetPaginatedOrders(searchTerm, pageNumber, pageSize);
                var OrdersVM = _mapper.Map<IEnumerable<OrdersViewModel>>(Orders);
                // Convert the list of Orders to an instance of StaticPagedList<OrdersViewModel>>
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalOrdersCount / pageSize);

                return View(OrdersVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Orders");
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> IndexWithoutPagination()
        {
            var Orders = await _OrdersService.GetOrders();
            var OrdersVM = _mapper.Map<List<OrdersViewModel>>(Orders);

            return View(OrdersVM);
        }

        // Create: Display the form to add a new Orders
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Create: Handle the form submission for adding a new Orders
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, OrdersViewModel OrdersViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(OrdersViewModel);
            }

            var Orders = _mapper.Map<OrdersDTO>(OrdersViewModel);
            await _OrdersService.Create(Orders);

            return RedirectToAction(nameof(Index));
        }

        //Update: Display the form to edit a Orders
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Orders = await _OrdersService.GetOrders(id.Value);

            if (Orders == null)
            {
                return NotFound();
            }

            var OrdersVM = _mapper.Map<OrdersViewModel>(Orders);
            return View(OrdersVM);
        }

        //Update: Handle the form submission for editing a Orders
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, OrdersViewModel OrdersViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(OrdersViewModel);
            }
            var updatedOrders = _mapper.Map<OrdersDTO>(OrdersViewModel);
            await _OrdersService.Update(Id, updatedOrders);

            return RedirectToAction(nameof(Index));
        }

        //Delete: Display the confirmation page for deleting a Orders
        public async Task<IActionResult> Delete(int? id)
        {
            var Orders = await _OrdersService.GetOrders(id.Value);
            var OrdersVM = _mapper.Map<OrdersViewModel>(Orders);
            return View(OrdersVM);
        }

        // Delete: Handle the confirmation and delete the Orders
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _OrdersService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}