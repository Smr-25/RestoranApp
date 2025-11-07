using RestaurantApp.BBL.Interfaces;
using RestaurantApp.Core.Models;
using RestaurantApp.DDL.Data;
using RestaurantApp.DDL.Repostories.Intefaces;

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


        
    }
}
