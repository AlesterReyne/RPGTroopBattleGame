namespace RPGTroopBattleGame.LootSystem;

public enum ItemType
{
    Gold,
    Equipment,
    Consumable
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public class Item
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemRarity Rarity { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }
    public Stats Bonuses { get; set; }

    public Item(string name, ItemType type, ItemRarity rarity, int value, string description)
    {
        Name = name;
        Type = type;
        Rarity = rarity;
        Value = value;
        Description = description;
        Bonuses = new Stats();
    }
}