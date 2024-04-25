using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class MultipleCoaches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_AspNetUsers_ApplicationUserId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_ApplicationUserId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Team");

            migrationBuilder.CreateTable(
                name: "ApplicationUserTeam",
                columns: table => new
                {
                    CoachedTeamsId = table.Column<int>(type: "integer", nullable: false),
                    CoachesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTeam", x => new { x.CoachedTeamsId, x.CoachesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTeam_AspNetUsers_CoachesId",
                        column: x => x.CoachesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTeam_Team_CoachedTeamsId",
                        column: x => x.CoachedTeamsId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTeam_CoachesId",
                table: "ApplicationUserTeam",
                column: "CoachesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserTeam");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Team",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_ApplicationUserId",
                table: "Team",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_AspNetUsers_ApplicationUserId",
                table: "Team",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
