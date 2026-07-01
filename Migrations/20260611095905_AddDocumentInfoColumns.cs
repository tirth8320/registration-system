using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentInfoColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDocuments_UserMasters_UserMasterUserId",
                table: "AdditionalDocuments");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalDocuments_UserMasterUserId",
                table: "AdditionalDocuments");

            migrationBuilder.DropColumn(
                name: "UserMasterUserId",
                table: "AdditionalDocuments");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "AdditionalDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "AdditionalDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "AdditionalDocuments");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "AdditionalDocuments");

            migrationBuilder.AddColumn<int>(
                name: "UserMasterUserId",
                table: "AdditionalDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalDocuments_UserMasterUserId",
                table: "AdditionalDocuments",
                column: "UserMasterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDocuments_UserMasters_UserMasterUserId",
                table: "AdditionalDocuments",
                column: "UserMasterUserId",
                principalTable: "UserMasters",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
