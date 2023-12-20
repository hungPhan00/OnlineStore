using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Controllers
{
    public class UsersController : Controller
    {
        private IUsersService _UsersService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUsersService UsersService, IMapper mapper)
        {
            this._UsersService = UsersService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var (Users, totalUsersCount) = await _UsersService.GetPaginatedUsers(searchTerm, pageNumber, pageSize);
                var UsersVM = _mapper.Map<IEnumerable<UsersViewModel>>(Users);
                // Convert the list of Users to an instance of StaticPagedList<UsersViewModel>>
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalUsersCount / pageSize);

                return View(UsersVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Users");
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> IndexWithoutPagination()
        {
            var Users = await _UsersService.GetUsers();
            var UsersVM = _mapper.Map<List<UsersViewModel>>(Users);

            return View(UsersVM);
        }

        // Create: Display the form to add a new Users
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Create: Handle the form submission for adding a new Users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, UsersViewModel UsersViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(UsersViewModel);
            }

            var Users = _mapper.Map<UsersDTO>(UsersViewModel);
            await _UsersService.Create(Users);
            
            return RedirectToAction(nameof(Index));
        }

        //Update: Display the form to edit a Users
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Users = await _UsersService.GetUsers(id.Value);

            if (Users == null)
            {
                return NotFound();
            }

            var UsersVM = _mapper.Map<UsersViewModel>(Users);
            return View(UsersVM);
        }

        //Update: Handle the form submission for editing a Users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, UsersViewModel UsersViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(UsersViewModel);
            }
            var updatedUsers = _mapper.Map<UsersDTO>(UsersViewModel);
            await _UsersService.Update(Id, updatedUsers);

            return RedirectToAction(nameof(Index));
        }

        //Delete: Display the confirmation page for deleting a Users
        public async Task<IActionResult> Delete(int? id)
        {

            var Users = await _UsersService.GetUsers(id.Value);
            var UsersVM = _mapper.Map<UsersViewModel>(Users);
            return View(UsersVM);
        }

        // Delete: Handle the confirmation and delete the Users
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _UsersService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
