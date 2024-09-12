using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class AuditTrailEntityTypeConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder.ToTable("audit_trails");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(a => a.ActivityId)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("activity_id");

        builder.Property(a => a.UserId)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("user_id");

        builder.Property(a => a.Details)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("details");

        builder.Property(a => a.DateOccurred)
            .IsRequired()
            .HasColumnName("date_occurred");
    }
}