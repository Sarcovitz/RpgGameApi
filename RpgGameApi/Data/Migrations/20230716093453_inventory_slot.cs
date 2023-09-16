using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class inventory_slot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Inventories_InventoryId",
                schema: "GAME",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_InventoryId",
                schema: "GAME",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                schema: "GAME",
                table: "Items");

            migrationBuilder.AlterColumn<decimal>(
                name: "ItemPrototypeId",
                schema: "GAME",
                table: "Items",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InventorySlotId",
                schema: "GAME",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventorySlots",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InventoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventorySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventorySlots_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalSchema: "GAME",
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventorySlotId",
                schema: "GAME",
                table: "Items",
                column: "InventorySlotId",
                unique: true,
                filter: "[InventorySlotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySlots_InventoryId",
                schema: "GAME",
                table: "InventorySlots",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_InventorySlots_InventorySlotId",
                schema: "GAME",
                table: "Items",
                column: "InventorySlotId",
                principalSchema: "GAME",
                principalTable: "InventorySlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_InventorySlots_InventorySlotId",
                schema: "GAME",
                table: "Items");

            migrationBuilder.DropTable(
                name: "InventorySlots",
                schema: "GAME");

            migrationBuilder.DropIndex(
                name: "IX_Items_InventorySlotId",
                schema: "GAME",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "InventorySlotId",
                schema: "GAME",
                table: "Items");

            migrationBuilder.AlterColumn<decimal>(
                name: "ItemPrototypeId",
                schema: "GAME",
                table: "Items",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AddColumn<decimal>(
                name: "InventoryId",
                schema: "GAME",
                table: "Items",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventoryId",
                schema: "GAME",
                table: "Items",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Inventories_InventoryId",
                schema: "GAME",
                table: "Items",
                column: "InventoryId",
                principalSchema: "GAME",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
