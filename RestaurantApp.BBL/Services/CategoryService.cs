namespace RestaurantApp.BBL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryReturnDto>> GetAllCategoriesAsync()
        {
            var query = await _repository.GetAllAsync();
            var categories = await query.ToListAsync();
            return _mapper.Map<List<CategoryReturnDto>>(categories);
        }

        public async Task<CategoryReturnDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryReturnDto>(category);
        }
        
        public async Task AddCategoryAsync(CategoryCreateDto dto)
        {
            if (await _repository.IsExistAsync(c => c.Name.ToLower() == dto.Name.ToLower()))
            {
                throw new EntityAlreadyExistException($"{dto.Name} adlı kateqoriya artıq mövcuddur.");
            }
            var category = new Category
            {
                Name = dto.Name
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }
        
        public async Task RemoveCategoryAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException($"ID-si {id} olan kateqoriya tapılmadı.");

            await _repository.RemoveAsync(entity);
            await _repository.SaveChangesAsync();
        }
        
        public async Task EditCategoryAsync(CategoryUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new EntityNotFoundException($"ID-si {dto.Id} olan kateqoriya tapılmadı.");
            
            if(await _repository.IsExistAsync(c => c.Name.ToLower() == dto.Name.ToLower() && c.Id != dto.Id))
            {
                throw new EntityAlreadyExistException($"{dto.Name} adlı kateqoriya artıq mövcuddur.");
            }
            entity.Name = dto.Name;
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }
    }
}

