using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GProject.Migrations
{
    /// <inheritdoc />
    public partial class fixmnogokomnogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_GamesLists_ListId",
                table: "GamesGameLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_Games_GameId",
                table: "GamesGameLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_Users_UserId",
                table: "GamesGameLists");

            migrationBuilder.DropIndex(
                name: "IX_GamesGameLists_GameId",
                table: "GamesGameLists");

            migrationBuilder.DropIndex(
                name: "IX_GamesGameLists_ListId",
                table: "GamesGameLists");

            migrationBuilder.DropIndex(
                name: "IX_GamesGameLists_UserId",
                table: "GamesGameLists");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GamesGameLists");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "GamesGameLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GamesGameLists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GamesGameLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ListId",
                table: "GamesGameLists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GamesGameLists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GamesGameLists_GameId",
                table: "GamesGameLists",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesGameLists_ListId",
                table: "GamesGameLists",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesGameLists_UserId",
                table: "GamesGameLists",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameLists_Users_UserId",
                table: "GamesGameLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
