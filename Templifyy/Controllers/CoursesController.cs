using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Templify.Application.Features.Courses.Queries.GetAllCourses;
using Templify.Application.Features.Courses.Queries.GetCourseById;
using Templify.Application.Features.Courses.Commands.CreateCourse;
using Templify.Application.Interfaces.Services;

namespace Templifyy.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return View(courses);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpGet]
    [Authorize(Roles = "Author,Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Author,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCourseCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        // TODO: Implement course creation
        return RedirectToAction(nameof(Index));
    }
}
