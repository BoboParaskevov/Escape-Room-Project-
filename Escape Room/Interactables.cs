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
        private bool final;
        private string text;
        private Puzzle puzzle;
        private string puzzleSolved;

        public string Name { get => name; set => name = value; }
        public string Condition { get => condition; set => condition = value; }
        public string Without_condition { get => without_condition; set => without_condition = value; }
        public bool Final { get => final; set => final = value; }
        public string Text { get => text; set => text = value; }
        public Puzzle Puzzle { get => puzzle; set => puzzle = value; }
        public string PuzzleSolved { get => puzzleSolved; set => puzzleSolved = value; }

        public Interactable(string name, string condition, string without_condition, bool final)
        {
            this.name = name;
            this.condition = condition;
            this.without_condition = without_condition;
            this.final = final;
        }
    }
}
