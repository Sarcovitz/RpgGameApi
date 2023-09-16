using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class MergedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GAME");

            migrationBuilder.CreateSequence(
                name: "ItemPrototypeSequence",
                schema: "GAME");

            migrationBuilder.CreateTable(
                name: "ArmorPrototypes",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false, defaultValueSql: "NEXT VALUE FOR [GAME].[ItemPrototypeSequence]"),
                    IsEquippable = table.Column<bool>(type: "bit", nullable: false),
                    IsRarityDepend = table.Column<bool>(type: "bit", nullable: false),
                    IsTradable = table.Column<bool>(type: "bit", nullable: false),
                    IsUpgradable = table.Column<bool>(type: "bit", nullable: false),
                    IsUsable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredLevel = table.Column<long>(type: "bigint", nullable: false),
                    UpgradeLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EquippableBy = table.Column<int>(type: "int", nullable: false),
                    DefenseValue = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorPrototypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CharacterSlots = table.Column<byte>(type: "tinyint", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginDate = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeaponPrototypes",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false, defaultValueSql: "NEXT VALUE FOR [GAME].[ItemPrototypeSequence]"),
                    IsEquippable = table.Column<bool>(type: "bit", nullable: false),
                    IsRarityDepend = table.Column<bool>(type: "bit", nullable: false),
                    IsTradable = table.Column<bool>(type: "bit", nullable: false),
                    IsUpgradable = table.Column<bool>(type: "bit", nullable: false),
                    IsUsable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredLevel = table.Column<long>(type: "bigint", nullable: false),
                    UpgradeLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EquippableBy = table.Column<int>(type: "int", nullable: false),
                    MinAttackValue = table.Column<long>(type: "bigint", nullable: false),
                    MaxAttackValue = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponPrototypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                schema: "GAME",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    Experience = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RequiredExperience = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Strength = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Dexterity = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Intelligence = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Vitality = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "GAME",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "GAME",
                table: "ArmorPrototypes",
                columns: new[] { "Id", "DefenseValue", "EquippableBy", "IsEquippable", "IsRarityDepend", "IsTradable", "IsUpgradable", "IsUsable", "Name", "RequiredLevel", "UpgradeLevel", "Value" },
                values: new object[,]
                {
                    { 13000m, 10L, 0, true, true, true, true, false, "Scratched Armor", 0L, (byte)0, 10m },
                    { 13001m, 16L, 0, true, true, true, true, false, "Scratched Armor +1", 0L, (byte)1, 50m },
                    { 13002m, 22L, 0, true, true, true, true, false, "Scratched Armor +2", 0L, (byte)2, 100m },
                    { 13003m, 28L, 0, true, true, true, true, false, "Scratched Armor +3", 0L, (byte)3, 150m },
                    { 13004m, 34L, 0, true, true, true, true, false, "Scratched Armor +4", 0L, (byte)4, 200m },
                    { 13005m, 40L, 0, true, true, true, true, false, "Scratched Armor +5", 0L, (byte)5, 250m },
                    { 13006m, 46L, 0, true, true, true, true, false, "Scratched Armor +6", 0L, (byte)6, 300m },
                    { 13007m, 52L, 0, true, true, true, true, false, "Scratched Armor +7", 0L, (byte)7, 350m },
                    { 13008m, 58L, 0, true, true, true, true, false, "Scratched Armor +8", 0L, (byte)5, 400m },
                    { 13009m, 64L, 0, true, true, true, true, false, "Scratched Armor +9", 0L, (byte)9, 450m }
                });

            migrationBuilder.InsertData(
                schema: "GAME",
                table: "WeaponPrototypes",
                columns: new[] { "Id", "EquippableBy", "IsEquippable", "IsRarityDepend", "IsTradable", "IsUpgradable", "IsUsable", "MaxAttackValue", "MinAttackValue", "Name", "RequiredLevel", "UpgradeLevel", "Value" },
                values: new object[,]
                {
                    { 1000m, 0, true, true, true, true, false, 13L, 10L, "Wooden Sword", 0L, (byte)0, 10m },
                    { 1001m, 0, true, true, true, true, false, 17L, 14L, "Wooden Sword +1", 0L, (byte)1, 50m },
                    { 1002m, 0, true, true, true, true, false, 21L, 18L, "Wooden Sword +2", 0L, (byte)2, 100m },
                    { 1003m, 0, true, true, true, true, false, 25L, 22L, "Wooden Sword +3", 0L, (byte)3, 150m },
                    { 1004m, 0, true, true, true, true, false, 29L, 26L, "Wooden Sword +4", 0L, (byte)4, 200m },
                    { 1005m, 0, true, true, true, true, false, 33L, 30L, "Wooden Sword +5", 0L, (byte)5, 250m },
                    { 1006m, 0, true, true, true, true, false, 37L, 34L, "Wooden Sword +6", 0L, (byte)6, 300m },
                    { 1007m, 0, true, true, true, true, false, 41L, 38L, "Wooden Sword +7", 0L, (byte)7, 350m },
                    { 1008m, 0, true, true, true, true, false, 45L, 42L, "Wooden Sword +8", 0L, (byte)8, 400m },
                    { 1009m, 0, true, true, true, true, false, 49L, 46L, "Wooden Sword +9", 0L, (byte)9, 450m },
                    { 1010m, 0, true, true, true, true, false, 21L, 18L, "Long Sword", 5L, (byte)0, 20m },
                    { 1011m, 0, true, true, true, true, false, 25L, 22L, "Long Sword +1", 5L, (byte)1, 75m },
                    { 1012m, 0, true, true, true, true, false, 29L, 26L, "Long Sword +2", 5L, (byte)2, 150m },
                    { 1013m, 0, true, true, true, true, false, 33L, 30L, "Long Sword +3", 5L, (byte)3, 225m },
                    { 1014m, 0, true, true, true, true, false, 37L, 34L, "Long Sword +4", 5L, (byte)4, 300m },
                    { 1015m, 0, true, true, true, true, false, 41L, 38L, "Long Sword +5", 5L, (byte)5, 375m },
                    { 1016m, 0, true, true, true, true, false, 45L, 42L, "Long Sword +6", 5L, (byte)6, 450m },
                    { 1017m, 0, true, true, true, true, false, 49L, 46L, "Long Sword +7", 5L, (byte)7, 525m },
                    { 1018m, 0, true, true, true, true, false, 53L, 50L, "Long Sword +8", 5L, (byte)8, 600m },
                    { 1019m, 0, true, true, true, true, false, 57L, 54L, "Long Sword +9", 5L, (byte)9, 675m },
                    { 5000m, 1, true, true, true, true, false, 13L, 10L, "Wooden Bow", 0L, (byte)0, 10m },
                    { 5001m, 1, true, true, true, true, false, 17L, 14L, "Wooden Bow +1", 0L, (byte)1, 50m },
                    { 5002m, 1, true, true, true, true, false, 21L, 18L, "Wooden Bow +2", 0L, (byte)2, 100m },
                    { 5003m, 1, true, true, true, true, false, 25L, 22L, "Wooden Bow +3", 0L, (byte)3, 150m },
                    { 5004m, 1, true, true, true, true, false, 29L, 26L, "Wooden Bow +4", 0L, (byte)4, 200m },
                    { 5005m, 1, true, true, true, true, false, 33L, 30L, "Wooden Bow +5", 0L, (byte)5, 250m },
                    { 5006m, 1, true, true, true, true, false, 37L, 34L, "Wooden Bow +6", 0L, (byte)6, 300m },
                    { 5007m, 1, true, true, true, true, false, 41L, 38L, "Wooden Bow +7", 0L, (byte)7, 350m },
                    { 5008m, 1, true, true, true, true, false, 45L, 42L, "Wooden Bow +8", 0L, (byte)8, 400m },
                    { 5009m, 1, true, true, true, true, false, 49L, 46L, "Wooden Bow +9", 0L, (byte)9, 450m },
                    { 5010m, 1, true, true, true, true, false, 21L, 18L, "Long Bow", 5L, (byte)0, 20m },
                    { 5011m, 1, true, true, true, true, false, 25L, 22L, "Long Bow +1", 5L, (byte)1, 75m },
                    { 5012m, 1, true, true, true, true, false, 29L, 26L, "Long Bow +2", 5L, (byte)2, 150m },
                    { 5013m, 1, true, true, true, true, false, 33L, 30L, "Long Bow +3", 5L, (byte)3, 225m },
                    { 5014m, 1, true, true, true, true, false, 37L, 34L, "Long Bow +4", 5L, (byte)4, 300m },
                    { 5015m, 1, true, true, true, true, false, 41L, 38L, "Long Bow +5", 5L, (byte)5, 375m },
                    { 5016m, 1, true, true, true, true, false, 45L, 42L, "Long Bow +6", 5L, (byte)6, 450m },
                    { 5017m, 1, true, true, true, true, false, 49L, 46L, "Long Bow +7", 5L, (byte)7, 525m },
                    { 5018m, 1, true, true, true, true, false, 53L, 50L, "Long Bow +8", 5L, (byte)8, 600m },
                    { 5019m, 1, true, true, true, true, false, 57L, 54L, "Long Bow +9", 5L, (byte)9, 675m },
                    { 9000m, 2, true, true, true, true, false, 13L, 10L, "Wooden Staff", 0L, (byte)0, 10m },
                    { 9001m, 2, true, true, true, true, false, 17L, 14L, "Wooden Staff +1", 0L, (byte)1, 50m },
                    { 9002m, 2, true, true, true, true, false, 21L, 18L, "Wooden Staff +2", 0L, (byte)2, 100m },
                    { 9003m, 2, true, true, true, true, false, 25L, 22L, "Wooden Staff +3", 0L, (byte)3, 150m },
                    { 9004m, 2, true, true, true, true, false, 29L, 26L, "Wooden Staff +4", 0L, (byte)4, 200m },
                    { 9005m, 2, true, true, true, true, false, 33L, 30L, "Wooden Staff +5", 0L, (byte)5, 250m },
                    { 9006m, 2, true, true, true, true, false, 37L, 34L, "Wooden Staff +6", 0L, (byte)6, 300m },
                    { 9007m, 2, true, true, true, true, false, 41L, 38L, "Wooden Staff +7", 0L, (byte)7, 350m },
                    { 9008m, 2, true, true, true, true, false, 45L, 42L, "Wooden Staff +8", 0L, (byte)8, 400m },
                    { 9009m, 2, true, true, true, true, false, 49L, 46L, "Wooden Staff +9", 0L, (byte)9, 450m },
                    { 9010m, 2, true, true, true, true, false, 21L, 18L, "Long Staff", 5L, (byte)0, 20m },
                    { 9011m, 2, true, true, true, true, false, 25L, 22L, "Long Staff +1", 5L, (byte)1, 75m },
                    { 9012m, 2, true, true, true, true, false, 29L, 26L, "Long Staff +2", 5L, (byte)2, 150m },
                    { 9013m, 2, true, true, true, true, false, 33L, 30L, "Long Staff +3", 5L, (byte)3, 225m },
                    { 9014m, 2, true, true, true, true, false, 37L, 34L, "Long Staff +4", 5L, (byte)4, 300m },
                    { 9015m, 2, true, true, true, true, false, 41L, 38L, "Long Staff +5", 5L, (byte)5, 375m },
                    { 9016m, 2, true, true, true, true, false, 45L, 42L, "Long Staff +6", 5L, (byte)6, 450m },
                    { 9017m, 2, true, true, true, true, false, 49L, 46L, "Long Staff +7", 5L, (byte)7, 525m },
                    { 9018m, 2, true, true, true, true, false, 53L, 50L, "Long Staff +8", 5L, (byte)8, 600m },
                    { 9019m, 2, true, true, true, true, false, 57L, 54L, "Long Staff +9", 5L, (byte)9, 675m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                schema: "GAME",
                table: "Characters",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                schema: "GAME",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "GAME",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "GAME",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmorPrototypes",
                schema: "GAME");

            migrationBuilder.DropTable(
                name: "Characters",
                schema: "GAME");

            migrationBuilder.DropTable(
                name: "WeaponPrototypes",
                schema: "GAME");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "GAME");

            migrationBuilder.DropSequence(
                name: "ItemPrototypeSequence",
                schema: "GAME");
        }
    }
}
