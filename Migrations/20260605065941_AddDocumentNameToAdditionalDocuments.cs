using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentNameToAdditionalDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "AdditionalDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "AdditionalDocuments");
        }
    }
}
