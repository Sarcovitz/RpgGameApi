using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class not_nul_charcterId_in_inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories",
                column: "CharacterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories");

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
        }
    }
}
