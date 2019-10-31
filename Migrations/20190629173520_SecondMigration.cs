using System;
using DocumentProcessing.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentProcessing.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_characters", "a,b,c,d,e,f,g");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ScannedFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "Purposes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Character = table.Column<AppointmentCharacters>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
           

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
               name: "Appointment");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ScannedFiles");

            migrationBuilder.DropColumn(
                name: "Character",
                table: "Purposes");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:appointment_characters", "a,b,c,d,e,f,g");
        }
    }
}
