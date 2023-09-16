using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class navigations_corrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.AlterColumn<decimal>(
                name: "UserId",
                schema: "GAME",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InventoryId",
                schema: "GAME",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters",
                column: "InventoryId",
                unique: true,
                filter: "[InventoryId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_InventoryId",
                schema: "GAME",
                table: "Characters");

            migrationBuilder.AlterColumn<decimal>(
                name: "UserId",
                schema: "GAME",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InventoryId",
                schema: "GAME",
                table: "Characters",
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
                unique: true);
        }
    }
}
