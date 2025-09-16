namespace RPGTroopBattleGame;

public class Troop
{
    private List<Character> _characters = new List<Character>();

    public List<Character> Characters
    {
        get { return _characters; }
    }

    public void CreateTroop()
    {
        int numberOfCharacters = 3;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            string characterClass = "";

            // First, choose a character class
            Console.WriteLine($"Choose class for character {i + 1}:");
            Console.WriteLine("1 - Knight (High HP, Balanced Attack/Defense)");
            Console.WriteLine("2 - Rogue (Medium HP, High Critical Chance)");
            Console.WriteLine("3 - Mage (Low HP, High Attack)");

            string classChoice;
            do
            {
                classChoice = Console.ReadLine();
                if (classChoice != "1" && classChoice != "2" && classChoice != "3")
                {
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3:");
                }
            } while (classChoice != "1" && classChoice != "2" && classChoice != "3");

            // Pre-populate with default values based on class
            string name = "";
            int healthPoints = 0;
            int attackPower = 0;
            int defensePower = 0;
            int criticalHitChance = 0;
            int damageMultiplier = 0;

            switch (classChoice)
            {
                case "1":
                    characterClass = "Knight";
                    name = "Knight";
                    healthPoints = 150;
                    attackPower = 20;
                    defensePower = 20;
                    criticalHitChance = 5;
                    damageMultiplier = 2;
                    break;
                case "2":
                    characterClass = "Rogue";
                    name = "Rogue";
                    healthPoints = 100;
                    attackPower = 10;
                    defensePower = 10;
                    criticalHitChance = 20;
                    damageMultiplier = 4;
                    break;
                case "3":
                    characterClass = "Mage";
                    name = "Mage";
                    healthPoints = 50;
                    attackPower = 30;
                    defensePower = 5;
                    criticalHitChance = 10;
                    damageMultiplier = 3;
                    break;
            }

            // Allow customization of the name
            Console.WriteLine($"Enter a name for your {characterClass} (press Enter to use default '{name}'): ");
            string customName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(customName))
            {
                name = customName;
            }

            // Allow customization of stats within class limits
            Console.WriteLine("\nCustomize character stats? (y/n)");
            string customize = Console.ReadLine().ToLower();

            if (customize == "y")
            {
                // Health Points
                Console.WriteLine(
                    $"Health Points (default {healthPoints}, range {healthPoints - 20}-{healthPoints + 20}): ");
                string hpInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(hpInput) && int.TryParse(hpInput, out int customHP))
                {
                    // Limit HP to a reasonable range based on class
                    healthPoints = Math.Max(healthPoints - 20, Math.Min(customHP, healthPoints + 20));
                }

