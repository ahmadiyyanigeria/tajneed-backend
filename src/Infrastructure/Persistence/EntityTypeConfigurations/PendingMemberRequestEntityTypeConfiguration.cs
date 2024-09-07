using Domain.Entities.MemberAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class PendingMemberRequestEntityTypeConfiguration : IEntityTypeConfiguration<PendingMemberRequest>
{
    public void Configure(EntityTypeBuilder<PendingMemberRequest> builder)
    {
        builder.ToTable("pending_member_requests");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(p => p.CreatedOn)
            .HasColumnName("created_on");

        builder.Property(p => p.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(p => p.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted");

        builder.Property(p => p.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(p => p.Requests)
            .HasColumnName("requests")
            .HasColumnType("jsonb");
    }
}