using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training_Managment_System.Migrations
{
    /// <inheritdoc />
    public partial class CourseNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseName",
                table: "Courses",
                column: "CourseName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseName",
                table: "Courses");
        }
    }
}
