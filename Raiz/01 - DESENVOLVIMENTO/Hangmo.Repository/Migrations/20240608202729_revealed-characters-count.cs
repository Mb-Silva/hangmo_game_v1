using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangmo.Repository.Migrations
{
    /// <inheritdoc />
    public partial class revealedcharacterscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RightGuessCount",
                table: "Games",
                newName: "RevealedCharactersCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RevealedCharactersCount",
                table: "Games",
                newName: "RightGuessCount");
        }
    }
}
