using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addNextDueIn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextDueAt",
                table: "ComponentRuleStatuses");

            migrationBuilder.AddColumn<int>(
                name: "NextDueIn",
                table: "ComponentRuleStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextDueIn",
                table: "ComponentRuleStatuses");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextDueAt",
                table: "ComponentRuleStatuses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
