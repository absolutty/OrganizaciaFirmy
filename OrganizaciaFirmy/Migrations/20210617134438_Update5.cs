using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizaciaFirmy.Migrations
{
    public partial class Update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocetZamestnancov",
                table: "ZoznamProjektov");

            migrationBuilder.DropColumn(
                name: "PocetZamestnancov",
                table: "ZoznamOddeleni");

            migrationBuilder.DropColumn(
                name: "PocetZamestnancov",
                table: "ZoznamDivizii");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PocetZamestnancov",
                table: "ZoznamProjektov",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PocetZamestnancov",
                table: "ZoznamOddeleni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PocetZamestnancov",
                table: "ZoznamDivizii",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
