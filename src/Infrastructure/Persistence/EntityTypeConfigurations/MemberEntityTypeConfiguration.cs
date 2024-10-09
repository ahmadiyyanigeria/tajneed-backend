using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Enums;

namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .IsRequired()
            .HasColumnName("id");


        builder.Property(m => m.AimsNo)
            .HasColumnName("aims_no");

        builder.Property(m => m.MembershipStatus)
            .IsRequired()
            .HasConversion<EnumToStringConverter<MembershipStatus>>()
            .HasColumnName("membership_status");

        builder.Property(m => m.NextOfKinName)
            .IsRequired()
            .HasColumnName("next_of_kin_name");


        builder.Property(m => m.ChandaNo)
            .IsRequired()
            .HasColumnName("chanda_no");

        builder.Property(m => m.WasiyatNo)
            .HasColumnName("wasiyat_no");

        builder.Property(m => m.SpouseNo)
            .HasColumnName("spouse_no");

        builder.Property(m => m.RecordFlag)
            .HasColumnName("record_flag");

        builder.Property(m => m.FatherNo)
            .HasColumnName("father_no");

        builder.Property(m => m.ChildrenNos)
            .HasColumnName("children_nos");


        builder.Property(m => m.NextOfKinPhoneNo)
            .HasColumnName("next_of_kin_phone_no");

        builder.Property(m => m.NextOfKinAddress)
            .HasColumnName("next_of_kin_address");



        builder.Property(m => m.CreatedOn)
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