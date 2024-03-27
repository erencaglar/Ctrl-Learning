using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlEdu.Migrations
{
    /// <inheritdoc />
    public partial class instructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorID",
                table: "Courses",
                column: "InstructorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_InstructorID",
                table: "Courses",
                column: "InstructorID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_InstructorID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorID",
                table: "Courses");
        }
    }
}
