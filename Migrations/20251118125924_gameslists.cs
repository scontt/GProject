using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GProject.Migrations
{
    /// <inheritdoc />
    public partial class gameslists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "GamesLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GamesLists_CreatorId",
                table: "GamesLists",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesLists_Users_CreatorId",
                table: "GamesLists",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesLists_Users_CreatorId",
                table: "GamesLists");

            migrationBuilder.DropIndex(
                name: "IX_GamesLists_CreatorId",
                table: "GamesLists");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "GamesLists");
        }
    }
}
