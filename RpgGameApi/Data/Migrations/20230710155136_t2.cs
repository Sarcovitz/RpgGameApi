using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class t2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Inventories_InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.AlterColumn<decimal>(
                name: "CharacterId",
                schema: "GAME",
                table: "Inventories",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories",
                column: "CharacterId",
                unique: true,
                filter: "[CharacterId] IS NOT NULL");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                schema: "GAME",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories");

            migrationBuilder.AlterColumn<decimal>(
                name: "CharacterId",
                schema: "GAME",
                table: "Inventories",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters",
                column: "InventoryId",
                unique: true,
                filter: "[InventoryId] IS NOT NULL");

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
    }
}
