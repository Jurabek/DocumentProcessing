using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentProcessing.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaDate",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisaDateTypeId",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaId",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisaTypeId",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegistrationId",
                table: "Documents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestId",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    DocumentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestId_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisaDateType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaDateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisaType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaType", x => x.Id);
                });

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
                name: "IX_Statuses_Name",
                table: "Statuses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Purposes_Name",
                table: "Purposes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VisaDateTypeId",
                table: "Documents",
                column: "VisaDateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VisaTypeId",
                table: "Documents",
                column: "VisaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RegistartionId",
                table: "Documents",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EntryNumber_AppointmentNumber",
                table: "Documents",
                columns: new[] { "EntryNumber", "AppointmentNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentOwners_Name",
                table: "DocumentOwners",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_Name",
                table: "Applicants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RequestId_DocumentId",
                table: "RequestId",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_VisaDateType_VisaDateTypeId",
                table: "Documents",
                column: "VisaDateTypeId",
                principalTable: "VisaDateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_VisaType_VisaTypeId",
                table: "Documents",
                column: "VisaTypeId",
                principalTable: "VisaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Documents_VisaDateType_VisaDateTypeId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_VisaType_VisaTypeId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "RequestId");

            migrationBuilder.DropTable(
                name: "VisaDateType");

            migrationBuilder.DropTable(
                name: "VisaType");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Name",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Purposes_Name",
                table: "Purposes");

            migrationBuilder.DropIndex(
                name: "IX_Documents_VisaDateTypeId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_VisaTypeId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_EntryNumber_AppointmentNumber",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_DocumentOwners_Name",
                table: "DocumentOwners");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_Name",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "VisaDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "VisaDateTypeId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "VisaId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "VisaTypeId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "Documents");


        }
    }
}
