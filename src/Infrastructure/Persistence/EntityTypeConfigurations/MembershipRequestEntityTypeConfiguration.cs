using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Enums;

namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class MembershipRequestEntityTypeConfiguration : IEntityTypeConfiguration<MembershipRequest>
{
    public void Configure(EntityTypeBuilder<MembershipRequest> builder)
    {
        builder.ToTable("membership_requests");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(m => m.JamaatId)
            .IsRequired()
            .HasColumnName("jamaat_id");

        builder.HasOne(m => m.Jamaat)
        .WithMany()
        .HasForeignKey(m => m.JamaatId)
        .OnDelete(DeleteBehavior.Restrict);

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
        
        builder.Property(m => m.BatchRequestId)
            .IsRequired()
            .HasColumnName("batch_request_id");

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
            .HasColumnName("middle_name");

        builder.Property(m => m.Dob)
                    .IsRequired()

            .HasColumnName("dob");

        builder.Property(m => m.Email)
                    .IsRequired()

            .HasColumnName("email");

        builder.Property(m => m.PhoneNo)
            .HasColumnName("phone_no");

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

        builder.Property(m => m.RequestStatus)
            .HasConversion<EnumToStringConverter<RequestStatus>>()
            .HasColumnName("request_status");

        builder.Property(m => m.EmploymentStatus)
            .HasConversion<EnumToStringConverter<EmploymentStatus>>()
            .IsRequired()
            .HasColumnName("employment_status");

        builder.Property(m => m.IsBornMember)
            .HasColumnName("is_born_member");

        builder.Property(m => m.Occupation)
            .HasColumnName("occupation");

        builder.Property(m => m.BiatDate)
            .HasColumnName("biat_date");

        builder.Property(m => m.NationalityId)
                    .IsRequired()

            .HasColumnName("nationality_id");

        builder.HasOne(m => m.AuxiliaryBody)
            .WithMany()
            .HasForeignKey(m => m.AuxiliaryBodyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Nationality)
            .WithMany()
            .HasForeignKey(m => m.NationalityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Member)
            .WithOne()
            .HasForeignKey<Member>(m => m.Id);

        builder.Property(p => p.ApprovalHistories)
            .HasColumnName("approval_histories")
            .HasColumnType("jsonb");

        
        builder.Property(p => p.DisApprovalHistory)
            .HasColumnName("disapproval_history")
            .HasColumnType("jsonb");


    }
}