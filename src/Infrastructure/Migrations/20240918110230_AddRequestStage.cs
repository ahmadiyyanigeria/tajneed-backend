using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TajneedApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "request_status",
                table: "pending_member_requests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "request_status",
                table: "pending_member_requests");
        }
    }
}
