using Microsoft.EntityFrameworkCore;
using Templify.Application.Interfaces.Repositories;
using Templify.Domain.Entities;
using Templify.Persistence.Contexts;

namespace Templify.Persistence.Repositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<IEnumerable<Course>> GetPublishedCoursesAsync();
    Task<IEnumerable<Course>> GetCoursesByAuthorAsync(int authorId);
    Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId);
    Task<IEnumerable<Course>> GetFeaturedCoursesAsync();
    Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm);
}

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Course>> GetPublishedCoursesAsync()
    {
        return await _dbSet
            .Include(c => c.Author)
            .Include(c => c.Category)
            .Where(c => c.IsPublished)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByAuthorAsync(int authorId)
    {
        return await _dbSet
            .Include(c => c.Category)
            .Where(c => c.AuthorId == authorId && c.IsPublished)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Include(c => c.Author)
            .Where(c => c.CategoryId == categoryId && c.IsPublished)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetFeaturedCoursesAsync()
    {
        return await _dbSet
            .Include(c => c.Author)
            .Include(c => c.Category)
            .Where(c => c.IsFeatured && c.IsPublished)
            .OrderByDescending(c => c.Rating)
            .Take(10)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm)
    {
        return await _dbSet
            .Include(c => c.Author)
            .Include(c => c.Category)
            .Where(c => c.IsPublished && 
                       (c.Title.Contains(searchTerm) || 
                        c.Description.Contains(searchTerm) ||
                        c.ShortDescription.Contains(searchTerm)))
            .OrderByDescending(c => c.Rating)
            .ToListAsync();
    }
}
