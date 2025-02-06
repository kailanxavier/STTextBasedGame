using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTextBasedGame
{
    public static class GameHelpers
    {
        public static Player StrawberryDropper(Player player, int gameDifficulty)
        {
            int dropChance = gameDifficulty switch
            {
                1 => 100,
                2 => 66,
                3 => 33,
                _ => 0
            };

            int randomNum = new Random().Next(1, 101);

            if (randomNum <= dropChance)
            {
                WriteColoredLine("\nYou have defeated the enemy and received a strawberry.", ConsoleColor.Green);
                player.Strawberry++;
            }
            else
            {
                WriteColoredLine("\nYou have defeated the enemy but no strawberries were dropped.", ConsoleColor.Red);
            }
            return player;
        }
        public static void WriteColoredLine(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}
