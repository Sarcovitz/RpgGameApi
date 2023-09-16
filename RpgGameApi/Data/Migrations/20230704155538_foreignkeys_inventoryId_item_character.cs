using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeys_inventoryId_item_character : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                schema: "GAME",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters",
                column: "InventoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Inventories_InventoryId",
                schema: "GAME",
                table: "Characters",
                column: "InventoryId",
                principalSchema: "GAME",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Inventories_InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                schema: "GAME",
                table: "Inventories",
                column: "CharacterId",
                principalSchema: "GAME",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
