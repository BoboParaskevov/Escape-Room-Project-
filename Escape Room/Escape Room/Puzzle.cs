using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
         internal class Puzzle
    {
        public string Question { get; }
        public string Solution { get; }
        public string SuccessText { get; }
        public bool Solved { get; private set; }

        public Puzzle(string question, string solution, string successText)
        {
            Question = question;
            Solution = solution.ToLower();
            SuccessText = successText;
            Solved = false;
        }

        public bool Attempt(string answer)
        {
            if (answer.ToLower() == Solution)
            {
                Console.WriteLine(SuccessText);
                Solved = true;
                return true;
            }
            else
            {
                Console.WriteLine("Wrong answer, try again.");
                return false;
            }
        }
    }
}

