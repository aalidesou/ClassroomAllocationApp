using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomAllocationApp.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allocations_Classrooms_ClassroomId1",
                table: "Allocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Allocations_Courses_CourseId1",
                table: "Allocations");

            migrationBuilder.DropIndex(
                name: "IX_Allocations_ClassroomId1",
                table: "Allocations");

            migrationBuilder.DropIndex(
                name: "IX_Allocations_CourseId1",
                table: "Allocations");

            migrationBuilder.DropColumn(
                name: "ClassroomId1",
                table: "Allocations");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Allocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassroomId1",
                table: "Allocations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "Allocations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_ClassroomId1",
                table: "Allocations",
                column: "ClassroomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_CourseId1",
                table: "Allocations",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Allocations_Classrooms_ClassroomId1",
                table: "Allocations",
                column: "ClassroomId1",
                principalTable: "Classrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Allocations_Courses_CourseId1",
                table: "Allocations",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
