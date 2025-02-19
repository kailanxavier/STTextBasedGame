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

        public static void WriteColoredLine(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}
