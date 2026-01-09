using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GProject.Migrations
{
    /// <inheritdoc />
    public partial class newManyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGameList_GamesLists_ListsId",
                table: "GameGameList");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGameList_Games_GamesId",
                table: "GameGameList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGameList",
                table: "GameGameList");

            migrationBuilder.RenameTable(
                name: "GameGameList",
                newName: "GamesGameLists");

            migrationBuilder.RenameIndex(
                name: "IX_GameGameList_ListsId",
                table: "GamesGameLists",
                newName: "IX_GamesGameLists_ListsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamesGameLists",
                table: "GamesGameLists",
                columns: new[] { "GamesId", "ListsId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_GamesLists_ListsId",
                table: "GamesGameLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_Games_GamesId",
                table: "GamesGameLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamesGameLists",
                table: "GamesGameLists");

            migrationBuilder.RenameTable(
                name: "GamesGameLists",
                newName: "GameGameList");

            migrationBuilder.RenameIndex(
                name: "IX_GamesGameLists_ListsId",
                table: "GameGameList",
                newName: "IX_GameGameList_ListsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGameList",
                table: "GameGameList",
                columns: new[] { "GamesId", "ListsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameGameList_GamesLists_ListsId",
                table: "GameGameList",
                column: "ListsId",
                principalTable: "GamesLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGameList_Games_GamesId",
                table: "GameGameList",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
