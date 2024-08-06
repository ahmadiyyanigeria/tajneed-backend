using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class HouseHoldMemberEntityTypeConfiguration : IEntityTypeConfiguration<HouseHoldMember>
{
    public void Configure(EntityTypeBuilder<HouseHoldMember> builder)
    {
        builder.ToTable("household_members");

        builder.HasKey(hm => hm.Id);

        builder.Property(hm => hm.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(hm => hm.HouseHoldId)
            .IsRequired()
            .HasColumnName("household_id");

        builder.Property(hm => hm.MemberId)
            .IsRequired()
            .HasColumnName("member_id");

        builder.Property(hm => hm.PositionId)
            .IsRequired()
            .HasColumnName("position_id");

        builder.Property(hm => hm.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(hm => hm.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(hm => hm.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(hm => hm.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(hm => hm.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        builder.HasOne(hm => hm.HouseHold)
            .WithMany()
            .HasForeignKey(hm => hm.HouseHoldId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(hm => hm.Member)
            .WithMany()
            .HasForeignKey(hm => hm.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(hm => hm.Position)
            .WithMany()
            .HasForeignKey(hm => hm.PositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}