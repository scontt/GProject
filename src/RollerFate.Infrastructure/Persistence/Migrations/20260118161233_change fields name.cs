using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollerFate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changefieldsname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_GamesLists_ListsId",
                table: "GamesGameLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_Games_GamesId",
                table: "GamesGameLists");

            migrationBuilder.RenameColumn(
                name: "ListsId",
                table: "GamesGameLists",
                newName: "ListId");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "GamesGameLists",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesGameLists_ListsId",
                table: "GamesGameLists",
                newName: "IX_GamesGameLists_ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameLists_GamesLists_ListId",
                table: "GamesGameLists",
                column: "ListId",
                principalTable: "GamesLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameLists_Games_GameId",
                table: "GamesGameLists",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_GamesLists_ListId",
                table: "GamesGameLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_Games_GameId",
                table: "GamesGameLists");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "GamesGameLists",
                newName: "ListsId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "GamesGameLists",
                newName: "GamesId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesGameLists_ListId",
                table: "GamesGameLists",
                newName: "IX_GamesGameLists_ListsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameLists_GamesLists_ListsId",
                table: "GamesGameLists",
                column: "ListsId",
                principalTable: "GamesLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameLists_Games_GamesId",
                table: "GamesGameLists",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
