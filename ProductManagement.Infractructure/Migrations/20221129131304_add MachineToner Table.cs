using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.DAL.Migrations
{
    public partial class addMachineTonerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toners_Machines_MachineId",
                table: "Toners");

            migrationBuilder.DropIndex(
                name: "IX_Toners_MachineId",
                table: "Toners");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Toners");

            migrationBuilder.CreateTable(
                name: "MachineToner",
                columns: table => new
                {
                    MachineTonerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    TonerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineToner", x => x.MachineTonerId);
                    table.ForeignKey(
                        name: "FK_MachineToner_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineToner_Toners_TonerId",
                        column: x => x.TonerId,
                        principalTable: "Toners",
                        principalColumn: "TonerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineToner_MachineId",
                table: "MachineToner",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineToner_TonerId",
                table: "MachineToner",
                column: "TonerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineToner");

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Toners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toners_MachineId",
                table: "Toners",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toners_Machines_MachineId",
                table: "Toners",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId");
        }
    }
}
