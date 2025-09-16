namespace RPGTroopBattleGame;

public static class MainMenu
{
    public static void Menu()
    {
        Console.WriteLine("Welcome to the RPG Troop Battle Game!\nPlease press any key to start.");
        Console.ReadKey();
        Console.Clear();
        StartMenu();
    }

    private static void StartMenu()
    {
        Console.Clear();
        Console.WriteLine(
            "Please choose an option:\n1 - Create default troop\n2 - Create troop manually");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                GameManager.InitializeTroops(true);
                ChooseAction();
                break;
            case "2":
                GameManager.InitializeTroops(false);
                ChooseAction();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                StartMenu(); // Loop back to allow another choice
                break;
        }
    }

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
                GameManager.GameLoop();
                break;
            case "2":
                StartMenu();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                ChooseAction(); // Loop back to allow another choice
                break;
        }
    }
}