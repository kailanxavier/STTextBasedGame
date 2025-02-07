using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STTextBasedGame.AdventureGame;

namespace STTextBasedGame
{
    public class GameManager
    {
        private readonly Player _player;
        private readonly Inventory _inventory;
        private readonly Difficulty _difficulty;
        private readonly Random _randomInstance;

        public GameManager(Player player, Inventory inventory, Difficulty difficulty, Random randomInstance)
        {
            _player = player;
            _inventory = inventory;
            _difficulty = difficulty;
            _randomInstance = randomInstance;

        }

        // Start the game
        public void Start()
        {
            bool isPlaying = true;
            while (isPlaying)
            {
                if (_player.Health <= 0) // Lose condition
                {
                    GameHelpers.WriteColoredLine("\nYou died." + "You were warned. They showed no mercy.", ConsoleColor.Red);
                    Console.ReadLine();
                    break;
                }

                ShowStatistics();
                DisplayMenu();

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        Explore();
                        break;
                    case "2":
                        VisitVillage();
                        break;
                    case "3":
                        Rest();
                        break;
                    case "4":
                        _inventory.ListItems();
                        break;
                    case "5":
                        EndGame();
                        isPlaying = false;
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    case "6661": // Easter egg
                        GameHelpers.WriteColoredLine("\nHow did you even find this?", ConsoleColor.Yellow);
                        GameHelpers.WriteColoredLine("You encounter A7X, they give you 100 strawberries.", ConsoleColor.Yellow);
                        _player.Strawberry += 100;
                        break;
                    default:
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        // Display player statistics
        private void ShowStatistics()
        {
            Console.WriteLine($"\n{_player.Name}'s statistics: ");
            Console.WriteLine($"Health: {_player.Health}");
            Console.WriteLine($"Strawberries: {_player.Strawberry}");
        }

        // Display main menu
        private static void DisplayMenu()
        {
            Console.WriteLine("\nWhat's your next move?\n" +
                              "\n1. Explore the forest" +
                              "\n2. Visit the village" +
                              "\n3. Rest" +
                              "\n4. Inventory" +
                              "\n5. Return to the kingdom" +
                              "\n6. Quit");
            Console.WriteLine("\nYour choice: ");
        }

        private void Rest()
        {
            GameHelpers.WriteColoredLine("\nYou rest for a few hours...\nYou gain 10 health.", ConsoleColor.Green);
            _player.Health = Math.Min(_player.Health + 10, 100);
        }

        private void VisitVillage()
        {
            GameHelpers.WriteColoredLine("\nYou are offered a health potion in exchange for 1 strawberry.\nWould you like to purchase the potion? (yes/no)", ConsoleColor.Blue);
            string? playerChoice = Console.ReadLine()?.ToLower();

            if (playerChoice == "yes")
            {
                if (_player.Strawberry > 0)
                {
                    GameHelpers.WriteColoredLine("\nYou trade 1 strawberry for the potion and restore 20 health.", ConsoleColor.Green);
                    _player.Health = Math.Min(_player.Health + 20, 100);
                    _player.Strawberry--;
                }
                else
                {
                    GameHelpers.WriteColoredLine("\nYou don't have enough strawberries to complete this trade.", ConsoleColor.Red);
                }
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou decline the offer and leave.", ConsoleColor.Red);
            }
        }

        private void Explore()
        {
            Console.WriteLine("You go deep into the forest...");

            var encounter = (EncounterType)_randomInstance.Next(0, 7);
            switch (encounter)
            {
                case EncounterType.Wolf:
                    WolfEncounter();
                    break;
                case EncounterType.FireSerpent:
                    FireSerpentEncounter();
                    break;
                case EncounterType.PolarBear:
                    PolarBearEncounter();
                    break;
                case EncounterType.MindWizard:
                    MindWizardEncounter();
                    break;
                case EncounterType.Strawberry:
                    Console.WriteLine("\nYou find 1 lost strawberry.");
                    _player.Strawberry++;
                    break;
                case EncounterType.Item:
                    var randomItem = GetRandomItem();
                    _inventory.AddItem(randomItem);
                    GameHelpers.WriteColoredLine($"You open the chest and find: {randomItem.Name}", ConsoleColor.Green);
                    break;
                default:
                    Console.WriteLine("\nNothing happens.");
                    break;
            }
        }

        private void WolfEncounter()
        {
            GameHelpers.WriteColoredLine("\nYou find a giant wolf. But she seems friendly.\nWould you like to attack the wolf? (yes/no)", ConsoleColor.Yellow);
            string? playerChoice = Console.ReadLine()?.ToLower();

            if (playerChoice == "yes")
            {
                if (_inventory.HasItem("enchanted sword"))
                {
                    GameHelpers.StrawberryDropper(_player, (int)_difficulty);
                }
                else
                {
                    GameHelpers.WriteColoredLine("\nYou attack the wolf and she attacks you back.\nYou lose 20 health.", ConsoleColor.Red);
                    _player.Health -= 20;
                }
            }
            else
            {
                Console.WriteLine("\nYou cower back to safety unscathed.");
            }
        }

        private void FireSerpentEncounter()
        {
            GameHelpers.WriteColoredLine("\nYou encounter a serpent of fire. She attacks you.\nYou lose 40 health, but she spares your life.", ConsoleColor.Red);
            _player.Health -= 40;
        }

        private void MindWizardEncounter()
        {
            GameHelpers.WriteColoredLine("\nYou encounter a mind wizard.\n", ConsoleColor.Gray);

            if (_inventory.HasItem("tin foil hat"))
            {
                GameHelpers.WriteColoredLine("\nThe mind wizard is not able to control your mind because of your awesome tin foil hat...\nYou snatch one of his strawberries and exit swiftly.\nUnfortunately, your hat was destroyed in battle.", ConsoleColor.Green);
                _player.Strawberry++;
                _inventory.RemoveItem("tin foil hat");
            }
            else if (_inventory.HasItem("banana peel launcher"))
            {
                GameHelpers.WriteColoredLine("\nYou use your banana peel launcher and the mind wizard slips and falls comically.\nUnfortunately, you are not able to steal any strawberries.", ConsoleColor.Cyan);
                _inventory.RemoveItem("banana peel launcher");
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou have no way of defending yourself. The mind wizard tricks you into giving him a strawberry and slaps you.\nYou lose 5 health.", ConsoleColor.Red);
                if (_player.Strawberry > 0) _player.Strawberry--;
                _player.Health -= 5;
            }
        }

        private void PolarBearEncounter()
        {
            GameHelpers.WriteColoredLine("\nYou encounter a polar bear.\nYes, in a random forest. Crazy, I know.", ConsoleColor.Yellow);

            if (_inventory.HasItem("coca-cola bottle"))
            {
                GameHelpers.WriteColoredLine("\nYou offer the bear a coca-cola.\nThe bear gladly takes it and gives you 1 strawberry in exchange.", ConsoleColor.Green);
                _inventory.RemoveItem("coca-cola bottle");
                _player.Strawberry++;
            }
            else if (_inventory.HasItem("laughing gas grenade"))
            {
                GameHelpers.WriteColoredLine("\nYou throw the laughing gas grenade on the ground.\nYou flee while the enemy is laughing uncontrollably. However, you get no strawberries.", ConsoleColor.Cyan);
                _inventory.RemoveItem("laughing gas grenade");
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou have nothing to offer the bear. It attacks you and you lose 20 health.", ConsoleColor.Red);
                _player.Health -= 20;
            }
        }

        private void EndGame()
        {
            if (_player.Strawberry >= 10)
            {
                GameHelpers.WriteColoredLine("\nYou returned to the kingdom and brought all your friends strawberries.\nYou win!", ConsoleColor.Green);
            }
            else
            {
                GameHelpers.WriteColoredLine("\nYou returned to the kingdom but you don't have enough strawberries. Jack eats you.\nYou lose!", ConsoleColor.Red);
            }

            GameHelpers.WriteColoredLine("\n\nPress enter/return to close the window...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        private static void DisplayErrorMessage()
        {
            Console.WriteLine("Please choose a valid option.");
        }

        private Item GetRandomItem()
        {
            var randomItems = new List<Item>
            {
                new("Coca-Cola Bottle", "This can be used to please the polar bear."),
                new("Tin Foil Hat", "This can be used against the mind wizard."),
                new("Enchanted Sword", "This can be used against the giant wolf if you choose to attack."),
                new("Laughing Gas Grenade", "This can be used against the polar bear."),
                new("Banana Peel Launcher", "This can be used against the mind wizard.")
            };

            return randomItems[_randomInstance.Next(randomItems.Count)];
        }
    }

    // Enum for encounter types
    public enum EncounterType { Wolf, FireSerpent, PolarBear, MindWizard, Strawberry, Item }
}
