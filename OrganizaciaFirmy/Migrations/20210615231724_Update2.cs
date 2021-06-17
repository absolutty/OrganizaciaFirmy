using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizaciaFirmy.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoznamOddeleni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PocetZamestnancov = table.Column<int>(type: "int", nullable: false),
                    IdVeducehoOddelenia = table.Column<int>(type: "int", nullable: false),
                    IdOddelenia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoznamOddeleni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoznamProjektov",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zameranie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PocetZamestnancov = table.Column<int>(type: "int", nullable: false),
                    IdVeducehoProjektu = table.Column<int>(type: "int", nullable: false),
                    IdDivizie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoznamProjektov", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoznamOddeleni");

            migrationBuilder.DropTable(
                name: "ZoznamProjektov");
        }
    }
}
