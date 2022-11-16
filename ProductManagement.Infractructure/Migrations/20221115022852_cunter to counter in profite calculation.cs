using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.DAL.Migrations
{
    public partial class cuntertocounterinprofitecalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CunterPerToner",
                table: "ProfitCalculations",
                newName: "CounterPerToner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CounterPerToner",
                table: "ProfitCalculations",
                newName: "CunterPerToner");
        }
    }
}
