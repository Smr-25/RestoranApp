using RestaurantApp.BBL.Exceptions;

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
                throw new MenuItemNotFoundException($"MenuItem with id {id} not found.");

            await _repository.RemoveAsync(entity);
            await _repository.SaveChangesAsync();
        }


        public async Task EditMenuItemAsync(int id, string name,decimal price)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new MenuItemNotFoundException($"MenuItem with id {id} not found.");
            if(await _repository.IsExistAsync(m=>m.Name.ToLower()==name.ToLower() && m.Id!=id))
            {
                throw new MenuItemAlreadyExistException($"MenuItem with name {name} already exists.");
            }
            entity.Name = name;
            entity.Price = price;
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            var query = await _repository.GetAllAsync(
                m => m.CategoryId == categoryId,
                q => q.Include(m => m.Category));

            return await query.ToListAsync();
               
        }

        public async Task<List<MenuItem>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var query = await _repository.GetAllAsync(m => m.Price >= minPrice && m.Price <= maxPrice,
                q => q.Include(m => m.Category));
            return await query.ToListAsync();
        }
        

        public async Task<List<MenuItem>> SearchMenuItemsAsync(string search)
        {
           var query = await _repository.GetAllAsync(
                m => m.Name.ToLower().Contains(search.ToLower()),
                q => q.Include(m => m.Category));
           return await query.ToListAsync();
           
        }

        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _repository.GetAllAsync().Result.ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id,q => q.Include(m => m.Category));
        }

        
    }
}
