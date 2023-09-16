using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class inventory_slots_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                schema: "GAME",
                table: "InventorySlots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                schema: "GAME",
                table: "InventorySlots");

            migrationBuilder.AddColumn<decimal>(
                name: "InventoryId",
                schema: "GAME",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: true);
        }
    }
}
