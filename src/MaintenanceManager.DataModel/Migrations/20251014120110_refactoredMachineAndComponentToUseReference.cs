using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactoredMachineAndComponentToUseReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Components");

            migrationBuilder.AddColumn<string>(
                name: "CustomerReference",
                table: "Machines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Machines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MachineReference",
                table: "Components",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Components",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_Reference",
                table: "Machines",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_Reference",
                table: "Components",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Machines_Reference",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Components_Reference",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "CustomerReference",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "MachineReference",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Components");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Machines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Components",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
