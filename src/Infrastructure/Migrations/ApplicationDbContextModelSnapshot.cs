﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TajneedApi.Domain.ValueObjects;
using TajneedApi.Infrastructure.Persistence.Repositories;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.AuditTrailAggregateRoot.AuditTrail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ActivityId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("activity_id");

                    b.Property<DateTime>("DateOccurred")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_occurred");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("details");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("audit_trails", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CaseAggregateRoot.Case", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<BiodataUpdateCase>("BiodataUpdateCase")
                        .HasColumnType("jsonb")
                        .HasColumnName("bio_data_update_case");

                    b.Property<string>("CaseType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("case_type");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<DuplicateAccountCase>("DuplicateAccountCase")
                        .HasColumnType("jsonb")
                        .HasColumnName("duplicate_account_case");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("member_id");

                    b.Property<string>("ReferenceCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("reference_code");

                    b.Property<RelocationCase>("RelocationCase")
                        .HasColumnType("jsonb")
                        .HasColumnName("relocation_case");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("cases", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CodeAggregateRoot.Code", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("codes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CodeAggregateRoot.CodeValue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CodeId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("CodeId");

                    b.ToTable("code_values", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.HouseHoldAggregateRoot.HouseHold", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("households", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.HouseHoldAggregateRoot.HouseHoldMember", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("HouseHoldId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("household_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("member_id");

                    b.Property<string>("PositionId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("position_id");

                    b.HasKey("Id");

                    b.HasIndex("HouseHoldId");

                    b.HasIndex("MemberId");

                    b.HasIndex("PositionId");

                    b.ToTable("household_members", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.JamaatAggregateRoot.AuxiliaryBody", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("AuxiliaryBodyName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("auxiliary_body_name");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("GroupGender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("group_gender");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.HasKey("Id");

                    b.ToTable("auxiliary_bodies", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.JamaatAggregateRoot.Circuit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CircuitCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("circuit_code");

                    b.Property<string>("CircuitName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("circuit_name");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.HasKey("Id");

                    b.ToTable("circuits", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.JamaatAggregateRoot.Jamaat", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CircuitId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("circuit_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("JamaatCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("jamaat_code");

                    b.Property<string>("JamaatName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("jamaat_name");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.HasKey("Id");

                    b.HasIndex("CircuitId");

                    b.ToTable("jamaats", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.JamaatAggregateRoot.Nationality", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("nationalities", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.JamaatAggregateRoot.Position", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("positions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MemberAggregateRoot.Member", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("AimsNo")
                        .HasColumnType("text")
                        .HasColumnName("aims_no");

                    b.Property<DateTime?>("BiatDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("biat_date");

                    b.Property<string>("ChandaNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("chanda_no");

                    b.Property<string>("ChildrenNos")
                        .HasColumnType("text")
                        .HasColumnName("children_nos");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("FatherNo")
                        .HasColumnType("text")
                        .HasColumnName("father_no");

                    b.Property<bool>("IsBornMember")
                        .HasColumnType("boolean")
                        .HasColumnName("is_born_member");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<MembershipInfo>("MembershipInfo")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("membership_info");

                    b.Property<string>("MembershipStatusId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("membership_status_id");

                    b.Property<string>("NationalityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nationality_id");

                    b.Property<string>("NextOfKinAddress")
                        .HasColumnType("text")
                        .HasColumnName("next_of_kin_address");

                    b.Property<string>("NextOfKinName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("next_of_kin_name");

                    b.Property<string>("NextOfKinPhoneNo")
                        .HasColumnType("text")
                        .HasColumnName("next_of_kin_phone_no");

                    b.Property<string>("Occupation")
                        .HasColumnType("text")
                        .HasColumnName("occupation");

                    b.Property<string>("RecordFlag")
                        .HasColumnType("text")
                        .HasColumnName("record_flag");

                    b.Property<string>("SpouseNo")
                        .HasColumnType("text")
                        .HasColumnName("spouse_no");

                    b.Property<string>("WasiyatNo")
                        .HasColumnType("text")
                        .HasColumnName("wasiyat_no");

                    b.HasKey("Id");

                    b.HasIndex("MembershipStatusId");

                    b.HasIndex("NationalityId");

                    b.ToTable("members", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MemberAggregateRoot.MemberMovement", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("FromJamaatId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("from_jamaat_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("member_id");

                    b.Property<DateTime>("MovementDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("movement_date");

                    b.Property<string>("ToJamaatId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("to_jamaat_id");

                    b.HasKey("Id");

                    b.HasIndex("FromJamaatId");

                    b.HasIndex("ToJamaatId");

                    b.ToTable("member_movements", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MemberAggregateRoot.MembershipStatus", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("membership_statuses", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MemberAggregateRoot.PendingMemberRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_on");

                    b.Property<IReadOnlyList<MembershipInfo>>("Requests")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("requests");

                    b.HasKey("Id");

                    b.ToTable("pending_member_requests", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CaseAggregateRoot.Case", b =>
                {
                    b.HasOne("Domain.Entities.MemberAggregateRoot.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Domain.Entities.CodeAggregateRoot.CodeValue", b =>
                {
                    b.HasOne("Domain.Entities.CodeAggregateRoot.Code", "Code")
                        .WithMany()
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Code");
                });

            modelBuilder.Entity("Domain.Entities.HouseHoldAggregateRoot.HouseHoldMember", b =>
                {
                    b.HasOne("Domain.Entities.HouseHoldAggregateRoot.HouseHold", "HouseHold")
                        .WithMany()
                        .HasForeignKey("HouseHoldId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.MemberAggregateRoot.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.JamaatAggregateRoot.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("HouseHold");

                    b.Navigation("Member");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Domain.Entities.JamaatAggregateRoot.Jamaat", b =>
                {
                    b.HasOne("Domain.Entities.JamaatAggregateRoot.Circuit", "Circuit")
                        .WithMany()
                        .HasForeignKey("CircuitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Circuit");
                });

            modelBuilder.Entity("Domain.Entities.MemberAggregateRoot.Member", b =>
                {
                    b.HasOne("Domain.Entities.MemberAggregateRoot.MembershipStatus", "MembershipStatus")
                        .WithMany()
                        .HasForeignKey("MembershipStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.JamaatAggregateRoot.Nationality", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MembershipStatus");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("Domain.Entities.MemberAggregateRoot.MemberMovement", b =>
                {
                    b.HasOne("Domain.Entities.JamaatAggregateRoot.Jamaat", "FromJamaat")
                        .WithMany()
                        .HasForeignKey("FromJamaatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.JamaatAggregateRoot.Jamaat", "ToJamaat")
                        .WithMany()
                        .HasForeignKey("ToJamaatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromJamaat");

                    b.Navigation("ToJamaat");
                });
#pragma warning restore 612, 618
        }
    }
}
