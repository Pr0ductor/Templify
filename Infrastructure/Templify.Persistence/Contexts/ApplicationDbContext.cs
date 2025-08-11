using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Templify.Domain.Entities;
using Templify.Persistence.Configurations;

namespace Templify.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CourseSubscription> CourseSubscriptions { get; set; }
    public DbSet<CourseModule> CourseModules { get; set; }
    public DbSet<CourseLesson> CourseLessons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Применяем конфигурации
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CourseSubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new CourseModuleConfiguration());
        modelBuilder.ApplyConfiguration(new CourseLessonConfiguration());
    }
}
