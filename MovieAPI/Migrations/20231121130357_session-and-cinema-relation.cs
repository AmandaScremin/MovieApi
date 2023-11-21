using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class sessionandcinemarelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CinemaId",
                table: "Sessions",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Cinema_CinemaId",
                table: "Sessions",
                column: "CinemaId",
                principalTable: "Cinema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Cinema_CinemaId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CinemaId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Sessions");
        }
    }
}
