namespace RPGTroopBattleGame;

public class Troop
{
    private List<Character> _characters = new List<Character>();

    public List<Character> Characters
    {
        get { return _characters; }
    }

    int maxCharacters = 6;

    public void CreateTroop()
    {
        int numberOfCharacters = 3;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            string characterClass = "";

            // First, choose a character class
            Console.WriteLine($"Choose class for character {i + 1}:");
            Console.WriteLine("1 - Mage (Low HP, High Attack)");
            Console.WriteLine("2 - Rogue (Medium HP, High Critical Chance)");
            Console.WriteLine("3 - Witch (Low HP, Debuff Abilities)");
            Console.WriteLine("4 - Knight (High HP, Balanced Attack/Defense)");
            Console.WriteLine("5 - Priest (Low HP, Healing Abilities)");
            Console.WriteLine("6 - Berserk (High HP, High Attack, Low Defense)");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxCharacters)
            {
                Console.WriteLine($"Invalid choice. Please enter a number between 1 and {maxCharacters}:");
            }

            string classChoice = choice.ToString();

            // Pre-populate with default values based on class
            string name = "";
            int healthPoints = 0;
            int attackPower = 0;
            int defensePower = 0;
            int criticalHitChance = 0;
            int damageMultiplier = 0;

            switch (classChoice)
            {
                case "1": // Mage (Low HP, High Attack)
                    characterClass = "Mage";
                    name = "Hans";
                    healthPoints = 50;
                    attackPower = 30;
                    defensePower = 5;
                    criticalHitChance = 10;
                    break;

                case "2": // Rogue (Medium HP, High Crit)
                    characterClass = "Rogue";
                    name = "Karl";
                    healthPoints = 100; // medium
                    attackPower = 12; // modest
                    defensePower = 10;
                    criticalHitChance = 25; // emphasize crit
                    break;

                case "3": // Witch (Low HP, Debuff)
                    characterClass = "Witch";
                    name = "Bojena";
                    healthPoints = 60; // low
                    attackPower = 18;
                    defensePower = 8;
                    criticalHitChance = 5;
                    break;

                case "4": // Knight (High HP, Balanced)
                    characterClass = "Knight";
                    name = "Henry";
                    healthPoints = 150; // high
                    attackPower = 20;
                    defensePower = 20;
                    criticalHitChance = 5;
                    break;

                case "5": // Priest (Low HP, Healing)
                    characterClass = "Priest";
                    name = "Boguta";
                    healthPoints = 70; // low
                    attackPower = 10;
                    defensePower = 10;
                    criticalHitChance = 5;
                    break;

                case "6": // Berserk (High HP, High Attack, Low Defense)
                    characterClass = "Berserk";
                    name = "Ragnar";
                    healthPoints = 140; // high
                    attackPower = 28; // high
                    defensePower = 6; // low
                    criticalHitChance = 10;
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
            }

            // Create and add character
            Character character = new Character(name, healthPoints, attackPower, defensePower, criticalHitChance,
                characterClass);
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
        _characters.Add(new Character("Knight", 150, 20, 20, 5, "Knight"));
        _characters.Add(new Character("Rogue", 100, 10, 10, 20, "Rogue"));
        _characters.Add(new Character("Priest", 50, 30, 5, 10, "Priest"));
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
            int hp, attack, defense, critChance;

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
                    break;

                case "Villain":
                case "Orc":
                case "Wolf":
                    // Medium enemies
                    hp = (int)(40 * difficultyScalar);
                    attack = (int)(20 * difficultyScalar);
                    defense = (int)(15 * difficultyScalar);
                    critChance = 10;
                    break;

                case "Dark Knight":
                case "Cultist":
                case "Skeleton":
                    // Stronger enemies
                    hp = (int)(35 * difficultyScalar);
                    attack = (int)(25 * difficultyScalar);
                    defense = (int)(12 * difficultyScalar);
                    critChance = 15;
                    break;

                default:
                    hp = (int)(35 * difficultyScalar);
                    attack = (int)(18 * difficultyScalar);
                    defense = (int)(12 * difficultyScalar);
                    critChance = 10;
                    break;
            }

            // Add random variation to make battles less predictable (+/- 10%)
            hp = (int)(hp * (0.9 + random.NextDouble() * 0.2));
            attack = (int)(attack * (0.9 + random.NextDouble() * 0.2));
            defense = (int)(defense * (0.9 + random.NextDouble() * 0.2));

            // Create and add the enemy
            _characters.Add(new Character(enemyType, hp, attack, defense, critChance, enemyType));
        }
    }

    public void DisplayTroop(bool isFinalState = false)
    {
        if (isFinalState)
        {
            foreach (Character character in _characters)
                Console.WriteLine(character.FinalState());
        }
        else
        {
            int memberNumber = 0;
            foreach (Character character in _characters)
            {
                if (character.CurrentHp > 0)
                {
                    memberNumber++;
                    Console.WriteLine($"Member {memberNumber}: {character.Name}");
                    Console.WriteLine($"Class: {character.CharacterClass}");
                    Console.WriteLine($"HP: {character.CurrentHp}");
                    Console.WriteLine($"Attack: {character.AttackPower}");
                    Console.WriteLine($"Defense: {character.DefensePower}"); // fixed label
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