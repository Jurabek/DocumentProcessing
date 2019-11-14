using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentProcessing.Migrations
{
    public partial class FifthMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegistrationId",
                table: "Documents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RegistrationId",
                table: "Documents",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_Name",
                table: "Registration",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Registration_RegistrationId",
                table: "Documents",
                column: "RegistrationId",
                principalTable: "Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Registration_RegistrationId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropIndex(
                name: "IX_Documents_RegistrationId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "Documents");
        }
    }
}
