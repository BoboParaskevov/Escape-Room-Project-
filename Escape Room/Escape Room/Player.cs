using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
       internal class Player
    {
        private List<Item> inventory;

        internal List<Item> Inventory { get => inventory; set => inventory = value; }

        public Player(List<Item> inventory)
        {
            this.inventory = inventory;
        }

        public void Add(Item item)
        {
            inventory.Add(item);
            Console.WriteLine($"[Inventory Updated] You acquired: {item.Name}");
        }

        public void Remove(Item item)
        {
            inventory.Remove(item);
            Console.WriteLine($"[Inventory Updated] You lost: {item.Name}");
        }

        public string CheckInventory()
        {
            if (inventory.Count == 0) return "Your inventory is empty.";
            string result = "Your inventory contains:\n";
            foreach (Item item in inventory)
            {
                result += $"- {item.Name}: {item.Description}\n";
            }
            return result;
        }
    }
}
