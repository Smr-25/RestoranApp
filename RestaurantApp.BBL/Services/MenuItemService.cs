using RestaurantApp.BBL.Interfaces;
using RestaurantApp.Core.Models;
using RestaurantApp.DDL.Repostories.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.BBL.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _repository;

        public MenuItemService(IRepository<MenuItem> repository)
        {
            _repository = repository;
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new ArgumentNullException(nameof(menuItem));

            await _repository.AddAsync(menuItem);
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"MenuItem with id {id} not found.");

            await _repository.RemoveAsync(entity);
        }


        public async Task EditMenuItemAsync(int id, MenuItem updatedMenuItem)
        {
            if (updatedMenuItem == null)
                throw new ArgumentNullException(nameof(updatedMenuItem));

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"MenuItem with id {id} not found.");

          
            existing.Name = updatedMenuItem.Name;
            existing.Price = updatedMenuItem.Price;
            existing.CategoryId = updatedMenuItem.CategoryId;

            await _repository.UpdateAsync(existing);
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

        public async Task<List<MenuItem>> SearchMenuItemsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await _repository.Table.Include(m => m.Category).ToListAsync();

            return await _repository.Table
                .Include(m => m.Category)
                .Where(m => m.Name.Contains(searchTerm))
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
