using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskmanagementapi.Migrations
{
    /// <inheritdoc />
    public partial class addstatuscoloums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "status",
                table: "tasks");
        }
    }
}
