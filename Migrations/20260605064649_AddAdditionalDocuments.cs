using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserMasterUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalDocuments_UserMasters_UserMasterUserId",
                        column: x => x.UserMasterUserId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalDocuments_UserMasterUserId",
                table: "AdditionalDocuments",
                column: "UserMasterUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalDocuments");
        }
    }
}
