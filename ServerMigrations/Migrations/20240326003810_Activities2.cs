using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class Activities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_ActivityType_ActivityTypeId",
                table: "Activity");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropIndex(
                name: "IX_Activity_ActivityTypeId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ActivityTypeId",
                table: "Activity");

            migrationBuilder.AddColumn<int>(
                name: "DefaultAthelteTeamId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultCoachTeamId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsWorkOut",
                table: "Activity",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Activity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutType",
                table: "Activity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkoutItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<string>(type: "text", nullable: false),
                    Distance = table.Column<int>(type: "integer", nullable: true),
                    DistanceMeasurment = table.Column<string>(type: "text", nullable: false),
                    WorkoutType = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutItem_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultAthelteTeamId",
                table: "AspNetUsers",
                column: "DefaultAthelteTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultCoachTeamId",
                table: "AspNetUsers",
                column: "DefaultCoachTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutItem_ActivityId",
                table: "WorkoutItem",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Team_DefaultAthelteTeamId",
                table: "AspNetUsers",
                column: "DefaultAthelteTeamId",
                principalTable: "Team",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Team_DefaultCoachTeamId",
                table: "AspNetUsers",
                column: "DefaultCoachTeamId",
                principalTable: "Team",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Team_DefaultAthelteTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Team_DefaultCoachTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "WorkoutItem");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DefaultAthelteTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DefaultCoachTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DefaultAthelteTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DefaultCoachTeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "IsWorkOut",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "WorkoutType",
                table: "Activity");

            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeId",
                table: "Activity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ActivityTypeId",
                table: "Activity",
                column: "ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_ActivityType_ActivityTypeId",
                table: "Activity",
                column: "ActivityTypeId",
                principalTable: "ActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
