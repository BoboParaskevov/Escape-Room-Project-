using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Room
    {
        private string description;
        private string new_description;
        private List<string> interactions = new List<string>(){ "investigate", "open" };
        private List<List<Interactable>> interactables;
        private int puzzlesSolved = 0;
        private bool solved = false;

        public string Description { get => description; set => description = value; }
        public string New_description { get => new_description; set => new_description = value; }
        public int PuzzlesSolved { get => puzzlesSolved; set => puzzlesSolved = value; }
        public bool Solved { get => solved; set => solved = value; }
        public List<string> Interactions { get => interactions; set => interactions = value; }
        public List<List<Interactable>> Interactables { get => interactables; set => interactables = value; }

        public Room()
        {
        }

        public void Start(Player player)
        {
            Console.WriteLine(description);
            while (solved == false)
            {
                string input = Console.ReadLine().ToLower();
                if (input.Contains("investigate"))
                {
                    ActionInput(input, player, 0);
                }
                else if (input.Contains("open"))
                {
                    ActionInput(input, player, 1);
                }
                else if (input == "room description")
                {
                    Console.WriteLine(description);
                }
                else if (input == "rules and mechanics")
                {
                    Console.WriteLine("Rules and Mechanics:\nIn this game you will be able to perform certain actions by typing them and the object you want to interact with.\nThose actions are:\nInvestigate - look at and search a certain object\nOpen - try to open something\nYou will have an inventory with items. You can see those items by typing \"Check Inventory\"\nThe last important thing is that if you want to see the description of the room you are in again you just need to type \"Room Description\"");
                }
                else if (input == "check inventory")
                {
                    Console.WriteLine(player.CheckInventory());
                }
                else
                {
                    Console.WriteLine("This input does nothing");
                }
            }
        }

        public void ActionInput(string input, Player player, int n)
        {
            input = input.Substring(input.IndexOf(" ") + 1);
            bool found = false;
            foreach (Interactable interactable in interactables[n])
            {
                if (input == interactable.Name)
                {
                    found = true;
                    if (interactable.Condition == "always")
                    {
                        if (interactable.Used == true)
                        {
                            Console.WriteLine(interactable.After_use);
                        }
                        else if (interactable.Puzzle != null)
                        {
                            if (interactable.Text != null)
                            {
                                Console.WriteLine(interactable.Text);
                            }
                            interactable.Puzzle.Start(player.Inventory);
                            puzzlesSolved++;
                            if (interactable.Item != null)
                            {
                                player.Add(interactable.Item);
                            }
                            if (interactable.Final == true)
                            {
                                Console.WriteLine("Exiting Room");
                                solved = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine(interactable.Text);
                            if (interactable.Item != null)
                            {
                                player.Add(interactable.Item);
                            }
                            if (interactable.Final == true)
                            {
                                Console.WriteLine("Exiting Room");
                                solved = true;
                            }
                        }
                        interactable.Used = true;
                    }
                    else if (int.TryParse(interactable.Condition, out int number))
                    {
                        bool foundItem = false;
                        if (int.Parse(interactable.Condition) == puzzlesSolved)
                        {
                            WithCondition(player, interactable);
                            foundItem = true;
                            if (interactable.Item != null)
                            {
                                player.Add(interactable.Item);
                            }
                            interactable.Used = true;
                        }
                        if (!foundItem)
                        {
                            Console.WriteLine(interactable.Without_condition);
                        }
                    }
                    else
                    {
                        bool foundItem = false;
                        for (int i = 0; i < player.Inventory.Count; i++) 
                        {
                            if (player.Inventory[i].Name.ToLower() == interactable.Condition)
                            {
                                foundItem = true;
                                if (player.Inventory[i].Disappear == true)
                                {
                                    player.Inventory.RemoveAt(i);
                                    interactable.Condition = "always";
                                }
                                WithCondition(player, interactable);
                                if (interactable.Item != null)
                                {
                                    player.Add(interactable.Item);
                                }
                                interactable.Used = true;
                                continue;
                            }
                        }
                        if (!foundItem)
                        {
                            Console.WriteLine(interactable.Without_condition);
                        }
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("This input does nothing");
            }
        }

        public void WithCondition(Player player, Interactable interactable)
        {
            if (interactable.Puzzle != null)
            {

                if (interactable.Text != null)
                {
                    Console.WriteLine(interactable.Text);
                }
                interactable.Puzzle.Start(player.Inventory);
                puzzlesSolved++;
                if (interactable.Final == true)
                {
                    Console.WriteLine("Exiting Room");
                    solved = true;
                }
            }
            else
            {
                Console.WriteLine(interactable.Text);
                if (interactable.Final == true)
                {
                    Console.WriteLine("Exiting Room");
                    solved = true;
                }
            }
        }
    }
}