using System;
using System.Diagnostics;
using System.Dynamic;

namespace STTextBasedGame
{
    class AdventureGame
    {
        struct Player
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Strawberry { get; set; }
        }



        private static readonly Random RandomInstance = new();
        private static int gameDifficulty;
        private static bool enemyDead = false;

        static void Main(string[] args)
        {
            Player player = new Player { Name = "", Health = 100, Strawberry = 0 };

            player.Name = GetValidPlayerName();
            Console.WriteLine($"\nGreetings {player.Name}, collect at least 10 strawberries to win.");
            Console.WriteLine("Once you have 10 strawberries you may return to the kingdom.");
            Console.WriteLine("The more the merrier but be careful.");
            Console.WriteLine("But be careful... they are merciless.");

            SetDifficulty();

            bool isPlaying = true;
            while (isPlaying)
            {
                if (player.Health <= 0) // lose condition
                {
                    Console.WriteLine("You were warned. They showed no mercy.");
                    break;
                }

                ShowStatistics(player);
                Console.WriteLine("\nWhat's your next move?");
                Console.WriteLine("\n1. Explore the forest");
                Console.WriteLine("2. Visit the village");
                Console.WriteLine("3. Rest");
                Console.WriteLine("4. Return to the kingdom");
                Console.WriteLine("5. Quit");

                Console.WriteLine("\nYour choice: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        player = Explore(player);
                        break;
                    case "2":
                        player = VisitVillage(player);
                        break;
                    case "3":
                        player = Rest(player);
                        break;
                    case "4":
                        EndGame();
                        isPlaying = false;
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        private static void EndGame()
        {
            Player player = new();
            if (player.Strawberry >= 9)
            {
                Console.WriteLine("You returned to the kingdom and brought all your friends strawberries.");
            }
            else
            {
                Console.WriteLine("You returned to the kingdom but you don't have enough strawberries. Jack eats you.");
            }
        }

        private static Player Rest(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player VisitVillage(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player Explore(Player player)
        {
            Console.WriteLine("You go deep into the forest...");

            int randomEncounter = RandomInstance.Next(1, 7);
            switch (randomEncounter)
            {
                case 1:
                    ColouredLineHelper("\nYou find a giant wolf. But she seems friendly.", ConsoleColor.Cyan);
                    WolfEncounter();
                    break;
                case 2:
                    ColouredLineHelper("\nYou encounter a serpent of fire. She attacks you.", ConsoleColor.Red);
                    FireSerpentEncounter();
                    break;
                case 3:
                    Console.WriteLine("\nYou encounter a polar bear.");
                    PolarBearEncounter();
                    break;
                case 4:
                    Console.WriteLine("\nYou encounter a mind reader");
                    MindReaderEncounter();
                    break;
                case 5:
                    Console.WriteLine("\nYou find 1 lost strawberry.");
                    player.Strawberry++;
                    break;
                default:
                    Console.WriteLine("\nNothing happens.");
                    break;
            }

            return player;
        }

        private static void SetDifficulty()
        {
            Console.WriteLine("\nPlease choose a game difficulty: ");
            Console.WriteLine("\n1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");
            Console.WriteLine("\nYour choice: ");
            string choice = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    gameDifficulty = 1;
                    break;
                case "2":
                    gameDifficulty = 2;
                    break;
                case "3":
                    gameDifficulty = 3;
                    break;
                default:
                    DisplayErrorMessage();
                    break;
            }
        }


        static void ShowStatistics(Player player)
        {
            Console.WriteLine($"\n{player.Name}'s statistics: ");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Strawberries: {player.Strawberry}");
        }

        static string GetValidPlayerName()
        {
            string? name;
            do
            {
                Console.WriteLine("What is your name?");
                name = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Your name can't be empty. Try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(name));

            return name;
        }

        private static void DisplayErrorMessage()
        {
            Console.WriteLine("Please choose a valid option.");
        }

        
        static void WolfEncounter()
        {
            // Weakness: Little red riding hood
            // Ask if player has her in their inventory
            // Attack? If yes then decide if they do damage or take damage, and if killed
            // call strawberry dropper
            // Run?

            Console.WriteLine("Would you like to attack the wolf? (yes/no)");
            string? playerChoice = Console.ReadLine()?.ToLower();
            if (playerChoice == "yes")
            {

            }
            if (enemyDead)
            {
                StrawberryDropper();
                Console.WriteLine("You have killed the wolf.");
            }
        }

        static void FireSerpentEncounter()
        { 
            // Not winnable
            // But only does damage and stops the player from getting to the strawberry
            // Attack?
            // Run?
            Console.WriteLine("Serpent");
        }

        static void MindReaderEncounter()
        {
            // Ask if player has protective helmet
            // if not then the mind reader knows what they are up to and stops them
            // Attack?
            // Run?
            Console.WriteLine("Mind reader");
        }

        static void PolarBearEncounter()
        {
            // Weakness: Coca-cola
            // If the player has coca-cola they can give it the bear and it is entertained
            Console.WriteLine("Talking polar bear");
        }

        // Helper to decide how many strawberries should be dropped
        static void StrawberryDropper()
        {
            Player player = new();
            int maxDropChance = gameDifficulty == 2 ? 3 : gameDifficulty == 3 ? 4 : 5;
            int dropChance = RandomInstance.Next(1, maxDropChance + 1);

            if (dropChance == 1) player.Strawberry++;
        }

        static void ColouredLineHelper(string text, ConsoleColor colour)
        { 
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

// Check list:

// Text input?
// Variables?
// Arithmetic operations?
// Logic operations?
// String operations?
// Data structures?
// Conditional statements?
// Functions?
// Coding best practices?
/* 
To do:

- Implement village interaction
- Implement inventory
- Implement attack method
*/