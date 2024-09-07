using Domain.Entities.CodeAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class CodeEntityTypeConfiguration : IEntityTypeConfiguration<Code>
{
    public void Configure(EntityTypeBuilder<Code> builder)
    {
        builder.ToTable("codes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(c => c.Description)
            .HasColumnName("description");

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