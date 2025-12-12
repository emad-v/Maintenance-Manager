using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedEntitiesAndDatabaseModelsForMaintenanceruleAndRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentRuleStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    ComponentReference = table.Column<string>(type: "text", nullable: false),
                    MaintenanceRuleReference = table.Column<string>(type: "text", nullable: false),
                    UsageCounterReference = table.Column<string>(type: "text", nullable: false),
                    LastServiceAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NextDueAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsOverDue = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentRuleStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    RuleName = table.Column<string>(type: "text", nullable: false),
                    IntervalValue = table.Column<int>(type: "integer", nullable: false),
                    CounterType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AppliesTo = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsageCounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    ComponentReference = table.Column<string>(type: "text", nullable: false),
                    CounterType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageCounters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentRuleStatuses_Reference",
                table: "ComponentRuleStatuses",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRules_Reference",
                table: "MaintenanceRules",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageCounters_ComponentReference_CounterType",
                table: "UsageCounters",
                columns: new[] { "ComponentReference", "CounterType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageCounters_Reference",
                table: "UsageCounters",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentRuleStatuses");

            migrationBuilder.DropTable(
                name: "MaintenanceRules");

            migrationBuilder.DropTable(
                name: "UsageCounters");
        }
    }
}
