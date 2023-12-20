using AutoMapper;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.DTO;
using OnlineStore.Domain.Interface.IServices;
using OnlineStore.Domain.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersRepository _UsersRepository;
        private readonly IMapper _mapper;

        public UsersService(UsersRepository UsersRepository, IMapper mapper)
        {
            _UsersRepository = UsersRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersDTO>> GetUsers()
        {
            return _mapper.Map<List<UsersDTO>>(await _UsersRepository.GetAll());
        }

        public async Task<(IEnumerable<UsersDTO>, int)> GetPaginatedUsers(string searchTerm, int pageNumber, int pageSize)
        {
            //Get peginated data
            var Users = await _UsersRepository.GetPaginatedData(pageNumber, pageSize);
            //search

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    Users = Users.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            //}
            //map data with dto
            var UsersDTOs = _mapper.Map<IEnumerable<UsersDTO>>(Users);
            //var mappedData = _UsersViewModelMapper.MapList(paginatedData.Data);            
            var totalUsersCount = await _UsersRepository.GetTotalCount();
            return (UsersDTOs, totalUsersCount);
        }

        public async Task<UsersDTO> GetUsers(int id)
        {
            return _mapper.Map<UsersDTO>(await _UsersRepository.GetById(id));
        }

        public async Task<UsersDTO> Create(UsersDTO UsersDTO)
        {
            var Users = _mapper.Map<Users>(UsersDTO);
            Users.IsDeleted = false;
            await _UsersRepository.Create(Users);

            return _mapper.Map<UsersDTO>(Users);
        }

        public async Task Update(int Id, UsersDTO UsersDTO)
        {
            var existingUsers = await _UsersRepository.GetById(Id);
            _mapper.Map(UsersDTO, existingUsers);
            await _UsersRepository.Update(existingUsers);
        }

        public async Task Delete(int id)
        {
            var entity = await _UsersRepository.GetById(id);
            await _UsersRepository.Delete(entity);
        }
    }
}
