using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace registration.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTimeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleMasters",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMasters", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "UserMasters",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "text", nullable: false),
                    EmailID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MobileNo = table.Column<string>(type: "text", nullable: false),
                    DOB = table.Column<DateTime>(type: "Timestamp without time zone", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "Timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMasters", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleMasters");

            migrationBuilder.DropTable(
                name: "UserMasters");
        }
    }
}
