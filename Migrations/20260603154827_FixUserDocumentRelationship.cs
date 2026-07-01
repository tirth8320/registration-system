using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class FixUserDocumentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "UserMasters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "UserMasters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
