using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class DeleteWorkoutFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutItem_Activity_ActivityId",
                table: "WorkoutItem");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "WorkoutItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutItem_Activity_ActivityId",
                table: "WorkoutItem",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutItem_Activity_ActivityId",
                table: "WorkoutItem");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "WorkoutItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutItem_Activity_ActivityId",
                table: "WorkoutItem",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id");
        }
    }
}
