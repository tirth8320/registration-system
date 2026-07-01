using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadhaarPath",
                table: "UserMasters");

            migrationBuilder.DropColumn(
                name: "OtherDocumentPath",
                table: "UserMasters");

            migrationBuilder.DropColumn(
                name: "PanPath",
                table: "UserMasters");

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AadhaarPath = table.Column<string>(type: "text", nullable: true),
                    PanPath = table.Column<string>(type: "text", nullable: true),
                    OtherDocumentPath = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_UserDocuments_UserMasters_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserId",
                table: "UserDocuments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDocuments");

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
        }
    }
}
