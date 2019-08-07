using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentProcessing.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateSequence(
                name: "EntryNumbers",
                startValue: 2000L);

            migrationBuilder.AlterColumn<long>(
                name: "EntryNumber",
                table: "Documents",
                nullable: false,
                defaultValueSql: "nextval('\"EntryNumbers\"')",
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "EntryNumbers");

            migrationBuilder.AlterColumn<long>(
                name: "EntryNumber",
                table: "Documents",
                nullable: false,
                oldClrType: typeof(long),
                oldDefaultValueSql: "nextval('\"EntryNumbers\"')");
        }
    }
}
