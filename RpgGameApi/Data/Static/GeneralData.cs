namespace RpgGame.Data.Static;

public static class GeneralData
{
    //USER
    public static readonly byte BASE_CHARACTER_SLOTS = 3;
    public static readonly byte MAX_CHARACTER_SLOTS = 10;

    //CHARATER
    public static readonly uint MAX_CHARACTER_LEVEL = 99;

    //INVENTORY
    public static readonly byte BASE_INVENTORY_PAGES = 3;
    public static readonly byte MAX_INVENTORY_PAGES = 6;
    public static readonly ushort INVENTORY_PAGE_SLOTS = 50;

    //ITEM
    public static readonly uint MAX_ITEM_STACK_SIZE = 1_000;
}
