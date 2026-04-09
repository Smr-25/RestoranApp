namespace RestaurantApp.BBL.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.BBL.Dtos.MenuItems;
using RestaurantApp.BBL.Exceptions;
using RestaurantApp.BBL.Interfaces;
using RestaurantApp.Core.Models;
using RestaurantApp.DDL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MenuItemService(IRepository<MenuItem> repository, IRepository<Category> categoryRepository, IMapper mapper) : IMenuItemService
{
    public async Task AddAsync(MenuItemCreateDto dto)
    {
        if (await repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim()))
            throw new EntityAlreadyExistException("Item with this name already exists.");
        
        if (!await categoryRepository.IsExistAsync(x => x.Id == dto.CategoryId))
            throw new EntityNotFoundException("Category not found.");
            
        var entity = mapper.Map<MenuItem>(dto);
        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task EditAsync(int id, MenuItemUpdateDto dto)
    {
        var entity = await repository.GetByIdAsync(id, new string[0]);
        if (entity == null)
            throw new EntityNotFoundException("Not found.");
            
        if (entity.Name.ToLower() != dto.Name.ToLower().Trim() && await repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != id))
            throw new EntityAlreadyExistException("Already exists.");
            
        mapper.Map(dto, entity);
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id, new string[0]);
        if (entity == null) throw new EntityNotFoundException("Not found.");
            
        await repository.RemoveAsync(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task<List<MenuItemReturnDto>> GetAllAsync()
    {
        var data = await repository.GetAllAsync(null, "Category").ToListAsync();
        return mapper.Map<List<MenuItemReturnDto>>(data);
    }
    
    public async Task<List<MenuItemReturnDto>> GetByCategoryAsync(int categoryId)
    {
        var data = await repository.GetAllAsync(x => x.CategoryId == categoryId, "Category").ToListAsync();
        return mapper.Map<List<MenuItemReturnDto>>(data);
    }
    
    public async Task<List<MenuItemReturnDto>> GetByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
    {
        var data = await repository.GetAllAsync(x => x.Price >= minPrice && x.Price <= maxPrice, "Category").ToListAsync();
        return mapper.Map<List<MenuItemReturnDto>>(data);
    }
    
    public async Task<List<MenuItemReturnDto>> SearchByNameAsync(string searchText)
    {
        var data = await repository.GetAllAsync(x => x.Name.ToLower().Contains(searchText.ToLower().Trim()), "Category").ToListAsync();
        return mapper.Map<List<MenuItemReturnDto>>(data);
    }
}
