using MediatR;

namespace Templify.Application.Features.Courses.Queries.GetCourseById;

public class GetCourseByIdQuery : IRequest<CourseDetailDto?>
{
    public int Id { get; set; }
}

public class CourseDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public int Duration { get; set; }
    public string Level { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int StudentsCount { get; set; }
    public bool IsPublished { get; set; }
    public bool IsFeatured { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<ModuleDto> Modules { get; set; } = new();
}

public class ModuleDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public int Duration { get; set; }
    public bool IsPublished { get; set; }
    public List<LessonDto> Lessons { get; set; } = new();
}

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string? FileUrl { get; set; }
    public int Order { get; set; }
    public int Duration { get; set; }
    public bool IsPublished { get; set; }
    public bool IsFree { get; set; }
}
