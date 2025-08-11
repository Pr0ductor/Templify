using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Templify.Domain.Entities;

namespace Templify.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);
            
        builder.Property(x => x.ShortDescription)
            .IsRequired()
            .HasMaxLength(500);
            
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
            
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);
            
        builder.Property(x => x.VideoUrl)
            .HasMaxLength(500);
            
        builder.Property(x => x.Duration)
            .IsRequired();
            
        builder.Property(x => x.Level)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(x => x.IsPublished)
            .IsRequired()
            .HasDefaultValue(false);
            
        builder.Property(x => x.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);
            
        builder.Property(x => x.Rating)
            .IsRequired()
            .HasDefaultValue(0);
            
        builder.Property(x => x.StudentsCount)
            .IsRequired()
            .HasDefaultValue(0);
            
        // Связи
        builder.HasOne(x => x.Author)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Индексы
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.AuthorId);
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.IsPublished);
        builder.HasIndex(x => x.IsFeatured);
    }
}
