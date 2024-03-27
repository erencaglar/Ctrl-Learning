using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlEdu.Migrations
{
    /// <inheritdoc />
    public partial class addenroll2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentModel_Courses_CourseID",
                table: "EnrollmentModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentModel_Users_UserID",
                table: "EnrollmentModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentModel",
                table: "EnrollmentModel");

            migrationBuilder.RenameTable(
                name: "EnrollmentModel",
                newName: "Enrollments");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentModel_CourseID",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseID");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentModelCourseID",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentModelUserID",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "UserID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EnrollmentModelUserID_EnrollmentModelCourseID",
                table: "Users",
                columns: new[] { "EnrollmentModelUserID", "EnrollmentModelCourseID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UserID",
                table: "Enrollments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Enrollments_EnrollmentModelUserID_EnrollmentModelCour~",
                table: "Users",
                columns: new[] { "EnrollmentModelUserID", "EnrollmentModelCourseID" },
                principalTable: "Enrollments",
                principalColumns: new[] { "UserID", "CourseID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Enrollments_EnrollmentModelUserID_EnrollmentModelCour~",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EnrollmentModelUserID_EnrollmentModelCourseID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EnrollmentModelCourseID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnrollmentModelUserID",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "EnrollmentModel");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseID",
                table: "EnrollmentModel",
                newName: "IX_EnrollmentModel_CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentModel",
                table: "EnrollmentModel",
                columns: new[] { "UserID", "CourseID" });

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentModel_Courses_CourseID",
                table: "EnrollmentModel",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentModel_Users_UserID",
                table: "EnrollmentModel",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
