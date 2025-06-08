using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Item
    {
        private string name;
        private string description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public Item(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override string ToString()
        {
            return name + " - " + description;
        }
    }
}
