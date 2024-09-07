using Domain.Entities.CaseAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class DuplicateAccountCaseEntityTypeConfiguration : IEntityTypeConfiguration<DuplicateAccountCase>
{
    public void Configure(EntityTypeBuilder<DuplicateAccountCase> builder)
    {
        builder.ToTable("duplicate_account_cases");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(d => d.CaseId)
            .IsRequired()
            .HasColumnName("case_id");

        builder.Property(d => d.PrimaryAccount)
            .IsRequired()
            .HasColumnName("primary_account");

        builder.Property(d => d.OtherAccounts)
            .IsRequired()
            .HasColumnName("other_accounts");

        builder.Property(d => d.Notes)
            .HasColumnName("notes");

        builder.HasOne(d => d.Case)
            .WithMany()
            .HasForeignKey(d => d.CaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(d => d.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(d => d.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(d => d.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(d => d.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(d => d.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }

}