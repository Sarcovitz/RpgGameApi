using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class armor_prototypes_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "GAME",
                table: "ArmorPrototypes",
                columns: new[] { "Id", "DefenseValue", "EquippableBy", "IsEquippable", "IsRarityDepend", "IsTradable", "IsUpgradable", "IsUsable", "Name", "RequiredLevel", "UpgradeLevel", "Value" },
                values: new object[,]
                {
                    { 17000m, 10L, 1, true, true, true, true, false, "Scratched Tunic", 0L, (byte)0, 10m },
                    { 17001m, 16L, 1, true, true, true, true, false, "Scratched Tunic +1", 0L, (byte)1, 50m },
                    { 17002m, 22L, 1, true, true, true, true, false, "Scratched Tunic +2", 0L, (byte)2, 100m },
                    { 17003m, 28L, 1, true, true, true, true, false, "Scratched Tunic +3", 0L, (byte)3, 150m },
                    { 17004m, 34L, 1, true, true, true, true, false, "Scratched Tunic +4", 0L, (byte)4, 200m },
                    { 17005m, 40L, 1, true, true, true, true, false, "Scratched Tunic +5", 0L, (byte)5, 250m },
                    { 17006m, 46L, 1, true, true, true, true, false, "Scratched Tunic +6", 0L, (byte)6, 300m },
                    { 17007m, 52L, 1, true, true, true, true, false, "Scratched Tunic +7", 0L, (byte)7, 350m },
                    { 17008m, 58L, 1, true, true, true, true, false, "Scratched Tunic +8", 0L, (byte)5, 400m },
                    { 17009m, 64L, 1, true, true, true, true, false, "Scratched Tunic +9", 0L, (byte)9, 450m },
                    { 21000m, 10L, 2, true, true, true, true, false, "Scratched Robe", 0L, (byte)0, 10m },
                    { 21001m, 16L, 2, true, true, true, true, false, "Scratched Robe +1", 0L, (byte)1, 50m },
                    { 21002m, 22L, 2, true, true, true, true, false, "Scratched Robe +2", 0L, (byte)2, 100m },
                    { 21003m, 28L, 2, true, true, true, true, false, "Scratched Robe +3", 0L, (byte)3, 150m },
                    { 21004m, 34L, 2, true, true, true, true, false, "Scratched Robe +4", 0L, (byte)4, 200m },
                    { 21005m, 40L, 2, true, true, true, true, false, "Scratched Robe +5", 0L, (byte)5, 250m },
                    { 21006m, 46L, 2, true, true, true, true, false, "Scratched Robe +6", 0L, (byte)6, 300m },
                    { 21007m, 52L, 2, true, true, true, true, false, "Scratched Robe +7", 0L, (byte)7, 350m },
                    { 21008m, 58L, 2, true, true, true, true, false, "Scratched Robe +8", 0L, (byte)5, 400m },
                    { 21009m, 64L, 2, true, true, true, true, false, "Scratched Robe +9", 0L, (byte)9, 450m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17000m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17001m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17002m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17003m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17004m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17005m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17006m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17007m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17008m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 17009m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21000m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21001m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21002m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21003m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21004m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21005m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21006m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21007m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21008m);

            migrationBuilder.DeleteData(
                schema: "GAME",
                table: "ArmorPrototypes",
                keyColumn: "Id",
                keyValue: 21009m);
        }
    }
}
