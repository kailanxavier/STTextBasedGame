using System;
using System.Runtime.CompilerServices;

namespace STTextBasedGame
{
    public class AdventureGame
    {
        private static Difficulty gameDifficulty;

        static async Task Main(string[] args)
        {
            // Initialize player and inventory
            var player = new Player { Name = GetValidPlayerName(), Health = 100, Strawberry = 0 };
            var inventory = new Inventory();

            GameHelpers.DisplayWelcomeMessage(player.Name);

            SetDifficulty(); // Set game difficulty

            var randomInstance = new Random();
            var gameManager = new GameManager(player, inventory, gameDifficulty, randomInstance);

            // Call start function from GameManager script
            await gameManager.StartAsync();
        }

        // Get string from console
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

        private static void SetDifficulty()
        {
            GameHelpers.SetDifficulty();

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