using Domain.Entities.CaseAggregateRoot;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Implementations.Persistence.EntityTypeConfigurations;

public class BiodataUpdateCaseEntityTypeConfiguration : IEntityTypeConfiguration<BiodataUpdateCase>
{
    public void Configure(EntityTypeBuilder<BiodataUpdateCase> builder)
    {
        builder.ToTable("biodata_update_cases");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(b => b.CaseId)
            .IsRequired()
            .HasColumnName("case_id");

        builder.Property(b => b.SurName)
            .IsRequired()
            .HasColumnName("surname");

        builder.Property(b => b.FirstName)
            .IsRequired()
            .HasColumnName("firstname");

        builder.Property(b => b.MiddleName)
            .IsRequired()
            .HasColumnName("middlename");

        builder.Property(b => b.Title)
            .IsRequired()
            .HasColumnName("title");

        builder.Property(b => b.Dob)
            .IsRequired()
            .HasColumnName("dob");

        builder.Property(b => b.Sex)
            .IsRequired()
            .HasConversion<EnumToStringConverter<Sex>>()
            .HasColumnName("sex");

        builder.Property(b => b.Address)
            .IsRequired()
            .HasColumnName("address");

        builder.Property(b => b.MaidenName)
            .IsRequired()
            .HasColumnName("maiden_name");

        builder.Property(b => b.Email)
            .IsRequired()
            .HasColumnName("email");

        builder.Property(b => b.PhoneNumber)
            .IsRequired()
            .HasColumnName("phone_number");

        builder.Property(b => b.EmploymentStatus)
            .IsRequired()
            .HasConversion<EnumToStringConverter<EmploymentStatus>>()
            .HasColumnName("employment_status");

        builder.Property(b => b.JamaatId)
            .IsRequired()
            .HasColumnName("jamaat_id");

        builder.Property(b => b.Notes)
            .HasColumnName("notes");

        builder.Property(b => b.SpouseNo)
            .HasColumnName("spouse_no");

        builder.Property(b => b.FatherNo)
            .HasColumnName("father_no");

        builder.Property(b => b.ChildrenNos)
            .HasColumnName("children_nos");

        builder.Property(b => b.BiometricId)
            .HasColumnName("biometric_id");

        builder.Property(b => b.MaritalStatus)
            .IsRequired()
            .HasConversion<EnumToStringConverter<MaritalStatus>>()
            .HasColumnName("marital_status");

        builder.Property(b => b.MembershipStatusId)
            .IsRequired()
            .HasColumnName("membership_status_id");

        builder.Property(b => b.NextOfKinPhoneNo)
            .HasColumnName("next_of_kin_phone_no");

        builder.Property(b => b.IsBornMember)
            .IsRequired()
            .HasColumnName("is_born_member");

        builder.Property(b => b.Occupation)
            .HasColumnName("occupation");

        builder.Property(b => b.BiatDate)
            .HasColumnName("biat_date");

        builder.Property(b => b.NationalityId)
            .IsRequired()
            .HasColumnName("nationality_id");

        builder.Property(b => b.HouseholdMemberId)
            .IsRequired()
            .HasColumnName("household_member_id");

        builder.HasOne(b => b.Case)
            .WithMany()
            .HasForeignKey(b => b.CaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Jamaat)
            .WithMany()
            .HasForeignKey(b => b.JamaatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.MembershipStatus)
            .WithMany()
            .HasForeignKey(b => b.MembershipStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Nationality)
            .WithMany()
            .HasForeignKey(b => b.NationalityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.HouseholdMember)
            .WithMany()
            .HasForeignKey(b => b.HouseholdMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(b => b.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(b => b.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(b => b.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(b => b.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(b => b.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");
    }
}