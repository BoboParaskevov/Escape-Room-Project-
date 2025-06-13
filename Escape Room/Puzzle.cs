using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Puzzle
    {
        private string description;
        private string answer;
        private List<Item> reward;
        private bool solved;

        public string Description { get => description; set => description = value; }
        public string Answer { get => answer; set => answer = value; }
        public bool Solved { get => solved; set => solved = value; }
        internal List<Item> Reward { get => reward; set => reward = value; }

        public Puzzle(string description, string answer, List<Item> reward)
        {
            this.description = description;
            this.answer = answer;
            this.reward = reward;
            solved = false;
        }

        public void Start(List<Item> inventory, int puzzlesSolved)
        {
            Console.WriteLine(description);
            while (solved = false)
            {
                string guess = Console.ReadLine();
                if (guess == answer)
                {
                    Console.WriteLine("That's correct.");
                    solved = true;
                    foreach (Item reward in reward)
                    {
                        inventory.Add(reward);
                    }
                    puzzlesSolved++;
                }
                else
                {
                    Console.WriteLine("Not quite right.");
                }
            }
        }
    }
}
