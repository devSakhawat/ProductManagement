using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.DAL.Migrations
{
    public partial class contertocounterinpaperusagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyTotalConter",
                table: "PaperUsages",
                newName: "MonthlyTotalCounter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyTotalCounter",
                table: "PaperUsages",
                newName: "MonthlyTotalConter");
        }
    }
}
