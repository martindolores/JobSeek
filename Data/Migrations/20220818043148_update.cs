using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeek.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    JobOfferId = table.Column<int>(nullable: false),
                    JobseekerId = table.Column<string>(nullable: false),
                    Resume = table.Column<string>(nullable: true),
                    CoverLetter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");
        }
    }
}
