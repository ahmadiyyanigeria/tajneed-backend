using TajneedApi.Domain.Entities.JamaatAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class AuxiliaryBodyEntityTypeConfiguration : IEntityTypeConfiguration<AuxiliaryBody>
{
    public void Configure(EntityTypeBuilder<AuxiliaryBody> builder)
    {
        builder.ToTable("auxiliary_bodies");

        builder.HasKey(ab => ab.Id);

        builder.Property(ab => ab.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(ab => ab.AuxiliaryBodyName)
            .IsRequired()
            .HasColumnName("auxiliary_body_name");

        builder.Property(ab => ab.GroupGender)
            .IsRequired()
            .HasConversion<EnumToStringConverter<Sex>>()
            .HasColumnName("group_gender");

        builder.Property(ab => ab.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(ab => ab.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(ab => ab.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(ab => ab.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(ab => ab.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}