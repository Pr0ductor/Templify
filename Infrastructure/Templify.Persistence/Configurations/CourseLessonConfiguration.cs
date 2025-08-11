using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Templify.Domain.Entities;

namespace Templify.Persistence.Configurations;

public class CourseLessonConfiguration : IEntityTypeConfiguration<CourseLesson>
{
    public void Configure(EntityTypeBuilder<CourseLesson> builder)
    {
        builder.ToTable("CourseLessons");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(10000);
            
        builder.Property(x => x.VideoUrl)
            .HasMaxLength(500);
            
        builder.Property(x => x.FileUrl)
            .HasMaxLength(500);
            
        builder.Property(x => x.Order)
            .IsRequired();
            
        builder.Property(x => x.Duration)
            .IsRequired();
            
        builder.Property(x => x.IsPublished)
            .IsRequired()
            .HasDefaultValue(false);
            
        builder.Property(x => x.IsFree)
            .IsRequired()
            .HasDefaultValue(false);
            
        // Связи
        builder.HasOne(x => x.Module)
            .WithMany(x => x.Lessons)
            .HasForeignKey(x => x.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Индексы
        builder.HasIndex(x => x.ModuleId);
        builder.HasIndex(x => x.Order);
        builder.HasIndex(x => x.IsPublished);
        builder.HasIndex(x => x.IsFree);
    }
}
