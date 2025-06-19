using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Interactable
    {
        private string name;
        private string condition;
        private string without_condition;
        private string after_use;
        private bool used = false;
        private bool final;
        private Item item;
        private string text;
        private Puzzle puzzle;

        public string Name { get => name; set => name = value; }
        public string Condition { get => condition; set => condition = value; }
        public string Without_condition { get => without_condition; set => without_condition = value; }
        public string After_use { get => after_use; set => after_use = value; }
        public bool Used { get => used; set => used = value; }
        public bool Final { get => final; set => final = value; }
        public Item Item { get => item; set => item = value; }
        public string Text { get => text; set => text = value; }
        public Puzzle Puzzle { get => puzzle; set => puzzle = value; }
        public Interactable(string name, string condition, string without_condition, string after_use, bool final)
        {
            this.name = name;
            this.condition = condition;
            this.without_condition = without_condition;
            this.after_use = after_use;
            this.final = final;
        }
    }
}
