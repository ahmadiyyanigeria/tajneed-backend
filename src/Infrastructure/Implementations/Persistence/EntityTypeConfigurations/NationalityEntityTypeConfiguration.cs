using Domain.Entities.JamaatAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class NationalityEntityTypeConfiguration : IEntityTypeConfiguration<Nationality>
{
    public void Configure(EntityTypeBuilder<Nationality> builder)
    {
        builder.ToTable("nationalities");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(n => n.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(n => n.Code)
            .IsRequired()
            .HasColumnName("code");

        builder.Property(n => n.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(n => n.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(n => n.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(n => n.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(n => n.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}