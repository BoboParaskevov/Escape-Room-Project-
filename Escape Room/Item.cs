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
        private bool disappear;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool Disappear { get => disappear; set => disappear = value; }

        public Item(string name, string description, string disappear)
        {
            this.name = name;
            this.description = description;
            this.disappear = false;
            if (disappear == "true")
            {
                this.disappear = true;
            }
        }

        public override string ToString()
        {
            return name + " - " + description;
        }
    }
}
