using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class RecoveryField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recovery",
                table: "Activity",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recovery",
                table: "Activity");
        }
    }
}
