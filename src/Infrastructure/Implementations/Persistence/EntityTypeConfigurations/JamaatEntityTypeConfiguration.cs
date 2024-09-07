using Domain.Entities.JamaatAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class JamaatEntityTypeConfiguration : IEntityTypeConfiguration<Jamaat>
{
    public void Configure(EntityTypeBuilder<Jamaat> builder)
    {
        builder.ToTable("jamaats");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(j => j.JamaatName)
            .IsRequired()
            .HasColumnName("jamaat_name");

        builder.Property(j => j.JamaatCode)
            .IsRequired()
            .HasColumnName("jamaat_code");

        builder.Property(j => j.CircuitId)
            .IsRequired()
            .HasColumnName("circuit_id");

        builder.Property(j => j.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(j => j.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(j => j.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(j => j.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(j => j.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        builder.HasOne(j => j.Circuit)
            .WithMany()
            .HasForeignKey(j => j.CircuitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}