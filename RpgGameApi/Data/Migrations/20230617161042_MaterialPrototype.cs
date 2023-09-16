using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaterialPrototype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStackable",
                schema: "GAME",
                table: "WeaponPrototypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStackable",
                schema: "GAME",
                table: "ArmorPrototypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MaterialPrototypes",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false, defaultValueSql: "NEXT VALUE FOR [GAME].[ItemPrototypeSequence]"),
                    IsEquippable = table.Column<bool>(type: "bit", nullable: false),
                    IsRarityDepend = table.Column<bool>(type: "bit", nullable: false),
                    IsStackable = table.Column<bool>(type: "bit", nullable: false),
                    IsTradable = table.Column<bool>(type: "bit", nullable: false),
                    IsUpgradable = table.Column<bool>(type: "bit", nullable: false),
                    IsUsable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredLevel = table.Column<long>(type: "bigint", nullable: false),
                    UpgradeLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPrototypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13000m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13001m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13002m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13003m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13004m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13005m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13006m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13007m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13008m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 13009m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17000m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17001m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17002m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17003m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17004m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17005m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17006m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17007m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17008m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17009m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21000m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21001m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21002m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21003m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21004m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21005m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21006m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21007m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21008m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21009m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1000m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1001m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1002m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1003m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1004m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1005m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1006m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1007m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1008m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1009m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1010m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1011m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1012m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1013m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1014m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1015m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1016m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1017m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1018m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 1019m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5000m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5001m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5002m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5003m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5004m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5005m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5006m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5007m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5008m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5009m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5010m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5011m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5012m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5013m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5014m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5015m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5016m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5017m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5018m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 5019m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9000m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9001m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9002m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9003m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9004m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9005m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9006m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9007m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9008m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9009m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9010m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9011m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9012m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9013m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9014m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9015m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9016m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9017m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9018m,
                column: "IsStackable",
                value: false);

            migrationBuilder.UpdateData(
                schema: "GAME",
                table: "WeaponPrototypes",
                keyColumn: "Id",
                keyValue: 9019m,
                column: "IsStackable",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialPrototypes",
                schema: "GAME");

            migrationBuilder.DropColumn(
                name: "IsStackable",
                schema: "GAME",
                table: "WeaponPrototypes");

            migrationBuilder.DropColumn(
                name: "IsStackable",
                schema: "GAME",
                table: "ArmorPrototypes");
        }
    }
}
