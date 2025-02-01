using System;

namespace STTextBasedGame
{
    class AdventureGame
    {
        public static string? playerName;
        private readonly static bool isPlaying = true;

        static void Main(string[] args)
        {
            if (isPlaying)
            {
                GetPlayerInfo();
                RandomEncounterGenerator();
            }
            else
            {
                return;
            }
        }

        static void GetPlayerInfo()
        {
            if (playerName == null)
            {
                Console.WriteLine("What is your name?");
                playerName = Console.ReadLine();
            }
            Console.WriteLine("Please choose a difficulty for your session: ");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");

            int playerChoice = Convert.ToInt32(Console.ReadLine());
            switch (playerChoice)
            {
                case 1:
                    Console.WriteLine($"{playerName}" + ", you have chosen to play the easy mode.");
                    break;
                case 2:
                    Console.WriteLine($"{playerName}" + ", you have chosen to play the medium mode.");
                    break;
                case 3:
                    Console.WriteLine($"{playerName}" + ", you have chosen to play the hard mode.");
                    break;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    break;
            }
        }

        static void RandomEncounterGenerator()
        {
            Random random = new();
            int randomEncounter = random.Next(1, 6);
            switch (randomEncounter)
            {
                case 1:
                    WolfEncounter();
                    break;
                case 2:
                    IronGolemEncounter();
                    break;
                case 3:
                    FireSerpentEncounter();
                    break;
                case 4:
                    MindReaderEncounter();
                    break;
                case 5:
                    TalkingPolarBear();
                    break;
            }
        }

        static void WolfEncounter()
        {
            // Weakness: Little red riding hood
            // Ask if player has the hood
            Console.WriteLine("Wolf");
        }

        static void IronGolemEncounter()
        {
            // Rust potion? : dies
            Console.WriteLine("Iron Golem");
        }

        static void FireSerpentEncounter()
        { 
            // Not winnable
            // But only does damage and stops the player from getting to the strawberry
            Console.WriteLine("Serpent");
        }

        static void MindReaderEncounter()
        {
            // Ask if player has protective helmet
            // if not then the mind reader knows what they are up to and stops them
            Console.WriteLine("Mind reader");
        }

        static void TalkingPolarBear()
        {
            // Weakness: Coca-cola
            // If the player has coca-cola they can give it the bear and it is entertained
            Console.WriteLine("Talking polar bear");
        }
    }
}