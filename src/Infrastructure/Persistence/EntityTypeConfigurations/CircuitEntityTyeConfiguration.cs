using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class CircuitEntityTyeConfiguration : IEntityTypeConfiguration<Circuit>
{
    public void Configure(EntityTypeBuilder<Circuit> builder)
    {
        builder.ToTable("circuits");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(c => c.CircuitName)
            .IsRequired()
            .HasColumnName("circuit_name");

        builder.Property(c => c.CircuitCode)
            .IsRequired()
            .HasColumnName("circuit_code");

        builder.Property(c => c.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(c => c.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(c => c.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(c => c.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(c => c.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}