using Domain.Entities.CodeAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class CodeValueEntityTypeConfiguration : IEntityTypeConfiguration<CodeValue>
{
    public void Configure(EntityTypeBuilder<CodeValue> builder)
    {
        builder.ToTable("code_values");

        builder.HasKey(cv => cv.Id);

        builder.Property(cv => cv.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(cv => cv.CodeId)
            .IsRequired()
            .HasColumnName("code_id");

        builder.Property(cv => cv.Value)
            .IsRequired()
            .HasColumnName("value");

        builder.Property(cv => cv.Description)
            .HasColumnName("description");

        builder.Property(cv => cv.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(cv => cv.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(cv => cv.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(cv => cv.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(cv => cv.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        builder.HasOne(rc => rc.Code)
            .WithMany()
            .HasForeignKey(rc => rc.CodeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}