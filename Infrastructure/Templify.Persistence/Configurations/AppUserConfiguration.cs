using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Templify.Domain.Entities;

namespace Templify.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.FirstName)
            .HasMaxLength(50);
            
        builder.Property(x => x.LastName)
            .HasMaxLength(50);
            
        builder.Property(x => x.ProfileImage)
            .HasMaxLength(500);
            
        builder.Property(x => x.Description)
            .HasMaxLength(1000);
            
        builder.Property(x => x.Role)
            .IsRequired();
            
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
            
        // Связь с IdentityUser
        builder.HasOne(x => x.IdentityUser)
            .WithOne()
            .HasForeignKey<AppUser>(x => x.IdentityId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Индексы
        builder.HasIndex(x => x.UserName)
            .IsUnique();
            
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
