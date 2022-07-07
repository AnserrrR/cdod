using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdod.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsToCourses_ContractStates_ContractStateId",
                table: "StudentsToCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsToCourses_Courses_CourseId",
                table: "StudentsToCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsToCourses_Students_StudentId",
                table: "StudentsToCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsToLessons_Lessons_LessonId",
                table: "StudentsToLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsToLessons_Students_StudentId",
                table: "StudentsToLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersToLessons_Lessons_LessonId",
                table: "TeachersToLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersToLessons_Teachers_TeacherId",
                table: "TeachersToLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachersToLessons",
                table: "TeachersToLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsToLessons",
                table: "StudentsToLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsToCourses",
                table: "StudentsToCourses");

            migrationBuilder.RenameTable(
                name: "TeachersToLessons",
                newName: "TeacherToLessons");

            migrationBuilder.RenameTable(
                name: "StudentsToLessons",
                newName: "StudentToLessons");

            migrationBuilder.RenameTable(
                name: "StudentsToCourses",
                newName: "StudentToCourses");

            migrationBuilder.RenameIndex(
                name: "IX_TeachersToLessons_LessonId",
                table: "TeacherToLessons",
                newName: "IX_TeacherToLessons_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsToLessons_StudentId",
                table: "StudentToLessons",
                newName: "IX_StudentToLessons_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsToCourses_StudentId",
                table: "StudentToCourses",
                newName: "IX_StudentToCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsToCourses_ContractStateId",
                table: "StudentToCourses",
                newName: "IX_StudentToCourses_ContractStateId");

            migrationBuilder.AlterColumn<string>(
                name: "Descriotion",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "CourseDTOId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Sum",
                table: "PayNotes",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(38,17)");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "PayNotes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "EquipmentPrice",
                table: "Courses",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(38,17)");

            migrationBuilder.AlterColumn<double>(
                name: "CoursePrice",
                table: "Courses",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(38,17)");

            migrationBuilder.AddColumn<int>(
                name: "CourseDurationInMonths",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherToLessons",
                table: "TeacherToLessons",
                columns: new[] { "TeacherId", "LessonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentToLessons",
                table: "StudentToLessons",
                columns: new[] { "LessonId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentToCourses",
                table: "StudentToCourses",
                columns: new[] { "CourseId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseDTOId",
                table: "Students",
                column: "CourseDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_PayNotes_CourseId",
                table: "PayNotes",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayNotes_Courses_CourseId",
                table: "PayNotes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseDTOId",
                table: "Students",
                column: "CourseDTOId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToCourses_ContractStates_ContractStateId",
                table: "StudentToCourses",
                column: "ContractStateId",
                principalTable: "ContractStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToCourses_Courses_CourseId",
                table: "StudentToCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToCourses_Students_StudentId",
                table: "StudentToCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToLessons_Lessons_LessonId",
                table: "StudentToLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToLessons_Students_StudentId",
                table: "StudentToLessons",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherToLessons_Lessons_LessonId",
                table: "TeacherToLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherToLessons_Teachers_TeacherId",
                table: "TeacherToLessons",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayNotes_Courses_CourseId",
                table: "PayNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseDTOId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentToCourses_ContractStates_ContractStateId",
                table: "StudentToCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentToCourses_Courses_CourseId",
                table: "StudentToCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentToCourses_Students_StudentId",
                table: "StudentToCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentToLessons_Lessons_LessonId",
                table: "StudentToLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentToLessons_Students_StudentId",
                table: "StudentToLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherToLessons_Lessons_LessonId",
                table: "TeacherToLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherToLessons_Teachers_TeacherId",
                table: "TeacherToLessons");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseDTOId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_PayNotes_CourseId",
                table: "PayNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherToLessons",
                table: "TeacherToLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentToLessons",
                table: "StudentToLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentToCourses",
                table: "StudentToCourses");

            migrationBuilder.DropColumn(
                name: "CourseDTOId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "PayNotes");

            migrationBuilder.DropColumn(
                name: "CourseDurationInMonths",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "TeacherToLessons",
                newName: "TeachersToLessons");

            migrationBuilder.RenameTable(
                name: "StudentToLessons",
                newName: "StudentsToLessons");

            migrationBuilder.RenameTable(
                name: "StudentToCourses",
                newName: "StudentsToCourses");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherToLessons_LessonId",
                table: "TeachersToLessons",
                newName: "IX_TeachersToLessons_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentToLessons_StudentId",
                table: "StudentsToLessons",
                newName: "IX_StudentsToLessons_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentToCourses_StudentId",
                table: "StudentsToCourses",
                newName: "IX_StudentsToCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentToCourses_ContractStateId",
                table: "StudentsToCourses",
                newName: "IX_StudentsToCourses_ContractStateId");

            migrationBuilder.AlterColumn<string>(
                name: "Descriotion",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum",
                table: "PayNotes",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "EquipmentPrice",
                table: "Courses",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "CoursePrice",
                table: "Courses",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachersToLessons",
                table: "TeachersToLessons",
                columns: new[] { "TeacherId", "LessonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsToLessons",
                table: "StudentsToLessons",
                columns: new[] { "LessonId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsToCourses",
                table: "StudentsToCourses",
                columns: new[] { "CourseId", "StudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsToCourses_ContractStates_ContractStateId",
                table: "StudentsToCourses",
                column: "ContractStateId",
                principalTable: "ContractStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsToCourses_Courses_CourseId",
                table: "StudentsToCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsToCourses_Students_StudentId",
                table: "StudentsToCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsToLessons_Lessons_LessonId",
                table: "StudentsToLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsToLessons_Students_StudentId",
                table: "StudentsToLessons",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersToLessons_Lessons_LessonId",
                table: "TeachersToLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersToLessons_Teachers_TeacherId",
                table: "TeachersToLessons",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
