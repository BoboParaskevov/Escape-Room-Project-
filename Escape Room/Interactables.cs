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
        private string text;
        private Puzzle puzzle;
        private string without_condition;
        private string puzzleSolved;

        public string Name { get => name; set => name = value; }
        public string Condition { get => condition; set => condition = value; }
        public string Text { get => text; set => text = value; }
        internal Puzzle Puzzle { get => puzzle; set => puzzle = value; }
        public string Without_condition { get => without_condition; set => without_condition = value; }
        public string PuzzleSolved { get => puzzleSolved; set => puzzleSolved = value; }

        public Interactable(string name, string condition, string without_condition)
        {
            this.name = name;
            this.condition = condition;
            this.without_condition = without_condition;
        }
    }
}
