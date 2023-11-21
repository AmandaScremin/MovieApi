using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class changesondelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinema_Address_AddressId",
                table: "Cinema");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinema_Address_AddressId",
                table: "Cinema",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinema_Address_AddressId",
                table: "Cinema");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinema_Address_AddressId",
                table: "Cinema",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
