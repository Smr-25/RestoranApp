namespace RestaurantApp.BBL.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _repository;

        public MenuItemService(IRepository<MenuItem> repository)
        {
            _repository = repository;
        }

        public async Task AddMenuItemAsync(string name,decimal price,int categoryId)
        {
            var menuItem = new MenuItem
            {
                Name = name,
                Price = price,
                CategoryId = categoryId
            };

            await _repository.AddAsync(menuItem);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"MenuItem with id {id} not found.");

            await _repository.RemoveAsync(entity);
            await _repository.SaveChangesAsync();
        }


        public async Task EditMenuItemAsync(int id, string name,decimal price)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"MenuItem with id {id} not found.");
            if(_repository.Table.Any(m => m.Name == name && m.Id == entity.Id))
            {
                throw new InvalidOperationException($"MenuItem with name {name} already exists.");
            }
            entity.Name = name;
            entity.Price = price;
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return await _repository.Table
                .Include(m => m.Category)
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _repository.Table
                .Include(m => m.Category)
                .Where(m => m.Price >= minPrice && m.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task<List<MenuItem>> SearchMenuItemsAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _repository.Table.Include(m => m.Category).ToListAsync();

            return await _repository.Table
                .Include(m => m.Category)
                .Where(m => m.Name.Contains(search))
                .ToListAsync();
        }

        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _repository.Table
                .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _repository.Table
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        
    }
}
