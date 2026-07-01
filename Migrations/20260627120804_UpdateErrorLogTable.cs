using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class UpdateErrorLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InnerException",
                table: "ErrorLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestMethod",
                table: "ErrorLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Severity",
                table: "ErrorLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "ErrorLogs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InnerException",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "RequestMethod",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "ErrorLogs");
        }
    }
}
