using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Templify.Application.Features.Courses.Queries.GetAllCourses;
using Templify.Application.Features.Courses.Queries.GetCourseById;
using Templify.Application.Interfaces.Services;

namespace Templifyy.Areas.Api.Controllers;

[Area("Api")]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CoursesApiController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesApiController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDetailDto>> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetFeaturedCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        var featuredCourses = courses.Where(c => c.IsFeatured).Take(10);
        return Ok(featuredCourses);
    }
}
