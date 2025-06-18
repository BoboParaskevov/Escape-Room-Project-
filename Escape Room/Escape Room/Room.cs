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
        private List<string> interactions;
        private List<List<Interactable>> interactables;
        private int puzzlesSolved;
        private bool solved;

        public string Description { get => description; set => description = value; }
        public List<string> Interactions { get => interactions; set => interactions = value; }
        public List<List<Interactable>> Interactables { get => interactables; set => interactables = value; }
        public int PuzzlesSolved { get => puzzlesSolved; set => puzzlesSolved = value; }
        public bool Solved { get => solved; set => solved = value; }
        

        public Room()
        {
            interactions.AddRange(new List<string> { "investigate", "open" });
            puzzlesSolved = 0;
            solved = false;
        }

        public void Start(List<Item> inventory)
        {
            Console.WriteLine(description);
            while (solved == false)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "investigate")
                {
                    input = input.Substring(input.IndexOf(" ") + 1);
                    foreach (Interactable interactable in interactables[0])
                    {
                        if (input == interactable.Name)
                        {
                            if (interactable.Condition == "always")
                            {
                                if (interactable.Puzzle != null)
                                {
                                    if (interactable.Puzzle.Solved == true)
                                    {
                                        Console.WriteLine(interactable.PuzzleSolved);
                                    }
                                    else
                                    {
                                        if (interactable.Text != null)
                                        {
                                            Console.WriteLine(interactable.Text);
                                        }
                                        interactable.Puzzle.Start(inventory, puzzlesSolved);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(interactable.Text);
                                }
                            }
                            else
                            {
                                foreach (Item item in inventory)
                                {
                                    if (item.Name == interactable.Condition)
                                    {
                                        if (item.Disappear == true)
                                        {
                                            inventory.Remove(item);
                                            interactable.Condition = "always";
                                        }
                                        if (interactable.Puzzle != null)
                                        {
                                            if (interactable.Puzzle.Solved == true)
                                            {
                                                Console.WriteLine(interactable.PuzzleSolved);
                                            }
                                            else
                                            {
                                                if (interactable.Text != null)
                                                {
                                                    Console.WriteLine(interactable.Text);
                                                }
                                                interactable.Puzzle.Start(inventory, puzzlesSolved);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(interactable.Text);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the necessary items to perform this action");
                                    }
                                }
                            }
                        }
                    }
                }
                else if (input == "open")
                {
                    input = input.Substring(input.IndexOf(" ") + 1);
                    foreach (Interactable interactable in interactables[1])
                    {
                        if (input == interactable.Name)
                        {
                            if (interactable.Condition == "always")
                            {
                                if (interactable.Puzzle != null)
                                {
                                    if (interactable.Puzzle.Solved == true)
                                    {
                                        Console.WriteLine(interactable.PuzzleSolved);
                                    }
                                    else
                                    {
                                        if (interactable.Text != null)
                                        {
                                            Console.WriteLine(interactable.Text);
                                        }
                                        interactable.Puzzle.Start(inventory, puzzlesSolved);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(interactable.Text);
                                }
                            }
                            else
                            {
                                foreach (Item item in inventory)
                                {
                                    if (item.Name == interactable.Condition)
                                    {
                                        if (item.Disappear == true)
                                        {
                                            inventory.Remove(item);
                                            interactable.Condition = "always";
                                        }
                                        if (interactable.Puzzle != null)
                                        {
                                            if (interactable.Puzzle.Solved == true)
                                            {
                                                Console.WriteLine(interactable.PuzzleSolved);
                                            }
                                            else
                                            {
                                                if (interactable.Text != null)
                                                {
                                                    Console.WriteLine(interactable.Text);
                                                }
                                                interactable.Puzzle.Start(inventory, puzzlesSolved);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(interactable.Text);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the necessary items to perform this action");
                                    }
                                }
                            }
                        }
                    }
                }
                else if (input == "room description")
                {
                    Console.WriteLine(description);
                }
                else if  (input == "rules and mechanics")
                {
                    Console.WriteLine("rules and mechanics(not added)");
                }
                else
                {
                    Console.WriteLine("This input does nothing");
                }
                if (puzzlesSolved == 3)
                {
                    bool check = false;
                    foreach (Item item in inventory)
                    {
                        if (key == item.Name)
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        solved = true;
                        Thread.Sleep(8000);
                        Console.Write("Exiting Room");
                        Thread.Sleep(1000);
                        Console.Write(".");
                        Thread.Sleep(1000);
                        Console.Write(".");
                        Thread.Sleep(1000);
                        Console.WriteLine(".");
                        Thread.Sleep(3000);
                    }
                }
            }
        }
    }
}