                // Attack Power
                Console.WriteLine($"Attack Power (default {attackPower}, range {attackPower - 5}-{attackPower + 5}): ");
                string atkInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(atkInput) && int.TryParse(atkInput, out int customAtk))
                {
                    // Limit attack to a reasonable range based on class
                    attackPower = Math.Max(attackPower - 5, Math.Min(customAtk, attackPower + 5));
                }

                // Defense Power
                Console.WriteLine(
                    $"Defense Power (default {defensePower}, range {defensePower - 5}-{defensePower + 5}): ");
                string defInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(defInput) && int.TryParse(defInput, out int customDef))
                {
                    // Limit defense to a reasonable range based on class
                    defensePower = Math.Max(defensePower - 5, Math.Min(customDef, defensePower + 5));
                }

                // Critical Hit Chance
                Console.WriteLine($"Critical Hit Chance (default {criticalHitChance}, range 1-30): ");
                string critInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(critInput) && int.TryParse(critInput, out int customCrit))
                {
                    // Limit crit chance to a reasonable range
                    criticalHitChance = Math.Max(1, Math.Min(customCrit, 30));
                }

                // Damage Multiplier
                Console.WriteLine($"Damage Multiplier (default {damageMultiplier}, range 2-5): ");
                string multInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(multInput) && int.TryParse(multInput, out int customMult))
                {
                    // Limit damage multiplier to a reasonable range
                    damageMultiplier = Math.Max(2, Math.Min(customMult, 5));
                }
            }

            // Create and add character
            Character character = new Character(name, healthPoints, attackPower, defensePower, criticalHitChance,
                damageMultiplier, characterClass);
            _characters.Add(character);

            Console.WriteLine($"\n{name} has been added to your troop!");
            Console.WriteLine(character.ToString());
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    public void InitializeDefaultTroop()
    {
        _characters.Clear();
        _characters.Add(new Character("Knight", 150, 20, 20, 5, 2, "Knight"));
        _characters.Add(new Character("Rogue", 100, 10, 10, 20, 4, "Rogue"));
        _characters.Add(new Character("Priest", 50, 30, 5, 10, 3, "Priest"));
    }

    public void InitializeEnemyTroop()
    {
        _characters.Clear();
        Random random = new Random();

        // Create a balanced enemy troop
        // Different enemy types for variety
        string[] enemyTypes =
        {
            "Bandit", "Villain", "Poacher", "Orc", "Goblin", "Skeleton",
            "Wolf", "Dark Knight", "Cultist"
        };

        // Get 3 random enemy types
        List<string> selectedEnemies = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string enemyType = enemyTypes[random.Next(enemyTypes.Length)];
            selectedEnemies.Add(enemyType);
        }

        // Add enemies with more balanced stats (not just HP=1)
        for (int i = 0; i < 3; i++)
        {
            string enemyType = selectedEnemies[i];
            int hp, attack, defense, critChance, damageMultiplier;

            // Scale enemy difficulty based on position (first is weakest, last is strongest)
            double easyDifficulty = 0.8;
            double mediumDifficulty = 1.0;
            double hardDifficulty = 1.2;
            double difficultyScalar = easyDifficulty + (i * 0.2); // 0.8, 1.0, 1.2

            // Base stats depend on enemy type
            switch (enemyType)
            {
                case "Bandit":
                case "Poacher":
                case "Goblin":
                    // Weaker enemies
                    hp = (int)(30 * difficultyScalar);
                    attack = (int)(15 * difficultyScalar);
                    defense = (int)(10 * difficultyScalar);
                    critChance = 5;
                    damageMultiplier = 2;
                    break;

                case "Villain":
                case "Orc":
                case "Wolf":
                    // Medium enemies
                    hp = (int)(40 * difficultyScalar);
                    attack = (int)(20 * difficultyScalar);
                    defense = (int)(15 * difficultyScalar);
                    critChance = 10;
                    damageMultiplier = 3;
                    break;

                case "Dark Knight":
                case "Cultist":
                case "Skeleton":
                    // Stronger enemies
                    hp = (int)(35 * difficultyScalar);
                    attack = (int)(25 * difficultyScalar);
                    defense = (int)(12 * difficultyScalar);
                    critChance = 15;
                    damageMultiplier = 3;
                    break;

                default:
                    hp = (int)(35 * difficultyScalar);
                    attack = (int)(18 * difficultyScalar);
                    defense = (int)(12 * difficultyScalar);
                    critChance = 10;
                    damageMultiplier = 2;
                    break;
            }

            // Add random variation to make battles less predictable (+/- 10%)
            hp = (int)(hp * (0.9 + random.NextDouble() * 0.2));
            attack = (int)(attack * (0.9 + random.NextDouble() * 0.2));
            defense = (int)(defense * (0.9 + random.NextDouble() * 0.2));

            // Create and add the enemy
            _characters.Add(new Character(enemyType, hp, attack, defense, critChance, damageMultiplier, enemyType));
        }
    }

    public void DisplayTroop(bool isFinalState = false)
    {
        if (isFinalState)
        {
            // Display final state for each character (alive or dead)
            foreach (var character in _characters)
            {
                Console.WriteLine(character.FinalState());
            }
        }
        else
        {
            // Display only living characters with numbers
            int livingCharacters = 0;
            foreach (var character in _characters)
            {
                if (character.CurrentHp > 0)
                {
                    livingCharacters++;
                    int index = _characters.IndexOf(character);
                    Console.WriteLine($"Member {livingCharacters}: {character.Name}");
                    Console.WriteLine($"Class: {character.CharacterClass}");
                    Console.WriteLine($"HP: {character.CurrentHp}");
                    Console.WriteLine($"Attack: {character.AttackPower}");
                    Console.WriteLine($"Defence: {character.DefensePower}");
                    Console.WriteLine($"Critical: {character.CriticalHitChance}%");
                    Console.WriteLine();
                }
            }
        }
    }

    public bool GameOverCheck(bool isPlayer)
    {
        int deadCharacters = 0;
        foreach (var character in _characters)
        {
            if (character.CurrentHp <= 0)
            {
                deadCharacters++;
            }
        }

        if (deadCharacters == _characters.Count)
        {
            if (isPlayer)
                Console.WriteLine("You have been defeated!");
            else
                Console.WriteLine("You won this battle!");

            return true;
        }

        return false;
    }

    // New method to support future item/equipment system
    public void ApplyLoot(int characterIndex, string lootItem)
    {
        // Future implementation for loot system
        // This would modify character stats based on equipment/items found
        Console.WriteLine($"{_characters[characterIndex].Name} found: {lootItem}");
    }
}