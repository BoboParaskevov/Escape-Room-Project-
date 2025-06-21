using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
           internal class Player
    {
        public List<Item> Inventory { get; private set; } = new List<Item>();

        public bool HasItem(string name) => Inventory.Exists(i => i.Name == name);

        public void AddItem(Item item)
        {
            if (!HasItem(item.Name))
            {
                Inventory.Add(item);
                Console.WriteLine($"[Item added to inventory: {item.Name}]");
            }
        }
    }
}

