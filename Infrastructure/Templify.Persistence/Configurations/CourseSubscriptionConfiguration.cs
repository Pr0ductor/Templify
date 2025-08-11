using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Templify.Domain.Entities;

namespace Templify.Persistence.Configurations;

public class CourseSubscriptionConfiguration : IEntityTypeConfiguration<CourseSubscription>
{
    public void Configure(EntityTypeBuilder<CourseSubscription> builder)
    {
        builder.ToTable("CourseSubscriptions");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.SubscriptionDate)
            .IsRequired();
            
        builder.Property(x => x.ExpirationDate);
            
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
            
        builder.Property(x => x.PaidAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
            
        builder.Property(x => x.PaymentStatus)
            .IsRequired()
            .HasMaxLength(50);
            
        // Связи
        builder.HasOne(x => x.Course)
            .WithMany(x => x.Subscriptions)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(x => x.User)
            .WithMany(x => x.Subscriptions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Индексы
        builder.HasIndex(x => x.CourseId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.SubscriptionDate);
        builder.HasIndex(x => x.IsActive);
    }
}
