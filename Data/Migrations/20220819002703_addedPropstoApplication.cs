using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSeek.Data.Migrations
{
    public partial class addedPropstoApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Resume",
                table: "Application",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobseekerId",
                table: "Application",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "RecruiterId",
                table: "Application",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_JobOfferId",
                table: "Application",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_JobseekerId",
                table: "Application",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_RecruiterId",
                table: "Application",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_JobOffer_JobOfferId",
                table: "Application",
                column: "JobOfferId",
                principalTable: "JobOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_JobseekerId",
                table: "Application",
                column: "JobseekerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_RecruiterId",
                table: "Application",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_JobOffer_JobOfferId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_JobseekerId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_RecruiterId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_JobOfferId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_JobseekerId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_RecruiterId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "Application");

            migrationBuilder.AlterColumn<string>(
                name: "Resume",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "JobseekerId",
                table: "Application",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
