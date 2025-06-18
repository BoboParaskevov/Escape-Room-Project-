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
        private int puzzlesSolved;
        private Item key;
        private bool solved;

        public string Description { get => description; set => description = value; }
        internal List<Puzzle> Puzzles { get => puzzles; set => puzzles = value; }
        public int PuzzlesSolved { get => puzzlesSolved; set => puzzlesSolved = value; }
        internal Item Key { get => key; set => key = value; }
        public bool Solved { get => solved; set => solved = value; }

        public Room(string description, List<Puzzle> puzzles, Item key)
        {
            this.description = description;
            this.puzzles = puzzles;
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
