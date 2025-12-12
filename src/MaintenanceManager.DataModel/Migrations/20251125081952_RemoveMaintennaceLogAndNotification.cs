using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaintennaceLogAndNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceLogs");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "UserCustomers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "UserCustomers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MaintenanceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    PerformedBy = table.Column<string>(type: "text", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    StatusReference = table.Column<string>(type: "text", nullable: false),
                    WorkPerformed = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    RecipientReference = table.Column<string>(type: "text", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    StatusReference = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogs_Reference",
                table: "MaintenanceLogs",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Reference",
                table: "Notifications",
                column: "Reference",
                unique: true);
        }
    }
}
