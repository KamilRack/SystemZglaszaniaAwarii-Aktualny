using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwarieNoweZnowu.Data.Migrations
{
    public partial class nowe_tabele_infoseeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Graphic",
                table: "Maszynas",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Maszynas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Graphic",
                table: "Magazyns",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Magazyns",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maszynas_Id",
                table: "Maszynas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Magazyns_Id",
                table: "Magazyns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Magazyns_AspNetUsers_Id",
                table: "Magazyns",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maszynas_AspNetUsers_Id",
                table: "Maszynas",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Magazyns_AspNetUsers_Id",
                table: "Magazyns");

            migrationBuilder.DropForeignKey(
                name: "FK_Maszynas_AspNetUsers_Id",
                table: "Maszynas");

            migrationBuilder.DropIndex(
                name: "IX_Maszynas_Id",
                table: "Maszynas");

            migrationBuilder.DropIndex(
                name: "IX_Magazyns_Id",
                table: "Magazyns");

            migrationBuilder.DropColumn(
                name: "Graphic",
                table: "Maszynas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Maszynas");

            migrationBuilder.DropColumn(
                name: "Graphic",
                table: "Magazyns");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Magazyns");
        }
    }
}
