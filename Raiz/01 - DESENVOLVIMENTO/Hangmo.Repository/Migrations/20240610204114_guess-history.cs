using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangmo.Repository.Migrations
{
    /// <inheritdoc />
    public partial class guesshistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<char>>(
                name: "GuessHistory",
                table: "Games",
                type: "character(1)[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuessHistory",
                table: "Games");
        }
    }
}
