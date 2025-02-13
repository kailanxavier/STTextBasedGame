using System;

namespace STTextBasedGame
{
    public class AdventureGame
    {
        private static Difficulty gameDifficulty;

        static void Main(string[] args)
        {
            var player = new Player { Name = GetValidPlayerName(), Health = 100, Strawberry = 0 };
            var inventory = new Inventory();

            DisplayWelcomeMessage(player.Name); // Welcome

            SetDifficulty(); // Set game difficulty

            var randomInstance = new Random();
            var gameManager = new GameManager(player, inventory, gameDifficulty, randomInstance);
            gameManager.Start();
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
                    GameHelpers.WriteColoredLine("Your name can't be empty. Try again.", ConsoleColor.Red);
                }
            }
            while (string.IsNullOrWhiteSpace(name));

            return name;
        }

        private static void DisplayWelcomeMessage(string playerName)
        {
            Console.WriteLine($"\nGreetings {playerName}, collect at least 10 strawberries to win.\n" +
                              "Once you have 10 or more strawberries, you may return to the kingdom.\n" +
                              "The more the merrier.\n" +
                              "But be careful... they are merciless.");
        }

        private static void SetDifficulty()
        {
            Console.WriteLine("\nPlease choose a game difficulty: ");
            Console.WriteLine("\n1. Easy\n2. Medium\n3. Hard");
            Console.WriteLine("\nYour choice: ");

            if (Enum.TryParse(Console.ReadLine(), out Difficulty difficulty) && Enum.IsDefined(typeof(Difficulty), difficulty))
            {
                gameDifficulty = difficulty;
            }
            else
            {
                GameHelpers.WriteColoredLine("Invalid choice. Default difficulty is easy.", ConsoleColor.Red);
                gameDifficulty = Difficulty.Easy;
            }
        }

        // Enum for game difficulty
        public enum Difficulty { Easy = 1, Medium, Hard };
    }
}