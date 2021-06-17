using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizaciaFirmy.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdOddelenia",
                table: "ZoznamOddeleni",
                newName: "IdDivizie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdDivizie",
                table: "ZoznamOddeleni",
                newName: "IdOddelenia");
        }
    }
}
