using Domain.Entities.MemberAggregateRoot;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(m => m.NationalityId)
            .IsRequired()
            .HasColumnName("nationality_id");

        builder.Property(m => m.AimsNo)
            .HasColumnName("aims_no");

        builder.Property(m => m.MembershipStatusId)
            .IsRequired()
            .HasColumnName("membership_status_id");

        builder.Property(m => m.NextOfKinName)
            .IsRequired()
            .HasColumnName("next_of_kin_name");

        builder.Property(m => m.IsBornMember)
            .IsRequired()
            .HasColumnName("is_born_member");

        builder.Property(m => m.ChandaNo)
            .IsRequired()
            .HasColumnName("chanda_no");

        builder.Property(m => m.Surname)
            .IsRequired()
            .HasColumnName("surname");

        builder.Property(m => m.FirstName)
            .IsRequired()
            .HasColumnName("first_name");

        builder.Property(m => m.AuxiliaryBodyId)
            .IsRequired()
            .HasColumnName("auxiliary_body_id");

        builder.Property(m => m.MiddleName)
            .IsRequired()
            .HasColumnName("middle_name");

        builder.Property(m => m.Dob)
            .IsRequired()
            .HasColumnName("dob");

        builder.Property(m => m.Email)
            .IsRequired()
            .HasColumnName("email");

        builder.Property(m => m.PhoneNo)
            .IsRequired()
            .HasColumnName("phone_no");

        builder.Property(m => m.JamaatId)
            .IsRequired()
            .HasColumnName("jamaat_id");

        builder.Property(m => m.Sex)
            .IsRequired()
            .HasConversion<EnumToStringConverter<Sex>>()
            .HasColumnName("sex");

        builder.Property(m => m.MaritalStatus)
            .IsRequired()
            .HasConversion<EnumToStringConverter<MaritalStatus>>()
            .HasColumnName("marital_status");

        builder.Property(m => m.Address)
            .IsRequired()
            .HasColumnName("address");

        builder.Property(m => m.Status)
            .IsRequired()
            .HasConversion<EnumToStringConverter<Status>>()
            .HasColumnName("status");

        builder.Property(m => m.EmploymentStatus)
            .IsRequired()
            .HasConversion<EnumToStringConverter<EmploymentStatus>>()
            .HasColumnName("employment_status");

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

        builder.Property(m => m.Occupation)
            .HasColumnName("occupation");

        builder.Property(m => m.NextOfKinPhoneNo)
            .HasColumnName("next_of_kin_phone_no");

        builder.Property(m => m.NextOfKinAddress)
            .HasColumnName("next_of_kin_address");

        builder.Property(m => m.BiatDate)
            .HasColumnName("biat_date");

        builder.HasOne(m => m.Nationality)
        .WithMany()
        .HasForeignKey(m => m.NationalityId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.MembershipStatus)
            .WithMany()
            .HasForeignKey(m => m.MembershipStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.AuxiliaryBody)
            .WithMany()
            .HasForeignKey(m => m.AuxiliaryBodyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Jamaat)
            .WithMany()
            .HasForeignKey(m => m.JamaatId)
            .OnDelete(DeleteBehavior.Restrict);

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