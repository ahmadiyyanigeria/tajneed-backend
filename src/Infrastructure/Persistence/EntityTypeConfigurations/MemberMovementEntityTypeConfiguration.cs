using Domain.Entities.MemberAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class MemberMovementEntityTypeConfiguration : IEntityTypeConfiguration<MemberMovement>
{
    public void Configure(EntityTypeBuilder<MemberMovement> builder)
    {
        builder.ToTable("member_movements");

        builder.HasKey(mm => mm.Id);

        builder.Property(mm => mm.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(mm => mm.MemberId)
            .IsRequired()
            .HasColumnName("member_id");

        builder.Property(mm => mm.FromJamaatId)
            .IsRequired()
            .HasColumnName("from_jamaat_id");

        builder.Property(mm => mm.ToJamaatId)
            .IsRequired()
            .HasColumnName("to_jamaat_id");

        builder.Property(mm => mm.MovementDate)
            .IsRequired()
            .HasColumnName("movement_date");

        builder.HasOne(mm => mm.FromJamaat)
            .WithMany()
            .HasForeignKey(mm => mm.FromJamaatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mm => mm.ToJamaat)
            .WithMany()
            .HasForeignKey(mm => mm.ToJamaatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(mm => mm.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(mm => mm.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(mm => mm.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(mm => mm.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(mm => mm.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}