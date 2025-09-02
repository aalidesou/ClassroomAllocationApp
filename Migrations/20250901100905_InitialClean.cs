using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomAllocationApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Allocations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VenueChangeRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    AllocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewClassroomId = table.Column<int>(type: "INTEGER", nullable: true),
                    RequestedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueChangeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueChangeRequests_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueChangeRequests_Classrooms_NewClassroomId",
                        column: x => x.NewClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VenueChangeRequests_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_ClassroomId1",
                table: "Allocations",
                column: "ClassroomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_CourseId1",
                table: "Allocations",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_VenueChangeRequests_AllocationId",
                table: "VenueChangeRequests",
                column: "AllocationId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueChangeRequests_NewClassroomId",
                table: "VenueChangeRequests",
                column: "NewClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueChangeRequests_TeacherId",
                table: "VenueChangeRequests",
                column: "TeacherId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allocations_Classrooms_ClassroomId1",
                table: "Allocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Allocations_Courses_CourseId1",
                table: "Allocations");

            migrationBuilder.DropTable(
                name: "VenueChangeRequests");

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

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Allocations");
        }
    }
}
