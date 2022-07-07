using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdod.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseDTOId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseDTOId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseDTOId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseDTOId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseDTOId",
                table: "Students",
                column: "CourseDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseDTOId",
                table: "Students",
                column: "CourseDTOId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
