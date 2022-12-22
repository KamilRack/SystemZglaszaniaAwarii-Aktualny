using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwarieNoweZnowu.Data.Migrations
{
    public partial class new_tabeleczki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Magazyns",
                columns: table => new
                {
                    MagazynId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MagazynName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagazynOpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Display = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazyns", x => x.MagazynId);
                });

            migrationBuilder.CreateTable(
                name: "Maszynas",
                columns: table => new
                {
                    MaszynaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaszynaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaszynaOpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Display = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maszynas", x => x.MaszynaId);
                });

            migrationBuilder.CreateTable(
                name: "MagazynMaszyna",
                columns: table => new
                {
                    MagazynsMagazynId = table.Column<int>(type: "int", nullable: false),
                    MaszynasMaszynaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazynMaszyna", x => new { x.MagazynsMagazynId, x.MaszynasMaszynaId });
                    table.ForeignKey(
                        name: "FK_MagazynMaszyna_Magazyns_MagazynsMagazynId",
                        column: x => x.MagazynsMagazynId,
                        principalTable: "Magazyns",
                        principalColumn: "MagazynId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagazynMaszyna_Maszynas_MaszynasMaszynaId",
                        column: x => x.MaszynasMaszynaId,
                        principalTable: "Maszynas",
                        principalColumn: "MaszynaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zgloszenias",
                columns: table => new
                {
                    ZgloszeniaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwariaOpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MagazynId = table.Column<int>(type: "int", nullable: false),
                    MaszynaId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zgloszenias", x => x.ZgloszeniaId);
                    table.ForeignKey(
                        name: "FK_Zgloszenias_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zgloszenias_Magazyns_MagazynId",
                        column: x => x.MagazynId,
                        principalTable: "Magazyns",
                        principalColumn: "MagazynId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zgloszenias_Maszynas_MaszynaId",
                        column: x => x.MaszynaId,
                        principalTable: "Maszynas",
                        principalColumn: "MaszynaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MagazynMaszyna_MaszynasMaszynaId",
                table: "MagazynMaszyna",
                column: "MaszynasMaszynaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenias_Id",
                table: "Zgloszenias",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenias_MagazynId",
                table: "Zgloszenias",
                column: "MagazynId");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenias_MaszynaId",
                table: "Zgloszenias",
                column: "MaszynaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MagazynMaszyna");

            migrationBuilder.DropTable(
                name: "Zgloszenias");

            migrationBuilder.DropTable(
                name: "Magazyns");

            migrationBuilder.DropTable(
                name: "Maszynas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Information",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
