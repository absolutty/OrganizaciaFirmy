using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizaciaFirmy.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoznamDivizii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PocetZamestnancov = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoznamDivizii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoznamZamestnancov",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priezvisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiviziaId = table.Column<int>(type: "int", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false),
                    OddelenieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoznamZamestnancov", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoznamDivizii");

            migrationBuilder.DropTable(
                name: "ZoznamZamestnancov");
        }
    }
}
