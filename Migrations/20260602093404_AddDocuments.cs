using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class AddDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AadhaarPath",
                table: "UserMasters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDocumentPath",
                table: "UserMasters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PanPath",
                table: "UserMasters",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMasters_RoleId",
                table: "UserMasters",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMasters_RoleMasters_RoleId",
                table: "UserMasters",
                column: "RoleId",
                principalTable: "RoleMasters",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMasters_RoleMasters_RoleId",
                table: "UserMasters");

            migrationBuilder.DropIndex(
                name: "IX_UserMasters_RoleId",
                table: "UserMasters");

            migrationBuilder.DropColumn(
                name: "AadhaarPath",
                table: "UserMasters");

            migrationBuilder.DropColumn(
                name: "OtherDocumentPath",
                table: "UserMasters");

            migrationBuilder.DropColumn(
                name: "PanPath",
                table: "UserMasters");
        }
    }
}
