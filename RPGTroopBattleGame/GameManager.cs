namespace RPGTroopBattleGame;

public static class GameManager
{
    private static Troop _playerTroop = new Troop();
    private static Troop _enemyTroop = new Troop();

    public static void InitializeTroops(bool isDefaultTroop)
    {
        // Reset troops
        _playerTroop = new Troop();
        _enemyTroop = new Troop();

        if (isDefaultTroop)
        {
            _playerTroop.InitializeDefaultTroop();
        }
        else
        {
            _playerTroop.CreateTroop();
        }

        _enemyTroop.InitializeEnemyTroop();
    }

    public static void GameLoop()
    {
        Console.Clear();
        bool isVictory = false;
        // Main game loop - continue until one side is defeated
        while (!(_playerTroop.GameOverCheck(true) || _enemyTroop.GameOverCheck(false)))
        {
            for (int i = 0; i < _playerTroop.Characters.Count; i++)
            {
                var character = _playerTroop.Characters[i];
                Combat.ProcessStatusEffects(character);
            }

            for (int i = 0; i < _enemyTroop.Characters.Count; i++)
            {
                var character = _enemyTroop.Characters[i];
                Combat.ProcessStatusEffects(character);
            }

            // Player turn - all characters act

            PlayerTurn();

            // Check if the enemy is defeated after the player turn
            if (_enemyTroop.GameOverCheck(false))
                break;

            // Enemy turn - all characters act
            EnemyTurn();

            // Check if a player is defeated after the enemy turn
            if (_playerTroop.GameOverCheck(true))
                break;

            // At the beginning of GameLoop or at the start of each round
        }

        isVictory = _enemyTroop.GameOverCheck(false);
        BattleResults.DisplayResults(_playerTroop, isVictory);
        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        MainMenu.Menu();
    }

    private static void PlayerTurn()
    {
        Console.Clear();
        Console.WriteLine("=== PLAYER TURN ===");
        Console.WriteLine("\nYour troop:");
        _playerTroop.DisplayTroop();
        Console.WriteLine("\nEnemy troop:");
        _enemyTroop.DisplayTroop();

        // Get alive player characters
        List<int> alivePlayerIndices = GetAliveCharacterIndices(_playerTroop.Characters);

        // Each live character gets one action
        foreach (int playerIndex in alivePlayerIndices)
        {
            Character playerCharacter = _playerTroop.Characters[playerIndex];
            Console.WriteLine($"\n--- {playerCharacter.Name}'s Action ---");

            // Check if the character is frozen
            if (playerCharacter.IsFrozen)
            {
                Console.WriteLine($"{playerCharacter.Name} is frozen and cannot act this turn!");
                continue; // Skip to the next character
            }

            Console.WriteLine(
                $"HP: {playerCharacter.CurrentHp} | ATK: {playerCharacter.AttackPower} | DEF: {playerCharacter.DefensePower} | CRIT: {playerCharacter.CriticalHitChance}%");

            // Get valid enemy targets
            List<int> aliveEnemyIndices = GetAliveCharacterIndices(_enemyTroop.Characters);
            if (aliveEnemyIndices.Count == 0)
            {
                Console.WriteLine("No enemies left to target!");
                break;
            }

            // Choose target
            Console.WriteLine("\nChoose enemy target:");
            for (int i = 0; i < aliveEnemyIndices.Count; i++)
            {
                int enemyIndex = aliveEnemyIndices[i];
                Character enemy = _enemyTroop.Characters[enemyIndex];
                Console.WriteLine($"{i + 1} - {enemy.Name} (HP: {enemy.CurrentHp})");
            }

            // Get valid player input for target selection
            int selection;
            do
            {
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out selection) || selection < 1 || selection > aliveEnemyIndices.Count)
                {
                    Console.WriteLine(
                        $"Invalid choice. Please enter a number between 1 and {aliveEnemyIndices.Count}:");
                }
            } while (selection < 1 || selection > aliveEnemyIndices.Count);

