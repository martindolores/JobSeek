using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeek.Data.Migrations
{
    public partial class changeTypeofJobseekerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobseekerId",
                table: "Application",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JobseekerId",
                table: "Application",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
