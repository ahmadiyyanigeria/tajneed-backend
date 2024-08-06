using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class AuxillaryBodyEntityTypeConfiguration : IEntityTypeConfiguration<AuxillaryBody>
{
    public void Configure(EntityTypeBuilder<AuxillaryBody> builder)
    {
        builder.ToTable("auxillary_bodies");

        builder.HasKey(ab => ab.Id);

        builder.Property(ab => ab.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(ab => ab.AuxillaryBodyName)
            .IsRequired()
            .HasColumnName("auxillary_body_name");

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