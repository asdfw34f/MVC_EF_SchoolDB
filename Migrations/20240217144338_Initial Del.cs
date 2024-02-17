using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolTestsApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Class",
                table: "Students",
                newName: "ClassId");

            migrationBuilder.RenameColumn(
                name: "Test",
                table: "History_Tests",
                newName: "TestID");

            migrationBuilder.RenameColumn(
                name: "Student",
                table: "History_Tests",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "Teacher",
                table: "Classes",
                newName: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_History_Tests_StudentId",
                table: "History_Tests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_History_Tests_TestID",
                table: "History_Tests",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Tests_Students_StudentId",
                table: "History_Tests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Tests_Tests_TestID",
                table: "History_Tests",
                column: "TestID",
                principalTable: "Tests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Tests_Students_StudentId",
                table: "History_Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Tests_Tests_TestID",
                table: "History_Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_History_Tests_StudentId",
                table: "History_Tests");

            migrationBuilder.DropIndex(
                name: "IX_History_Tests_TestID",
                table: "History_Tests");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Students",
                newName: "Class");

            migrationBuilder.RenameColumn(
                name: "TestID",
                table: "History_Tests",
                newName: "Test");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "History_Tests",
                newName: "Student");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Classes",
                newName: "Teacher");
        }
    }
}
