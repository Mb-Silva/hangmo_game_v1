using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangmo.Repository.Migrations
{
    /// <inheritdoc />
    public partial class rightguesscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RightGuessCount",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RightGuessCount",
                table: "Games");
        }
    }
}
