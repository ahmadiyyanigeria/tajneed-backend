using Domain.Entities.CaseAggregateRoot;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class CaseEntityTypeConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.ToTable("cases");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");


        builder.Property(c => c.MemberId)
            .IsRequired()
            .HasColumnName("member_id");

        builder.Property(c => c.Status)
            .IsRequired()
            .HasColumnName("status");

        builder.Property(c => c.ReferenceCode)
            .IsRequired()
            .HasColumnName("reference_code");

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<EnumToStringConverter<Status>>()
            .HasColumnName("status");

        builder.Property(c => c.CaseType)
            .IsRequired()
            .HasConversion<EnumToStringConverter<CaseType>>()
            .HasColumnName("case_type");

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

        builder.Property(p => p.BiodataUpdateCase)
            .HasColumnName("bio_data_update_case")
            .HasColumnType("jsonb");

        builder.Property(p => p.DuplicateAccountCase)
            .HasColumnName("duplicate_account_case")
            .HasColumnType("jsonb");

        builder.Property(p => p.RelocationCase)
            .HasColumnName("relocation_case")
            .HasColumnType("jsonb");

        builder.HasOne(c => c.Member)
            .WithMany()
            .HasForeignKey(c => c.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}