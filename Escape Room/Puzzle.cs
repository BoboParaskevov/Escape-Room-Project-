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
        private string congratulation;
        private List<Item> reward;
        private bool solved = false;

        public string Description { get => description; set => description = value; }
        public string Answer { get => answer; set => answer = value; }
        public bool Solved { get => solved; set => solved = value; }
        public List<Item> Reward { get => reward; set => reward = value; }
        public string Congratulation { get => congratulation; set => congratulation = value; }

        public Puzzle(string description, string answer, string congratulation, List<Item> reward)
        {
            this.description = description;
            this.answer = answer;
            this.congratulation = congratulation;
            this.reward = reward;
        }

        public void Start(List<Item> inventory)
        {
            Console.WriteLine(description);
            while (solved == false)
            {
                string guess = Console.ReadLine();
                if (guess.ToLower() == answer)
                {
                    Console.WriteLine(congratulation);
                    solved = true;
                    foreach (Item reward in reward)
                    {
                        inventory.Add(reward);
                    }
                }
                else
                {
                    Console.WriteLine("Not quite right.");
                }
            }
        }
    }
}