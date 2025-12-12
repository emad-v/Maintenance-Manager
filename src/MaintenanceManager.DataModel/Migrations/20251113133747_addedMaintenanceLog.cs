using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedMaintenanceLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    StatusReference = table.Column<string>(type: "text", nullable: false),
                    PerformedBy = table.Column<string>(type: "text", nullable: false),
                    WorkPerformed = table.Column<string>(type: "text", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogs_Reference",
                table: "MaintenanceLogs",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceLogs");
        }
    }
}
