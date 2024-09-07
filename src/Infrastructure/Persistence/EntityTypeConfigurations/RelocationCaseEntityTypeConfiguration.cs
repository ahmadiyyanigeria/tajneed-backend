using Domain.Entities.CaseAggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class RelocationCaseEntityTypeConfiguration : IEntityTypeConfiguration<RelocationCase>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RelocationCase> builder)
    {
        builder.ToTable("relocation_cases");

        builder.HasKey(rc => rc.Id);

        builder.Property(rc => rc.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(rc => rc.CaseId)
            .IsRequired()
            .HasColumnName("case_id");

        builder.Property(rc => rc.OldJamaatId)
            .IsRequired()
            .HasColumnName("old_jamaat_id");

        builder.Property(rc => rc.NewJamaatId)
            .IsRequired()
            .HasColumnName("new_jamaat_id");

        builder.Property(rc => rc.Notes)
            .HasColumnName("notes");

        builder.Property(rc => rc.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(rc => rc.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(rc => rc.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(rc => rc.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(rc => rc.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        builder.HasOne(rc => rc.Case)
            .WithMany()
            .HasForeignKey(rc => rc.CaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rc => rc.OldJamaat)
            .WithMany()
            .HasForeignKey(rc => rc.OldJamaatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rc => rc.NewJamaat)
            .WithMany()
            .HasForeignKey(rc => rc.NewJamaatId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}