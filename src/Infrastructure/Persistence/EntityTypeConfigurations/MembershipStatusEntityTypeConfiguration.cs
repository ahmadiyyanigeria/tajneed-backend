using Domain.Entities.MemberAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class MembershipStatusEntityTypeConfiguration : IEntityTypeConfiguration<MembershipStatus>
{
    public void Configure(EntityTypeBuilder<MembershipStatus> builder)
    {
        builder.ToTable("membership_statuses");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(m => m.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(m => m.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(m => m.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(m => m.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(m => m.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(m => m.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}