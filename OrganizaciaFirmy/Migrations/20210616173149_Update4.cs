using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizaciaFirmy.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdDivizie",
                table: "ZoznamOddeleni",
                newName: "IdProjektu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProjektu",
                table: "ZoznamOddeleni",
                newName: "IdDivizie");
        }
    }
}
