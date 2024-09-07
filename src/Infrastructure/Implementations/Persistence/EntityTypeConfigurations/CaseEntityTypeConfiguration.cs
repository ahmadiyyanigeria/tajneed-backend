using Domain.Entities.CaseAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class CaseEntityTypeConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
{
    builder.ToTable("cases");

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Id)
        .IsRequired()
        .HasColumnName("id");

    builder.Property(c => c.CaseTypeId)
        .IsRequired()
        .HasColumnName("case_type_id");

    builder.Property(c => c.MemberId)
        .IsRequired()
        .HasColumnName("member_id");

    builder.Property(c => c.Status)
        .IsRequired()
        .HasColumnName("status");

    builder.Property(c => c.ReferenceCode)
        .IsRequired()
        .HasColumnName("reference_code");

    builder.Property(c => c.Details)
        .IsRequired()
        .HasColumnName("details");

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

    builder.HasOne(c => c.CaseType)
        .WithMany()
        .HasForeignKey(c => c.CaseTypeId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(c => c.Member)
        .WithMany()
        .HasForeignKey(c => c.MemberId)
        .OnDelete(DeleteBehavior.Restrict);
}
}