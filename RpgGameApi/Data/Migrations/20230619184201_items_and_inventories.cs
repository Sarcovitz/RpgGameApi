using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class items_and_inventories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InventoryId",
                schema: "GAME",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Inventories",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryPages = table.Column<byte>(type: "tinyint", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CharacterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "GAME",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemPrototypeId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalSchema: "GAME",
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CharacterId",
                schema: "GAME",
                table: "Inventories",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventoryId",
                schema: "GAME",
                table: "Items",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemPrototypeId",
                schema: "GAME",
                table: "Items",
                column: "ItemPrototypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items",
                schema: "GAME");

            migrationBuilder.DropTable(
                name: "Inventories",
                schema: "GAME");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                schema: "GAME",
                table: "Characters");
        }
    }
}
