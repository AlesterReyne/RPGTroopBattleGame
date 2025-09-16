using RPGTroopBattleGame.LootSystem;

namespace RPGTroopBattleGame;

public static class BattleResults
{
    public static void DisplayResults(Troop playerTroop, bool isVictory)
    {
        Console.Clear();
        Console.WriteLine("=== BATTLE RESULTS ===");
        Console.WriteLine(isVictory ? "VICTORY!" : "DEFEAT!");
        Console.WriteLine("\nSurviving Characters:");

        int totalGold = 0;
        int totalItems = 0;

        foreach (Character character in playerTroop.Characters)
        {
            Console.WriteLine($"\n{character.Name} ({character.CharacterClass})");
            Console.WriteLine($"Final HP: {character.CurrentHp}/{character.MaxHp}");
            Console.WriteLine($"Level: {character.CurrentLevel}");

            // Display character's loot
            List<Item> items = character.Inventory.GetItems();
            int gold = character.Inventory.GetGold();

            Console.WriteLine("Loot acquired:");
            Console.WriteLine($"- Gold: {gold}");

            foreach (Item item in items)
            {
                Console.WriteLine($"- {item.Name} ({item.Rarity}) Value: {item.Value}");
                if (item.Type == ItemType.Equipment)
                {
                    Console.WriteLine(
                        $"  Bonuses: ATK+{item.Bonuses.AttackBonus} DEF+{item.Bonuses.DefenseBonus} HP+{item.Bonuses.HealthBonus}");
                }
            }

            totalGold += gold;
            totalItems += items.Count;
        }

        Console.WriteLine($"\nTotal Battle Gains:");
        Console.WriteLine($"- Gold: {totalGold}");
        Console.WriteLine($"- Items: {totalItems}");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}