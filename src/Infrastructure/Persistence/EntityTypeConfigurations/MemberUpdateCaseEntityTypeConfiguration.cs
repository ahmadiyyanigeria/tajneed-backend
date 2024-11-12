using TajneedApi.Domain.Entities.CaseAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.EntityTypeConfigurations;

public class CaseEntityTypeConfiguration : IEntityTypeConfiguration<MemberUpdateCase>
{
    public void Configure(EntityTypeBuilder<MemberUpdateCase> builder)
    {
        builder.ToTable("member_update_cases");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");


        builder.Property(c => c.MemberId)
            .IsRequired()
            .HasColumnName("member_id");



        builder.Property(c => c.RequestStatus)
            .IsRequired()
            .HasConversion<EnumToStringConverter<RequestStatus>>()
            .HasColumnName("request_status");

      

        builder.Property(c => c.CreatedOn)
            .IsRequired()
            .HasColumnName("created_on");

        builder.Property(c => c.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by");

        builder.Property(c => c.LastModifiedOn)
            .HasColumnName("last_modified_on");

        builder.Property(c => c.LastModifiedBy)
            .HasColumnName("last_modified_by");

        builder.Property(c => c.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        builder.Property(p => p.BiodataUpdateCase)
            .HasColumnName("bio_data_update_case")
            .HasColumnType("jsonb");

        builder.Property(p => p.DuplicateAccountCase)
            .HasColumnName("duplicate_account_case")
            .HasColumnType("jsonb");

        builder.Property(p => p.RelocationCase)
            .HasColumnName("relocation_case")
            .HasColumnType("jsonb");

        builder.HasOne(c => c.Member)
            .WithMany()
            .HasForeignKey(c => c.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(p => p.ApprovalHistories)
            .HasColumnName("approval_histories")
            .HasColumnType("jsonb");

        
        builder.Property(p => p.DisApprovalHistory)
            .HasColumnName("disapproval_history")
            .HasColumnType("jsonb");
    }
}