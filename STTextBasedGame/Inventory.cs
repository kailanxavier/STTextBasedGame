using System;
using System.Collections.Generic;
using System.Linq;

namespace STTextBasedGame
{
    public class Inventory
    {
        private List<Item> Items { get; } = new();

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(string itemName)
        {
            Items.RemoveAll(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasItem(string itemName)
        {
            return Items.Any(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public void ListItems()
        {
            if (Items.Count == 0)
            {
                GameHelpers.WriteColoredLine("\nYou have no items.", ConsoleColor.Yellow);
                return;
            }
                GameHelpers.WriteColoredLine("\nYour inventory: ", ConsoleColor.Yellow);
                foreach (var item in Items)
                {
                    Console.WriteLine($"{item.Name}: {item.Description}");
                }
        }
    }
}
