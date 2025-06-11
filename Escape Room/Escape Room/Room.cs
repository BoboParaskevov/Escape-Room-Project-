using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Room
    {
        private string description;
        private List<Puzzle> puzzles;
        private List<string> interactions;
        private List<List<string>> interactables;
        private int puzzlesSolved;
        private Item key;
        private bool solved;

        public string Description { get => description; set => description = value; }
        internal List<Puzzle> Puzzles { get => puzzles; set => puzzles = value; }
        public int PuzzlesSolved { get => puzzlesSolved; set => puzzlesSolved = value; }
        internal Item Key { get => key; set => key = value; }
        public bool Solved { get => solved; set => solved = value; }
        public List<string> Interactions { get => interactions; set => interactions = value; }
        public List<List<string>> Interactables { get => interactables; set => interactables = value; }

        public Room(string description, List<Puzzle> puzzles, Item key)
        {
            this.description = description;
            this.puzzles = puzzles;
            interactions.AddRange(new List<string> { "investigate", "open"});
            interactables = new List<List<string>>(2);
            this.key = key;
            puzzlesSolved = 0;
            solved = false;
        }

        public string ShowDescription()
        {
            return description;
        }
    }
}
