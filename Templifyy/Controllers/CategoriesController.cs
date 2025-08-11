using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Templify.Application.Features.Categories.Queries.GetAllCategories;
using Templify.Application.Features.Categories.Commands.CreateCategory;
using Templify.Application.Interfaces.Services;

namespace Templifyy.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        // TODO: Implement category creation
        return RedirectToAction(nameof(Index));
    }
}
