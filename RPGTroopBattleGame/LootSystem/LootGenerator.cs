namespace RPGTroopBattleGame.LootSystem;

public static class LootGenerator
{
    private static readonly Random _random = new Random();


    // === Generate Loot System ===
    public static Item GenerateLoot()
    {
        // --- Step 1: Determine item type ---
        // 70% Gold, 10% Equipment, 20% Consumable
        int typeRoll = _random.Next(100);
        ItemType type;
        if (typeRoll < 70)
            type = ItemType.Gold;
        else if (typeRoll < 80)
            type = ItemType.Equipment;
        else
            type = ItemType.Consumable;

        // --- Step 2: Determine rarity and base value ---
        ItemRarity rarity = type == ItemType.Gold
            ? ItemRarity.Common
            : GenerateRarity();

        int baseValue = type == ItemType.Gold
            ? _random.Next(10, 101)                // Gold value = 10–100
            : CalculateBaseValue(rarity);          // Equipment/consumable scaling by rarity

        // --- Step 3: Name and description ---
        string name = type == ItemType.Gold
            ? $"{baseValue} Gold"
            : GenerateItemName(type, rarity);

        string description = type == ItemType.Gold
            ? "Currency used for trading"
            : GenerateDescription(type, rarity);

        // --- Step 4: Construct item ---
        var item = new Item(name, type, rarity, baseValue, description);

        // --- Step 5: Add stat bonuses for equipment ---
        if (type == ItemType.Equipment)
        {
            GenerateEquipmentBonuses(item, rarity);
        }

        return item;
    }


    // === Loot Generation Helpers ===
    private static ItemRarity GenerateRarity()
    {
        int roll = _random.Next(100);

        if (roll < 40) return ItemRarity.Common;     // 40%
        if (roll < 70) return ItemRarity.Uncommon;   // 30%
        if (roll < 85) return ItemRarity.Rare;       // 15%
        if (roll < 95) return ItemRarity.Epic;       // 10%
        return ItemRarity.Legendary;                 // 5%
    }

    private static int CalculateBaseValue(ItemRarity rarity)
    {
        return rarity switch
        {
            ItemRarity.Common    => _random.Next(10, 31),
            ItemRarity.Uncommon  => _random.Next(30, 61),
            ItemRarity.Rare      => _random.Next(60, 121),
            ItemRarity.Epic      => _random.Next(120, 251),
            ItemRarity.Legendary => _random.Next(250, 501),
            _                    => 10
        };
    }

    private static string GenerateItemName(ItemType type, ItemRarity rarity)
    {
        string[] equipmentPrefixes = { "Sturdy", "Sharp", "Heavy", "Light", "Ancient" };
        string[] equipmentTypes    = { "Sword", "Shield", "Armor", "Bow", "Staff" };
        string[] consumableTypes   = { "Potion", "Scroll", "Elixir", "Tonic", "Crystal" };

        string prefix   = equipmentPrefixes[_random.Next(equipmentPrefixes.Length)];
        string itemType = type == ItemType.Equipment
            ? equipmentTypes[_random.Next(equipmentTypes.Length)]
            : consumableTypes[_random.Next(consumableTypes.Length)];

        return $"{rarity} {prefix} {itemType}";
    }

    private static string GenerateDescription(ItemType type, ItemRarity rarity)
    {
        if (type == ItemType.Equipment)
        {
            return $"A {rarity.ToString().ToLower()} piece of equipment";
        }

        return $"A {rarity.ToString().ToLower()} consumable item";
    }

    private static void GenerateEquipmentBonuses(Item item, ItemRarity rarity)
    {
        int bonusMultiplier = rarity switch
        {
            ItemRarity.Common    => 1,
            ItemRarity.Uncommon  => 2,
            ItemRarity.Rare      => 3,
            ItemRarity.Epic      => 4,
            ItemRarity.Legendary => 5,
            _                    => 1
        };

        item.Bonuses.AttackBonus        = _random.Next(1, 4) * bonusMultiplier;
        item.Bonuses.DefenseBonus       = _random.Next(1, 4) * bonusMultiplier;
        item.Bonuses.HealthBonus        = _random.Next(5, 11) * bonusMultiplier;
        item.Bonuses.CriticalChanceBonus = _random.Next(0, 3) * bonusMultiplier;
    }
}
