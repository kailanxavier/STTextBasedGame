using System;
using System.Diagnostics;
using System.Dynamic;

namespace STTextBasedGame
{
    class AdventureGame
    {
        private static readonly Random RandomInstance = new();
        private static int gameDifficulty;

#pragma warning disable
        private static Inventory inventory;

        static void Main(string[] args)
        {
            Player player = new() { Name = "", Health = 100, Strawberry = 0 };
            Inventory inventory = new();

            player.Name = GetValidPlayerName();
            Console.WriteLine($"\nGreetings {player.Name}, collect at least 10 strawberries to win.\n" +
                "Once you have 10 or more strawberries you may return to the kingdom.\n" + 
                "The more the merrier.\n" + "But be careful... they are merciless.");

            SetDifficulty();

            bool isPlaying = true;
            while (isPlaying)
            {
                if (player.Health <= 0) // Lose condition
                {
                    Console.WriteLine("You were warned. They showed no mercy.");
                    break;
                }

                ShowStatistics(player);
                Console.WriteLine("\nWhat's your next move?\n" + 
                    "\n1. Explore the forest" +
                    "\n2. Visit the village" +
                    "\n3. Rest" +
                    "\n4. Inventory" +
                    "\n5. Return to the kindgom" +
                    "\n6. Quit");

                Console.WriteLine("\nYour choice: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
#pragma warning disable
                        player = Explore(player, inventory);
                        break;
                    case "2":
                        player = VisitVillage(player);
                        break;
                    case "3":
                        player = Rest(player);
                        break;
                    case "4":
                        inventory.ListItems();
                        break;
                    case "5":
                        EndGame(player);
                        isPlaying = false;
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    case "6661":
                        GameHelpers.WriteColoredLine("\nHow did you even find this?", ConsoleColor.Yellow);
                        GameHelpers.WriteColoredLine("You encounter A7X, they give you 100 strawberries.", ConsoleColor.Yellow);
                        player.Strawberry += 100;
                        break;
                    default:
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        private static Player EndGame(Player player)
        {
            if (player.Strawberry >= 10)
            {
                GameHelpers.WriteColoredLine("\nYou returned to the kingdom and brought all your friends strawberries." +
                    "\nYou win!", ConsoleColor.Green);
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou returned to the kingdom but you don't have enough strawberries. Jack eats you." +
                    "\nYou lose!", ConsoleColor.Red);
            }
            return player;
        }

        private static Player Rest(Player player)
        {
            GameHelpers.WriteColoredLine("\nYou rest for a few hours..." + 
                "\nYou gain 10 health.", ConsoleColor.Green);
            player.Health = Math.Min(player.Health + 10, 100);
            return player;
        }

        private static Player VisitVillage(Player player)
        {
            GameHelpers.WriteColoredLine("\nYou are offered a health potion in a exchange for 1 strawberry. " + 
                "\nWould you like to purchase the potion? (yes/no)", ConsoleColor.Blue);
            string? playerChoice = Console.ReadLine().ToLower();
            if (playerChoice == "yes")
            {
                if (player.Strawberry > 0)
                {
                    GameHelpers.WriteColoredLine("\nYou trade 1 strawberry for the potion and restore 20 health.", ConsoleColor.Green);
                    player.Health = Math.Min(player.Health + 20, 100);
                    player.Strawberry -= 1;
                }
                else
                {
                    GameHelpers.WriteColoredLine("\nYou don't have enough strawberries to complete this trade", ConsoleColor.Red);
                }
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou decline the offer and leave.", ConsoleColor.Red);
            }
            return player;
        }

        private static Player Explore(Player player, Inventory inventory)
        {
            Console.WriteLine("You go deep into the forest...");

            int randomEncounter = RandomInstance.Next(1, 7);
            switch (randomEncounter)
            {
                case 1:
                    player = WolfEncounter(player, inventory);
                    break;
                case 2:
                    player = FireSerpentEncounter(player);
                    break;
                case 3:
                    player = PolarBearEncounter(player, inventory);
                    break;
                case 4:
                    player = MindWizardEncounter(player, inventory);
                    break;
                case 5:
                    Console.WriteLine("\nYou find 1 lost strawberry.");
                    player.Strawberry++;
                    break;
                case 6:
                    string randomItemName = "";
                    int randomItem = RandomInstance.Next(1, 6);
                    switch (randomItem)
                    {
                        case 1:
                            randomItemName = "Coca-Cola Bottle";
                            inventory.AddItem(new Item(randomItemName, "This can be used to please the polar bear."));
                            break;
                        case 2:
                            randomItemName = "Tin Foil Hat";
                            inventory.AddItem(new Item(randomItemName, "This can be used agaisnt the mind wizard."));
                            break;
                        case 3:
                            randomItemName = "Enchanted Sword";
                            inventory.AddItem(new Item(randomItemName, "This can be used agaisnt the giant wolf if you choose to attack."));
                            break;
                        case 4:
                            randomItemName = "Laughing Gas Grenade";
                            inventory.AddItem(new Item(randomItemName, "This can be used against the mind wizard"));
                            break;
                        case 5:
                            randomItemName = "Banana Peel Launcher";
                            inventory.AddItem(new Item(randomItemName, "This item can be used to escape any enemy."));
                            break;
                    }
                    GameHelpers.WriteColoredLine($"You open the chest and find: {randomItemName}", ConsoleColor.Green);
                    break;
                default:
                    Console.WriteLine("\nNothing happens.");
                    break;
            }

            return player;
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

        private static void DisplayErrorMessage()
        {
            Console.WriteLine("Please choose a valid option.");
        }

        static Player WolfEncounter(Player player, Inventory inventory)
        {
            GameHelpers.WriteColoredLine("\nYou find a giant wolf. But she seems friendly." +
                "\nWould you like to attack the wolf? (yes/no)", ConsoleColor.Yellow);
            string? playerChoice = Console.ReadLine()?.ToLower();
            if (playerChoice == "yes")
            {
                bool hasSword = inventory.Items.Any(x => x.Name.ToLower() == "enchanted sword");
                if (hasSword)
                {
                    GameHelpers.StrawberryDropper(player, gameDifficulty);
                }
                else
                {
                    GameHelpers.WriteColoredLine("\nYou attack the wolf and she attacks you back." +
                        "\nYou lose 20 health.", ConsoleColor.Red);
                    player.Health -= 20;
                }
            }
            else Console.WriteLine("\nYou cower back to safety unscathed.");

            return player;
        }

        static Player FireSerpentEncounter(Player player)
        {
            GameHelpers.WriteColoredLine("\nYou encounter a serpent of fire. She attacks you.", ConsoleColor.Red);
            GameHelpers.WriteColoredLine("\nYou lose 40 health, but she spares your life.", ConsoleColor.Red);
            player.Health -= 40;

            return player;
        }

        static Player MindWizardEncounter(Player player, Inventory inventory)
        {
            GameHelpers.WriteColoredLine("\nYou encounter a mind wizard.\n", ConsoleColor.Gray);
            bool hasTinFoil = inventory.Items.Any(x => x.Name.ToLower() == "tin foil hat");
            bool hasLauncher = inventory.Items.Any(x => x.Name.ToLower() == "banana peel launcher");
            if (hasTinFoil)
            {
                GameHelpers.WriteColoredLine("\nThe mind wizard is not able to control your mind because of your awesome tin foil hat..." +
                    "\nYou snatch one of his strawberries and exit swiftly." +
                    "\nUnfortunately your hat was destroyed in battle.", ConsoleColor.Green);
                player.Strawberry++;
                inventory.RemoveItem("tin foil hat");
            }
            else if (hasLauncher)
            {
                GameHelpers.WriteColoredLine("\nYou use your banana peel launcher and the mind wizard slips and falls comically." +
                    "\nUnfortunately you are not able to steal any strawberries.", ConsoleColor.Cyan);
                inventory.RemoveItem("banana peel launcher");
            }
            else
            {
                GameHelpers.WriteColoredLine("You have no way of defending yourself. The mind wizard tricks you into giving him a strawberry and slaps you" +
                    "\nYou lose 5 health.", ConsoleColor.Red);
                if (player.Strawberry > 0) player.Strawberry--;
                player.Health -= 5;
            }

            return player;
        }

        static Player PolarBearEncounter(Player player, Inventory inventory)
        {
            GameHelpers.WriteColoredLine("\nYou encounter a polar bear." +
                "\nYes in a random forest. Crazy I know.", ConsoleColor.Yellow);
            bool hasCocaCola = inventory.Items.Any(x => x.Name.ToLower() == "coca-cola bottle");
            bool hasLaughingGas = inventory.Items.Any(x => x.Name.ToLower() == "laughing gas grenade");

            if (hasCocaCola)
            {
                GameHelpers.WriteColoredLine("\nYou offer the bear a coca-cola." +
                    "\nThe bear gladly takes it and gives you 1 strawberry in exchange.", ConsoleColor.Green);
                inventory.RemoveItem("coca-cola bottle");
                player.Strawberry++;
            }
            else if (hasLaughingGas)
            {
                GameHelpers.WriteColoredLine("\nYou throw the laughing gas grenade on the ground." +
                    "\nYou flee while the enemy is laughing uncontrollably. However, you get no strawberries.", ConsoleColor.Cyan);
                inventory.RemoveItem("laughing gas grenade");
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou have nothing to offer the bear. It attacks you and you lose 20 health.", ConsoleColor.Red);
                player.Health -= 20;
            }

            return player;
        }
        
    }
}