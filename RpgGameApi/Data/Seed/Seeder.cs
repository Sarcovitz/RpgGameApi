using CsvHelper;
using RpgGame.Models.Entity;
using System.Globalization;

namespace RpgGame.Data.Seed;

public class Seeder
{
    private static List<T> LoadFromCsvFile<T>(string fileName)
    {
        using StreamReader reader = new($"Data\\Seed\\Files\\{fileName}.csv");
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        List<T> records = csv.GetRecords<T>().ToList();
        return records;
    }

    //GETTERS
    public static List<ArmorPrototype> GetArmorPrototypes()
    {
        List<ArmorPrototype> seed = new();
        seed.AddRange(LoadFromCsvFile<ArmorPrototype>("ArmorPrototypes_Warrior")); //WARRIOR
        seed.AddRange(LoadFromCsvFile<ArmorPrototype>("ArmorPrototypes_Archer")); //ARCHER
        seed.AddRange(LoadFromCsvFile<ArmorPrototype>("ArmorPrototypes_Mage")); //MAGE
        return seed;
    }

    public static List<MaterialPrototype> GetMaterialPrototypes()
    {
        List<MaterialPrototype> seed = new();
        seed.AddRange(LoadFromCsvFile<MaterialPrototype>("MaterialPrototypes"));
        return seed;
    }

    public static List<WeaponPrototype> GetWeaponPrototypes()
    {
        List<WeaponPrototype> seed = new();
        seed.AddRange(LoadFromCsvFile<WeaponPrototype>("WeaponPrototypes_Warrior")); //WARRIOR
        seed.AddRange(LoadFromCsvFile<WeaponPrototype>("WeaponPrototypes_Archer")); //ARCHER
        seed.AddRange(LoadFromCsvFile<WeaponPrototype>("WeaponPrototypes_Mage")); //MAGE
        return seed;
    }
}
