using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_trails",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    activity_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    user_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    details = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    date_occurred = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_trails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auxiliary_bodies",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    auxiliary_body_name = table.Column<string>(type: "text", nullable: false),
                    group_gender = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auxiliary_bodies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "case_types",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "circuits",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    circuit_name = table.Column<string>(type: "text", nullable: false),
                    circuit_code = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_circuits", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "codes",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "households",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_households", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "membership_statuses",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nationalities",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nationalities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pending_member_requests",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    requests = table.Column<IReadOnlyList<MemberRequest>>(type: "jsonb", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pending_member_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jamaats",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    jamaat_name = table.Column<string>(type: "text", nullable: false),
                    jamaat_code = table.Column<string>(type: "text", nullable: false),
                    circuit_id = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jamaats", x => x.id);
                    table.ForeignKey(
                        name: "FK_jamaats_circuits_circuit_id",
                        column: x => x.circuit_id,
                        principalTable: "circuits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "code_values",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    code_id = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code_values", x => x.id);
                    table.ForeignKey(
                        name: "FK_code_values_codes_code_id",
                        column: x => x.code_id,
                        principalTable: "codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member_movements",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    member_id = table.Column<string>(type: "text", nullable: false),
                    from_jamaat_id = table.Column<string>(type: "text", nullable: false),
                    to_jamaat_id = table.Column<string>(type: "text", nullable: false),
                    movement_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_movements", x => x.id);
                    table.ForeignKey(
                        name: "FK_member_movements_jamaats_from_jamaat_id",
                        column: x => x.from_jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_member_movements_jamaats_to_jamaat_id",
                        column: x => x.to_jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    chanda_no = table.Column<string>(type: "text", nullable: false),
                    wasiyat_no = table.Column<string>(type: "text", nullable: true),
                    spouse_no = table.Column<string>(type: "text", nullable: true),
                    father_no = table.Column<string>(type: "text", nullable: true),
                    children_nos = table.Column<string>(type: "text", nullable: true),
                    aims_no = table.Column<string>(type: "text", nullable: true),
                    record_flag = table.Column<string>(type: "text", nullable: true),
                    membership_status_id = table.Column<string>(type: "text", nullable: false),
                    next_of_kin_phone_no = table.Column<string>(type: "text", nullable: true),
                    next_of_kin_name = table.Column<string>(type: "text", nullable: false),
                    next_of_kin_address = table.Column<string>(type: "text", nullable: true),
                    is_born_member = table.Column<bool>(type: "boolean", nullable: false),
                    occupation = table.Column<string>(type: "text", nullable: true),
                    biat_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nationality_id = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    surname = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    auxiliary_body_id = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_no = table.Column<string>(type: "text", nullable: false),
                    jamaat_id = table.Column<string>(type: "text", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    marital_status = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    employment_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_members_auxiliary_bodies_auxiliary_body_id",
                        column: x => x.auxiliary_body_id,
                        principalTable: "auxiliary_bodies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_members_jamaats_jamaat_id",
                        column: x => x.jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_members_membership_statuses_membership_status_id",
                        column: x => x.membership_status_id,
                        principalTable: "membership_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_members_nationalities_nationality_id",
                        column: x => x.nationality_id,
                        principalTable: "nationalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cases",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    reference_code = table.Column<string>(type: "text", nullable: false),
                    case_type_id = table.Column<string>(type: "text", nullable: false),
                    member_id = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    details = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cases", x => x.id);
                    table.ForeignKey(
                        name: "FK_cases_case_types_case_type_id",
                        column: x => x.case_type_id,
                        principalTable: "case_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cases_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "household_members",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    household_id = table.Column<string>(type: "text", nullable: false),
                    member_id = table.Column<string>(type: "text", nullable: false),
                    position_id = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_members_households_household_id",
                        column: x => x.household_id,
                        principalTable: "households",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_household_members_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_household_members_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "duplicate_account_cases",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    case_id = table.Column<string>(type: "text", nullable: false),
                    primary_account = table.Column<string>(type: "text", nullable: false),
                    other_accounts = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_duplicate_account_cases", x => x.id);
                    table.ForeignKey(
                        name: "FK_duplicate_account_cases_cases_case_id",
                        column: x => x.case_id,
                        principalTable: "cases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "relocation_cases",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    case_id = table.Column<string>(type: "text", nullable: false),
                    old_jamaat_id = table.Column<string>(type: "text", nullable: false),
                    new_jamaat_id = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relocation_cases", x => x.id);
                    table.ForeignKey(
                        name: "FK_relocation_cases_cases_case_id",
                        column: x => x.case_id,
                        principalTable: "cases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relocation_cases_jamaats_new_jamaat_id",
                        column: x => x.new_jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relocation_cases_jamaats_old_jamaat_id",
                        column: x => x.old_jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "biodata_update_cases",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    case_id = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    middlename = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    maiden_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    employment_status = table.Column<string>(type: "text", nullable: false),
                    jamaat_id = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    spouse_no = table.Column<string>(type: "text", nullable: true),
                    father_no = table.Column<string>(type: "text", nullable: true),
                    children_nos = table.Column<string>(type: "text", nullable: true),
                    biometric_id = table.Column<string>(type: "text", nullable: true),
                    marital_status = table.Column<string>(type: "text", nullable: false),
                    membership_status_id = table.Column<string>(type: "text", nullable: false),
                    next_of_kin_phone_no = table.Column<string>(type: "text", nullable: true),
                    is_born_member = table.Column<bool>(type: "boolean", nullable: false),
                    occupation = table.Column<string>(type: "text", nullable: true),
                    biat_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nationality_id = table.Column<string>(type: "text", nullable: false),
                    household_member_id = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_biodata_update_cases", x => x.id);
                    table.ForeignKey(
                        name: "FK_biodata_update_cases_cases_case_id",
                        column: x => x.case_id,
                        principalTable: "cases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_biodata_update_cases_household_members_household_member_id",
                        column: x => x.household_member_id,
                        principalTable: "household_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_biodata_update_cases_jamaats_jamaat_id",
                        column: x => x.jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_biodata_update_cases_membership_statuses_membership_status_~",
                        column: x => x.membership_status_id,
                        principalTable: "membership_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_biodata_update_cases_nationalities_nationality_id",
                        column: x => x.nationality_id,
                        principalTable: "nationalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_biodata_update_cases_case_id",
                table: "biodata_update_cases",
                column: "case_id");

            migrationBuilder.CreateIndex(
                name: "IX_biodata_update_cases_household_member_id",
                table: "biodata_update_cases",
                column: "household_member_id");

            migrationBuilder.CreateIndex(
                name: "IX_biodata_update_cases_jamaat_id",
                table: "biodata_update_cases",
                column: "jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_biodata_update_cases_membership_status_id",
                table: "biodata_update_cases",
                column: "membership_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_biodata_update_cases_nationality_id",
                table: "biodata_update_cases",
                column: "nationality_id");

            migrationBuilder.CreateIndex(
                name: "IX_cases_case_type_id",
                table: "cases",
                column: "case_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_cases_member_id",
                table: "cases",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_code_values_code_id",
                table: "code_values",
                column: "code_id");

            migrationBuilder.CreateIndex(
                name: "IX_duplicate_account_cases_case_id",
                table: "duplicate_account_cases",
                column: "case_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_members_household_id",
                table: "household_members",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_members_member_id",
                table: "household_members",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_members_position_id",
                table: "household_members",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_jamaats_circuit_id",
                table: "jamaats",
                column: "circuit_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_movements_from_jamaat_id",
                table: "member_movements",
                column: "from_jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_movements_to_jamaat_id",
                table: "member_movements",
                column: "to_jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_auxiliary_body_id",
                table: "members",
                column: "auxiliary_body_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_jamaat_id",
                table: "members",
                column: "jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_membership_status_id",
                table: "members",
                column: "membership_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_nationality_id",
                table: "members",
                column: "nationality_id");

            migrationBuilder.CreateIndex(
                name: "IX_relocation_cases_case_id",
                table: "relocation_cases",
                column: "case_id");

            migrationBuilder.CreateIndex(
                name: "IX_relocation_cases_new_jamaat_id",
                table: "relocation_cases",
                column: "new_jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_relocation_cases_old_jamaat_id",
                table: "relocation_cases",
                column: "old_jamaat_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_trails");

            migrationBuilder.DropTable(
                name: "biodata_update_cases");

            migrationBuilder.DropTable(
                name: "code_values");

            migrationBuilder.DropTable(
                name: "duplicate_account_cases");

            migrationBuilder.DropTable(
                name: "member_movements");

            migrationBuilder.DropTable(
                name: "pending_member_requests");

            migrationBuilder.DropTable(
                name: "relocation_cases");

            migrationBuilder.DropTable(
                name: "household_members");

            migrationBuilder.DropTable(
                name: "codes");

            migrationBuilder.DropTable(
                name: "cases");

            migrationBuilder.DropTable(
                name: "households");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "case_types");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "auxiliary_bodies");

            migrationBuilder.DropTable(
                name: "jamaats");

            migrationBuilder.DropTable(
                name: "membership_statuses");

            migrationBuilder.DropTable(
                name: "nationalities");

            migrationBuilder.DropTable(
                name: "circuits");
        }
    }
}
