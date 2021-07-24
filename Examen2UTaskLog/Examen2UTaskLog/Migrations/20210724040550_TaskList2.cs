using Microsoft.EntityFrameworkCore.Migrations;

namespace Examen2UTaskLog.Migrations
{
    public partial class TaskList2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TaskLists_TaskListId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "TaskListId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TaskLists_TaskListId",
                table: "Assignments",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "TaskListId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TaskLists_TaskListId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "TaskListId",
                table: "Assignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TaskLists_TaskListId",
                table: "Assignments",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "TaskListId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
