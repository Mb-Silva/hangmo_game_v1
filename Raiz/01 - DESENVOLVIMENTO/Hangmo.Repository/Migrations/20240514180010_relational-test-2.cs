using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangmo.Repository.Migrations
{
    /// <inheritdoc />
    public partial class relationaltest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inviteLink",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "guessCount",
                table: "Games",
                newName: "GuessCount");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GuessCount",
                table: "Games",
                newName: "guessCount");

            migrationBuilder.AddColumn<string>(
                name: "inviteLink",
                table: "Games",
                type: "text",
                nullable: true);
        }
    }
}
