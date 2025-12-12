using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLastMaintenanceCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastMaintenanceCounterValue",
                table: "ComponentRuleStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMaintenanceCounterValue",
                table: "ComponentRuleStatuses");
        }
    }
}
