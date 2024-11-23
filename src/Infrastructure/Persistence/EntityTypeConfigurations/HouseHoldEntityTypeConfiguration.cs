using TajneedApi.Domain.Entities.HouseHoldAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class HouseHoldEntityTypeConfiguration : IEntityTypeConfiguration<HouseHold>
{
    public void Configure(EntityTypeBuilder<HouseHold> builder)
    {
        builder.ToTable("households");

        builder.HasKey(hh => hh.Id);

        builder.Property(hh => hh.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(hh => hh.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(hh => hh.Address)
            .IsRequired()
            .HasColumnName("address");

        builder.Property(hh => hh.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(hh => hh.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(hh => hh.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(hh => hh.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(hh => hh.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}
