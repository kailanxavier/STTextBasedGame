using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STTextBasedGame.AdventureGame;

namespace STTextBasedGame
{
    public static class GameHelpers
    {
        public static void StrawberryDropper(Player player, int difficulty)
        {
            // If we ever implement an extra hard difficulty rememmber to add a Math.Ceiling to prevent the int from being rounded down
            int strawberries = Math.Max(1, 5 / difficulty); // Drops fewer strawberries as difficulty increases
            player.Strawberry += strawberries;
            WriteColoredLine($"\nYou defeated the wolf and looted it for {strawberries} strawberries!", ConsoleColor.Green);
        }

        public static void WriteColoredLine(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}
