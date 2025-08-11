using MediatR;

namespace Templify.Application.Features.Courses.Queries.GetAllCourses;

public class GetAllCoursesQuery : IRequest<IEnumerable<CourseDto>>
{
    public bool? IsPublished { get; set; }
    public bool? IsFeatured { get; set; }
    public int? CategoryId { get; set; }
    public string? SearchTerm { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Duration { get; set; }
    public string Level { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int StudentsCount { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}
