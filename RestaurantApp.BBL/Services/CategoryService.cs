using RestaurantApp.BBL.Exceptions;

namespace RestaurantApp.BBL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var query = await _repository.GetAllAsync();
            return await query.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
        public async Task AddCategoryAsync(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }
        
        public async Task RemoveCategoryAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new CategoryNotFoundException($"Category with id {id} not found.");

            await _repository.RemoveAsync(entity);
            await _repository.SaveChangesAsync();
        }
        
        public async Task EditCategoryAsync(int id, string name)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new CategoryNotFoundException($"Category with id {id} not found.");
            if(await _repository.IsExistAsync(c=>c.Name.ToLower()==name.ToLower() && c.Id!=id))
            {
                throw new CategoryAlreadyExistException($"Category with name {name} already exists.");
            }
            entity.Name = name;
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }
    }
}

