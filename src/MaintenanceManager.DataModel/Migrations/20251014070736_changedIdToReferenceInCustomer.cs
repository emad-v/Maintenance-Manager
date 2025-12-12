using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedIdToReferenceInCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNo",
                table: "Components",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Reference",
                table: "Customers",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Reference",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNo",
                table: "Components",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