            int selectedEnemyIndex = aliveEnemyIndices[selection - 1];
            Character targetEnemy = _enemyTroop.Characters[selectedEnemyIndex];

            // Choose action
            Console.WriteLine("\nChoose action:");
            Console.WriteLine("1 - Attack");
            Console.WriteLine(
                $"2 - Special ability. {SpecialAbilities.GetDescription(playerCharacter.CharacterClass)}");

            int actionChoice;
            do
            {
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out actionChoice) || (actionChoice != 1 && actionChoice != 2))
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2:");
                }
            } while (actionChoice != 1 && actionChoice != 2);

            // Perform the selected action
            switch (actionChoice)
            {
                case 1:
                    // Attack
                    Combat.Attack(playerCharacter, targetEnemy);
                    break;
                case 2:
                    // Special ability
                    Combat.UseSpecialAbility(playerCharacter, targetEnemy, playerCharacter.Name,
                        playerCharacter.CurrentLevel);
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            // Update the display after each action
            Console.Clear();
            Console.WriteLine("=== PLAYER TURN ===");
            Console.WriteLine("\nYour troop:");
            _playerTroop.DisplayTroop();
            Console.WriteLine("\nEnemy troop:");
            _enemyTroop.DisplayTroop();
        }

        Console.WriteLine("\nAll your characters have acted. Press any key to end your turn...");
        Console.ReadKey();
    }

    private static void EnemyTurn()
    {
        Console.Clear();
        Console.WriteLine("=== ENEMY TURN ===");
        Console.WriteLine("\nYour troop:");
        _playerTroop.DisplayTroop();
        Console.WriteLine("\nEnemy troop:");
        _enemyTroop.DisplayTroop();

        // Get alive enemy characters
        List<int> aliveEnemyIndices = GetAliveCharacterIndices(_enemyTroop.Characters);
        Random random = new Random();

        // Each live enemy gets one action
        foreach (int enemyIndex in aliveEnemyIndices)
        {
            // Refresh live player list
            List<int> alivePlayerIndices = GetAliveCharacterIndices(_playerTroop.Characters);
            if (alivePlayerIndices.Count == 0)
            {
                Console.WriteLine("All your characters have been defeated!");
                break;
            }

            Character enemyCharacter = _enemyTroop.Characters[enemyIndex];
            Console.WriteLine($"\n--- Enemy {enemyCharacter.Name}'s Action ---");

            // Check if the character is frozen
            if (enemyCharacter.IsFrozen)
            {
                Console.WriteLine($"Enemy {enemyCharacter.Name} is frozen and cannot act this turn!");
                continue; // Skip to the next character
            }

            // Enemy AI - randomly select a target from live players
            int targetIndex = alivePlayerIndices[random.Next(alivePlayerIndices.Count)];
            Character targetPlayer = _playerTroop.Characters[targetIndex];

            Console.WriteLine($"Enemy {enemyCharacter.Name} targets {targetPlayer.Name}");

            // Enemy performs attack
            Combat.Attack(enemyCharacter, targetPlayer);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            // Update the display after each action
            Console.Clear();
            Console.WriteLine("=== ENEMY TURN ===");
            Console.WriteLine("\nYour troop:");
            _playerTroop.DisplayTroop();
            Console.WriteLine("\nEnemy troop:");
            _enemyTroop.DisplayTroop();
        }

        Console.WriteLine("\nAll enemy characters have acted. Press any key to end enemy turn...");
        Console.ReadKey();
    }

    // Helper method to get indices of live characters
    private static List<int> GetAliveCharacterIndices(List<Character> characters)
    {
        List<int> aliveIndices = new List<int>();
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].CurrentHp > 0)
            {
                aliveIndices.Add(i);
            }
        }

        return aliveIndices;
    }

    public static void DisplayPlayerTroopInfo()
    {
        _playerTroop.DisplayTroop();
    }
}