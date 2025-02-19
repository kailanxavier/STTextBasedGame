using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static STTextBasedGame.AdventureGame;

namespace STTextBasedGame
{
    public static class GameHelpers
    {
        // Function to decide number of strawberries to drop when the wolf is defeated
        public static void StrawberryDropper(Player player, int difficulty)
        {
            // Note:
            // Since the difficulty only ranges from 1-3 this isn't a problem at the moment.
            // However remember to implement a Math.Ceiling in case you add an extreme difficulty,
            // otherwise it would always be rounded down to 0, dropping no strawberries which would kinda
            // defeat the point of even killing the wolf

            int strawberries = Math.Max(1, 5 / difficulty); // drops 5 when easy, 2 when medium and 1 when hard
            player.Strawberry += strawberries;
            WriteColoredLine($"\nYou defeated the wolf and looted it for {strawberries} strawberries!", ConsoleColor.Green);
        }

        // Helper to write coloured lines, has a lot of references and could break a lot of things
        public static void WriteColoredLine(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // Display player statistics
        public static void ShowStatistics(Player _player)
        {
            Console.WriteLine($"\n{_player.Name}'s statistics: ");
            Console.WriteLine($"Health: {_player.Health}");
            Console.WriteLine($"Strawberries: {_player.Strawberry}");
        }

        public static void DisplayWelcomeMessage(string playerName)
        {
            Console.WriteLine($"\nGreetings {playerName}, collect at least 10 strawberries to win.\n" +
                              "Once you have 10 or more strawberries, you may return to the kingdom.\n" +
                              "The more the merrier.\n" +
                              "But be careful... they are merciless.");
        }

        // Display main menu
        public static void DisplayMenu(bool isCooldownActive)
        {
            Console.WriteLine("\nWhat's your next move?\n" +
                              "\n1. Explore the forest" +
                              "\n2. Visit the village" +
                              (isCooldownActive ? "\n3. Rest (On Cooldown)" : "\n3. Rest") +
                              "\n4. Inventory" +
                              "\n5. Return to the kingdom" +
                              "\n6. Quit");
            Console.WriteLine("\nYour choice: ");
        }

        public static void EasterEgg()
        {
            WriteColoredLine("\nHow did you even find this?", ConsoleColor.Yellow);
            WriteColoredLine("You encounter A7X, they give you 100 strawberries.", ConsoleColor.Yellow);
        }

        public static void SetDifficulty()
        {
            // This displays the text for SetDifficulty
            Console.WriteLine("\nPlease choose a game difficulty: ");
            Console.WriteLine("\n1. Easy\n2. Medium\n3. Hard");
            Console.WriteLine("\nYour choice: ");
        }

        public static void GameOver()
        {
            WriteColoredLine("\nYou died. You were warned. They showed no mercy.", ConsoleColor.Red);
            string result = "Lost!";
            GameManager.GameHistory(result);
            WriteColoredLine("\nPress any key to close the window...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

    }
}
