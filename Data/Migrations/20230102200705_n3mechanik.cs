using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemZglaszaniaAwariiGlowny.Data.Migrations
{
    public partial class n3mechanik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MechanikId",
                table: "Zgloszenias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Mechaniks",
                columns: table => new
                {
                    MechanikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MechanikName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MechanikNazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechaniks", x => x.MechanikId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenias_MechanikId",
                table: "Zgloszenias",
                column: "MechanikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zgloszenias_Mechaniks_MechanikId",
                table: "Zgloszenias",
                column: "MechanikId",
                principalTable: "Mechaniks",
                principalColumn: "MechanikId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zgloszenias_Mechaniks_MechanikId",
                table: "Zgloszenias");

            migrationBuilder.DropTable(
                name: "Mechaniks");

            migrationBuilder.DropIndex(
                name: "IX_Zgloszenias_MechanikId",
                table: "Zgloszenias");

            migrationBuilder.DropColumn(
                name: "MechanikId",
                table: "Zgloszenias");
        }
    }
}
