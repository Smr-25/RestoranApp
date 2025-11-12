namespace RestaurantApp.BBL.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IRepository<MenuItem> repository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddMenuItemAsync(MenuItemCreateDto dto)
        {
            if (await _repository.IsExistAsync(m => m.Name.ToLower() == dto.Name.ToLower()))
            {
                throw new EntityAlreadyExistException($"{dto.Name} adlı məhsul artıq mövcuddur.");
            }

            if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId))
            {
                throw new EntityNotFoundException($"ID-si {dto.CategoryId} olan kateqoriya tapılmadı.");
            }
            var menuItem = new MenuItem
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };

            await _repository.AddAsync(menuItem);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException($"ID-si {id} olan məhsul tapılmadı.");

            await _repository.RemoveAsync(entity);
            await _repository.SaveChangesAsync();
        }


        public async Task EditMenuItemAsync(MenuItemUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new EntityNotFoundException($"ID-si {dto.Id} olan məhsul tapılmadı.");
            
            if(await _repository.IsExistAsync(m => m.Name.ToLower() == dto.Name.ToLower() && m.Id != dto.Id))
            {
                throw new EntityAlreadyExistException($"{dto.Name} adlı məhsul artıq mövcuddur.");
            }
            
            if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId))
            {
                throw new EntityNotFoundException($"ID-si {dto.CategoryId} olan kateqoriya tapılmadı.");
            }
            
            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.CategoryId = dto.CategoryId;
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<MenuItemReturnDto>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            var query = await _repository.GetAllAsync(
                m => m.CategoryId == categoryId,
                q => q.Include(m => m.Category));

            var menuItems = await query.ToListAsync();
            return _mapper.Map<List<MenuItemReturnDto>>(menuItems);
        }

        public async Task<List<MenuItemReturnDto>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var query = await _repository.GetAllAsync(m => m.Price >= minPrice && m.Price <= maxPrice,
                q => q.Include(m => m.Category));
            var menuItems = await query.ToListAsync();
            return _mapper.Map<List<MenuItemReturnDto>>(menuItems);
        }
        

        public async Task<List<MenuItemReturnDto>> SearchMenuItemsAsync(string search)
        {
           var query = await _repository.GetAllAsync(
                m => m.Name.ToLower().Contains(search.ToLower()),
                q => q.Include(m => m.Category));
           var menuItems = await query.ToListAsync();
           return _mapper.Map<List<MenuItemReturnDto>>(menuItems);
        }

        public async Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync()
        {
            var query = await _repository.GetAllAsync(
               q => q.Include(m => m.Category));
            var menuItems = await query.ToListAsync();
            return _mapper.Map<List<MenuItemReturnDto>>(menuItems);
        }

        public async Task<MenuItemReturnDto?> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _repository.GetByIdAsync(id, q => q.Include(m => m.Category));
            return menuItem == null ? null : _mapper.Map<MenuItemReturnDto>(menuItem);
        }

        
    }
}
