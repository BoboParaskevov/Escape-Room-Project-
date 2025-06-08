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
        }

        public void Remove(Item item)
        {
            inventory.Remove(item);
        }

        public string CheckInventory()
        {
            string a = string.Empty;
            foreach (Item item in inventory)
            {
                a += $"{item}\n";
            }
            return a;
        }
    }
}
