using Microsoft.EntityFrameworkCore;
using RpgGame.Data.Seed;
using RpgGame.Models.Entity;
using RpgGame.Models.Entity.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgGame.Data;

public class GameDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public GameDbContext(DbContextOptions<GameDbContext> options, IConfiguration configuration) 
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<ArmorPrototype> ArmorPrototypes { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventorySlot> InventorySlots { get; set; }
    public DbSet<ItemBase> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WeaponPrototype> WeaponPrototypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("default"));
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("GAME");

        var armorPrototype = modelBuilder.Entity<ArmorPrototype>();
        armorPrototype.HasBaseType<ItemPrototypeBase>();
        armorPrototype.ToTable("ArmorPrototypes");
        armorPrototype.HasData(Seeder.GetArmorPrototypes());

        var character = modelBuilder.Entity<Character>();
        character.ToTable("Characters");
        character.HasKey(character => character.Id);
        character.Property(character => character.Id)
            .UseIdentityColumn();
        character.HasIndex(character => character.Name);
        character.HasOne(character => character.Inventory)
            .WithOne(inventory => inventory.Character)
            .HasForeignKey<Inventory>(inventory => inventory.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        var inventory = modelBuilder.Entity<Inventory>();
        inventory.ToTable("Inventories");
        inventory.HasKey(inventory => inventory.Id);
        inventory.Property(inventory => inventory.Id)
            .UseIdentityColumn();
        inventory.HasMany(inventory => inventory.Slots)
            .WithOne(slot => slot.Inventory)
            .HasForeignKey(slot => slot.InventoryId)
            .OnDelete(DeleteBehavior.Cascade);

        var inventorySlot = modelBuilder.Entity<InventorySlot>();
        inventorySlot.ToTable("InventorySlots");
        inventorySlot.HasKey(inventorySlot => inventorySlot.Id);
        inventorySlot.Property(inventorySlot => inventorySlot.Id)
            .HasDefaultValueSql("NEWID()");
        inventorySlot.HasOne(inventorySlot => inventorySlot.Item)
            .WithOne(item => item.InventorySlot)
            .HasForeignKey<ItemBase>(item => item.InventorySlotId);

        var item = modelBuilder.Entity<ItemBase>();
        item.UseTpcMappingStrategy();
        item.HasKey(item => item.Id);
        item.Property(item => item.Id)
            .HasDefaultValueSql("NEWID()");

        var itemPrototype = modelBuilder.Entity<ItemPrototypeBase>();
        itemPrototype.UseTpcMappingStrategy();
        itemPrototype.HasKey(itemPrototype => itemPrototype.Id);

        var materialPrototype = modelBuilder.Entity<MaterialPrototype>();
        materialPrototype.HasBaseType<ItemPrototypeBase>();
        materialPrototype.ToTable("MaterialPrototypes");

        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.HasKey(user => user.Id);
        user.Property(user => user.Id)
            .UseIdentityColumn();
        user.HasIndex(user => user.Username)
            .IsUnique();
        user.HasIndex(user => user.Email)
            .IsUnique();
        user.HasMany(user => user.Characters)
            .WithOne(character => character.User)
            .HasForeignKey(user => user.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        var weaponPrototype = modelBuilder.Entity<WeaponPrototype>();
        weaponPrototype.HasBaseType<ItemPrototypeBase>();
        weaponPrototype.ToTable("WeaponPrototypes");
        weaponPrototype.HasData(Seeder.GetWeaponPrototypes());

        var weapon = modelBuilder.Entity<Weapon>();
        weapon.HasBaseType<ItemBase>();
        weapon.ToTable("Weapons");

        base.OnModelCreating(modelBuilder);
    }
}
