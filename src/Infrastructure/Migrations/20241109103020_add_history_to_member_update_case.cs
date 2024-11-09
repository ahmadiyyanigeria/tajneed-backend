using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using TajneedApi.Domain.ValueObjects;

#nullable disable

namespace TajneedApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_history_to_member_update_case : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cases");

            migrationBuilder.CreateTable(
                name: "member_update_cases",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    member_id = table.Column<string>(type: "text", nullable: false),
                    request_status = table.Column<string>(type: "text", nullable: false),
                    bio_data_update_case = table.Column<BiodataUpdateCase>(type: "jsonb", nullable: true),
                    relocation_case = table.Column<RelocationCase>(type: "jsonb", nullable: true),
                    duplicate_account_case = table.Column<DuplicateAccountCase>(type: "jsonb", nullable: true),
                    approval_histories = table.Column<IReadOnlyList<ApprovalMemberUpdateCaseRequestHistory>>(type: "jsonb", nullable: false),
                    disapproval_history = table.Column<DisapprovalMemberUpdateCaseRequestHistory>(type: "jsonb", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_update_cases", x => x.id);
                    table.ForeignKey(
                        name: "FK_member_update_cases_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_member_update_cases_member_id",
                table: "member_update_cases",
                column: "member_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_update_cases");

            migrationBuilder.CreateTable(
                name: "cases",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    member_id = table.Column<string>(type: "text", nullable: false),
                    bio_data_update_case = table.Column<BiodataUpdateCase>(type: "jsonb", nullable: true),
                    case_type = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duplicate_account_case = table.Column<DuplicateAccountCase>(type: "jsonb", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reference_code = table.Column<string>(type: "text", nullable: false),
                    relocation_case = table.Column<RelocationCase>(type: "jsonb", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_cases_member_id",
                table: "cases",
                column: "member_id");
        }
    }
}
