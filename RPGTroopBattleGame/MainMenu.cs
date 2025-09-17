namespace RPGTroopBattleGame;

public static class MainMenu
{
    // === Entry Point ===
    public static void Menu()
    {
        Console.WriteLine("Welcome to the RPG Troop Battle Game!\nPlease press any key to start.");
        Console.ReadKey();
        Console.Clear();
        StartMenu();
    }

    // === Troop Creation Menu ===
    private static void StartMenu()
    {
        Console.Clear();
        Console.WriteLine("Please choose an option:\n1 - Create default troop\n2 - Create troop manually");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                GameManager.InitializeTroops(true);   // Use predefined troop
                ChooseAction();
                break;

            case "2":
                GameManager.InitializeTroops(false);  // Build troop manually
                ChooseAction();
                break;

            default:
                Console.WriteLine("Invalid choice.");
                StartMenu(); // Loop back to allow another choice
                break;
        }
    }

    // === Post-Setup Actions ===
    private static void ChooseAction()
    {
        Console.Clear();
        Console.WriteLine("Your troop info");
        GameManager.DisplayPlayerTroopInfo();

        Console.WriteLine("\n1 - Start Combat\n2 - Back");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                GameManager.GameLoop();  // Enter main game loop
                break;

            case "2":
                StartMenu();             // Go back to troop creation
                break;

            default:
                Console.WriteLine("Invalid choice.");
                ChooseAction();          // Loop back to allow another choice
                break;
        }
    }
}