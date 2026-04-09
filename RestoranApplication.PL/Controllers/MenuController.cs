using Microsoft.AspNetCore.Mvc;
using RestaurantApp.BBL.Interfaces;
using RestaurantApp.BBL.Dtos.MenuItems;
using RestaurantApp.BBL.Exceptions;

namespace RestoranApplication.PL.Controllers;

public class MenuController(IMenuItemService menuService, ICategoryService categoryService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await categoryService.GetAllAsync();
        return View(new MenuItemCreateDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(MenuItemCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            return View(dto);
        }

        try
        {
            await menuService.AddAsync(dto);
            TempData["Success"] = "Menu item added successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (EntityAlreadyExistException ex)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
        catch (EntityNotFoundException ex)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await menuService.GetAllAsync();
        return View(items);
    }
}
