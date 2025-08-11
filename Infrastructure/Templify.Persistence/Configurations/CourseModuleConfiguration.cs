using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Templify.Domain.Entities;

namespace Templify.Persistence.Configurations;

public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
{
    public void Configure(EntityTypeBuilder<CourseModule> builder)
    {
        builder.ToTable("CourseModules");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(x => x.Order)
            .IsRequired();
            
        builder.Property(x => x.Duration)
            .IsRequired();
            
        builder.Property(x => x.IsPublished)
            .IsRequired()
            .HasDefaultValue(false);
            
        // Связи
        builder.HasOne(x => x.Course)
            .WithMany(x => x.Modules)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Индексы
        builder.HasIndex(x => x.CourseId);
        builder.HasIndex(x => x.Order);
        builder.HasIndex(x => x.IsPublished);
    }
}
