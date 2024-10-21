using Microsoft.EntityFrameworkCore.Migrations;
using TajneedApi.Domain.ValueObjects;

#nullable disable

namespace TajneedApi.Infrastructure.Migrations;

/// <inheritdoc />
public partial class initial : Migration
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
            name: "membership_requests",
            columns: table => new
            {
                id = table.Column<string>(type: "text", nullable: false),
                jamaat_id = table.Column<string>(type: "text", nullable: false),
                batch_request_id = table.Column<string>(type: "text", nullable: false),
                surname = table.Column<string>(type: "text", nullable: false),
                first_name = table.Column<string>(type: "text", nullable: false),
                auxiliary_body_id = table.Column<string>(type: "text", nullable: false),
                middle_name = table.Column<string>(type: "text", nullable: false),
                dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                phone_no = table.Column<string>(type: "text", nullable: false),
                sex = table.Column<string>(type: "text", nullable: false),
                marital_status = table.Column<string>(type: "text", nullable: false),
                address = table.Column<string>(type: "text", nullable: false),
                request_status = table.Column<string>(type: "text", nullable: false),
                employment_status = table.Column<string>(type: "text", nullable: false),
                is_born_member = table.Column<bool>(type: "boolean", nullable: false),
                occupation = table.Column<string>(type: "text", nullable: false),
                biat_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                nationality_id = table.Column<string>(type: "text", nullable: false),
                approval_histories = table.Column<IReadOnlyList<ApprovalMemberRequestHistory>>(type: "jsonb", nullable: false),
                disapproval_history = table.Column<DisapprovalMemberRequestHistory>(type: "jsonb", nullable: true),
                created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                created_by = table.Column<string>(type: "text", nullable: false),
                last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                last_modified_by = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_membership_requests", x => x.id);
                table.ForeignKey(
                    name: "FK_membership_requests_auxiliary_bodies_auxiliary_body_id",
                    column: x => x.auxiliary_body_id,
                    principalTable: "auxiliary_bodies",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_membership_requests_jamaats_jamaat_id",
                    column: x => x.jamaat_id,
                    principalTable: "jamaats",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_membership_requests_nationalities_nationality_id",
                    column: x => x.nationality_id,
                    principalTable: "nationalities",
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
                MembershipRequestId = table.Column<string>(type: "text", nullable: false),
                membership_status = table.Column<string>(type: "text", nullable: false),
                next_of_kin_phone_no = table.Column<string>(type: "text", nullable: true),
                next_of_kin_name = table.Column<string>(type: "text", nullable: false),
                next_of_kin_address = table.Column<string>(type: "text", nullable: true),
                created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                created_by = table.Column<string>(type: "text", nullable: false),
                last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                last_modified_by = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_members", x => x.id);
                table.ForeignKey(
                    name: "FK_members_membership_requests_MembershipRequestId",
                    column: x => x.MembershipRequestId,
                    principalTable: "membership_requests",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_members_membership_requests_id",
                    column: x => x.id,
                    principalTable: "membership_requests",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "cases",
            columns: table => new
            {
                id = table.Column<string>(type: "text", nullable: false),
                reference_code = table.Column<string>(type: "text", nullable: false),
                case_type = table.Column<string>(type: "text", nullable: false),
                member_id = table.Column<string>(type: "text", nullable: false),
                status = table.Column<string>(type: "text", nullable: false),
                bio_data_update_case = table.Column<BiodataUpdateCase>(type: "jsonb", nullable: true),
                relocation_case = table.Column<RelocationCase>(type: "jsonb", nullable: true),
                duplicate_account_case = table.Column<DuplicateAccountCase>(type: "jsonb", nullable: true),
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

        migrationBuilder.CreateIndex(
            name: "IX_cases_member_id",
            table: "cases",
            column: "member_id");

        migrationBuilder.CreateIndex(
            name: "IX_code_values_code_id",
            table: "code_values",
            column: "code_id");

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
            name: "IX_members_MembershipRequestId",
            table: "members",
            column: "MembershipRequestId");

        migrationBuilder.CreateIndex(
            name: "IX_membership_requests_auxiliary_body_id",
            table: "membership_requests",
            column: "auxiliary_body_id");

        migrationBuilder.CreateIndex(
            name: "IX_membership_requests_jamaat_id",
            table: "membership_requests",
            column: "jamaat_id");

        migrationBuilder.CreateIndex(
            name: "IX_membership_requests_nationality_id",
            table: "membership_requests",
            column: "nationality_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "audit_trails");

        migrationBuilder.DropTable(
            name: "cases");

        migrationBuilder.DropTable(
            name: "code_values");

        migrationBuilder.DropTable(
            name: "household_members");

        migrationBuilder.DropTable(
            name: "member_movements");

        migrationBuilder.DropTable(
            name: "codes");

        migrationBuilder.DropTable(
            name: "households");

        migrationBuilder.DropTable(
            name: "members");

        migrationBuilder.DropTable(
            name: "positions");

        migrationBuilder.DropTable(
            name: "membership_requests");

        migrationBuilder.DropTable(
            name: "auxiliary_bodies");

        migrationBuilder.DropTable(
            name: "jamaats");

        migrationBuilder.DropTable(
            name: "nationalities");

        migrationBuilder.DropTable(
            name: "circuits");
    }
}
