using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOverdueTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ComponentRuleStatuses_ComponentReference_MaintenanceRuleRef~",
                table: "ComponentRuleStatuses",
                columns: new[] { "ComponentReference", "MaintenanceRuleReference" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ComponentRuleStatuses_ComponentReference_MaintenanceRuleRef~",
                table: "ComponentRuleStatuses");
        }
    }
}
