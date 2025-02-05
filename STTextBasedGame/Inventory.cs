using System;
using System.Collections.Generic;
using System.Linq;

namespace STTextBasedGame
{
    public class Inventory
    {
        // using a list here because the length needs to be edited uwu
        private List<Item> items;

        public Inventory()
        {
            items = [];
        }

        public void AddItem(Item item)
        {
            items.Add(item);

        }

        public void RemoveItem(string itemName)
        { 
            var item = items.Find(x => x.Name.ToLower() == itemName.ToLower());
            if (item != null) items.Remove(item);
        }

        public void ListItems()
        {
            if (items.Count == 0)
            {
                GameHelpers.WriteColoredLine("\nYou have no items.", ConsoleColor.Yellow);
            }
            else
            {
                GameHelpers.WriteColoredLine("\nInventory: ", ConsoleColor.Yellow);
                foreach (var item in items)
                {
                    Console.WriteLine("\n" + item);
                }
            }
        }

        public List<Item> Items
        { 
            get { return items; }
        }
    }
}
