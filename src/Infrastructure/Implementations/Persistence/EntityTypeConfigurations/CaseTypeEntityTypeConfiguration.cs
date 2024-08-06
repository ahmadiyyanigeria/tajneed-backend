using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class CaseTypeEntityTypeConfiguration : IEntityTypeConfiguration<CaseType>
{
    public void Configure(EntityTypeBuilder<CaseType> builder)
    {
        builder.ToTable("case_types");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(ct => ct.Code)
            .IsRequired()
            .HasColumnName("code");

        builder.Property(ct => ct.Description)
            .HasColumnName("description");

        builder.Property(ct => ct.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(ct => ct.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(ct => ct.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(ct => ct.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(ct => ct.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}