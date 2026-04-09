namespace RestaurantApp.BBL.Interfaces;
using RestaurantApp.BBL.Dtos.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryService
{
    Task<List<CategoryReturnDto>> GetAllAsync();
}
