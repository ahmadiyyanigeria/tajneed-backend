using Domain.Entities.JamaatAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class PositionEntityTypeConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(p => p.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(p => p.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(p => p.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}