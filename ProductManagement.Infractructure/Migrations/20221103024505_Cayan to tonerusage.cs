using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.DAL.Migrations
{
    public partial class Cayantotonerusage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cyan",
                table: "TonerUsages",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cyan",
                table: "TonerUsages");
        }
    }
}
