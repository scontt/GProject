using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollerFate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addusertolists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GamesGameLists",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamesGameLists_UserId",
                table: "GamesGameLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameLists_Users_UserId",
                table: "GamesGameLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameLists_Users_UserId",
                table: "GamesGameLists");

            migrationBuilder.DropIndex(
                name: "IX_GamesGameLists_UserId",
                table: "GamesGameLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GamesGameLists");
        }
    }
}
