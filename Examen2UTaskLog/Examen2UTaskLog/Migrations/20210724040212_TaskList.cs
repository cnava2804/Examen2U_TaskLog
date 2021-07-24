using Microsoft.EntityFrameworkCore.Migrations;

namespace Examen2UTaskLog.Migrations
{
    public partial class TaskList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskListId",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    TaskListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskListName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.TaskListId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TaskListId",
                table: "Assignments",
                column: "TaskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TaskLists_TaskListId",
                table: "Assignments",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "TaskListId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TaskLists_TaskListId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TaskListId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "TaskListId",
                table: "Assignments");
        }
    }
}
