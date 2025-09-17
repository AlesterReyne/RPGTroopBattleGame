namespace RPGTroopBattleGame.LootSystem;

// === Item Categories ===
public enum ItemType
{
    Gold,
    Equipment,
    Consumable
}

// === Item Rarity Levels ===
public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

// === Item Definition ===
public class Item
{
    // === Properties ===
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemRarity Rarity { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }
    public Stats Bonuses { get; set; }   // Only used for Equipment


    // === Constructor ===
    public Item(string name, ItemType type, ItemRarity rarity, int value, string description)
    {
        Name = name;
        Type = type;
        Rarity = rarity;
        Value = value;
        Description = description;

        // Equipment can grant stat bonuses (default = 0)
        Bonuses = new Stats();
    }
}