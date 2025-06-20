using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Game
    {
        private Player player;
        private List<Room> rooms = new List<Room>(3);
        private List<Item> mergedItems;
        private List<string> roomNames;

        public Player Player { get => player; set => player = value; }
        public List<Room> Rooms { get => rooms; set => rooms = value; }
        public List<Item> MergedItems { get => mergedItems; set => mergedItems = value; }

        public Game()
        {
            List<Item> inventory = new List<Item>();
            player = new Player(inventory);
            //StreamReader sr = new StreamReader("merged_items.txt");
            //while (!sr.EndOfStream)
            //{
            //    string line = sr.ReadLine();
            //    string name = line.Substring(0, line.IndexOf(" ") - 1);
            //    string description = line.Substring(line.IndexOf("-") + 2, line.IndexOf(";") - line.IndexOf("-") + 2);
            //    string disappear = line.Substring(line.IndexOf(";"));
            //    Item item = new Item(name, description, disappear);
            //    mergedItems.Add(item);
            //}
            //roomNames.Add("C:\\Users\\matei\\source\\repos\\Escape Room\\Escape Room\\bin\\Debug\\net8.0\\Childhood Room.txt");
            //for (int i = 0; i < 3; i++)
            //{
                Room room = new Room();
                rooms.Add(room);
                List<List<Interactable>> interactables = new List<List<Interactable>>();
                List<Interactable> investigates = new List<Interactable>();
                List<Interactable> opens = new List<Interactable>();
                interactables.Add(investigates);
                interactables.Add(opens);
                room.Interactables = interactables;
                StreamReader rr = new StreamReader("Childhood_Room.txt");
                while (!rr.EndOfStream)
                {
                    string line = rr.ReadLine();
                    string substring = line.Substring(0, line.IndexOf(":"));
                    if (substring == "Description")
                    {
                        string roomDescription = line.Substring(line.IndexOf(":") + 2).Replace(@"\n", Environment.NewLine);
                        room.Description = roomDescription;
                    }
                    else if (substring == "Yes")
                    {
                        room.New_description = room.Description;
                    }
                    else if (substring == "No")
                    {
                        string new_description = line.Substring(line.IndexOf(":") + 2).Replace(@"\n", Environment.NewLine);
                        room.New_description = new_description;
                    }
                    else if (substring == "Investigate")
                    {
                        ReadAction(line, interactables, 0);
                    }
                    else if (substring == "Open")
                    {
                        ReadAction(line, interactables, 1);
                    }
                }
            //}
        }

        public void ReadAction(string line, List<List<Interactable>> interactables, int n)
        {
            line = line.Substring(line.IndexOf(":") + 2);
            string interName = line.Substring(0, line.IndexOf(";"));
            line = line.Substring(line.IndexOf(";") + 2);
            string condition = line.Substring(0, line.IndexOf(";"));
            line = line.Substring(line.IndexOf(";") + 2);
            string without_condition = line.Substring(0, line.IndexOf(";")).Replace(@"\n", Environment.NewLine);
            line = line.Substring(line.IndexOf(";") + 2);
            string after_use = line.Substring(0, line.IndexOf(";")).Replace(@"\n", Environment.NewLine);
            line = line.Substring(line.IndexOf(";") + 2);
            bool final = false;
            if (line.Substring(0, line.IndexOf(";")) == "true")
            {
                final = true;
            }
            Interactable interactable = new Interactable(interName, condition, without_condition, after_use, final);
            line = line.Substring(line.IndexOf(";") + 2);
            if (line.Substring(0, line.IndexOf(";")) != " ")
            {
                List<Item> item = new List<Item>();
                ReadReward(line, item);
                interactable.Item = item[0];
            }
            line = line.Substring(line.IndexOf(";") + 2);
            if (line.Substring(0, line.IndexOf(":")) == "Text")
            {
                line = line.Substring(line.IndexOf(':') + 2);
                string text = line.Substring(0, line.IndexOf(";")).Replace(@"\n", Environment.NewLine);
                interactable.Text = text;
                if (line.Contains("Puzzle"))
                {
                    ReadPuzzle(line, interactable);
                }
            }
            else if (line.Substring(0, line.IndexOf(":")) == "Puzzle")
            {
                ReadPuzzle(line, interactable);
            }
            interactables[n].Add(interactable);
        }

        public void ReadPuzzle(string line, Interactable interactable)
        {
            List<Item> reward = new List<Item>();
            line = line.Substring(line.IndexOf(":") + 2);
            string puzzleDescription = line.Substring(0, line.IndexOf(";")).Replace(@"\n", Environment.NewLine);
            line = line.Substring(line.IndexOf(";") + 2);
            string answer = line.Substring(0, line.IndexOf(";"));
            line = line.Substring(line.IndexOf(";") + 2);
            string congratulation = line.Substring(0, line.IndexOf(";")).Replace(@"\n", Environment.NewLine);
            line = line.Substring(line.IndexOf(";") + 2);
            if (line.Substring(0, line.IndexOf(";")) == "Yes")
            {
                line = line.Substring(line.IndexOf(';') + 2);
                ReadReward(line, reward);
                if (line.Contains("),"))
                {
                    line = line.Substring(line.IndexOf(",") + 2);
                    ReadReward(line, reward);
                }
            }
            Puzzle puzzle = new Puzzle(puzzleDescription, answer, congratulation, reward);
            interactable.Puzzle = puzzle;
        }

        public void ReadReward(string line, List<Item> reward)
        {
            string itemName = line.Substring(0, line.IndexOf(" -"));
            line = line.Substring(line.IndexOf("-") + 2);
            string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
            line = line.Substring(line.IndexOf("(") + 1);
            string disappear = line.Substring(0, line.IndexOf(")"));
            Item item = new Item(itemName, itemDescription, disappear);
            reward.Add(item);
        }

        public void Start()
        {
            Console.WriteLine("Rules and Mechanics:\nIn this game you will be able to perform certain actions by typing them and the object you want to interact with.\nThose actions are:\nInvestigate - look at and search a certain object\nOpen - try to open something\nYou will have an inventory with items. You can see those items by typing \"Check Inventory\"\nThe last important thing is that if you want to see the description of the room you are in again you just need to type \"Room Description\"\n");
            rooms[0].Start(player);
            rooms[1].Start(player);
            rooms[2].Start(player);
        }

        public void Merge(string firstItem, string secondItem)
        {
            StreamReader sr = new StreamReader("merging.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                int number = 0;
                foreach (char c in line)
                {
                    if (c == '+')
                    {
                        number++;
                    }
                }
                if (number == 1)
                {
                    if (line.Substring(0, line.IndexOf(" ") - 1) == firstItem)
                    {
                        if (line.Substring(line.IndexOf("+") + 2, line.IndexOf("=") - 2) == secondItem)
                        {
                            foreach (Item item in mergedItems)
                            {
                                if (item.Name == line.Substring(line.IndexOf("=") + 2))
                                {
                                    player.Add(item);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You can't merge those items");
                }
            }
        }

        public void Merge(string firstItem, string secondItem, string thirdItem)
        {
            StreamReader sr = new StreamReader("merging.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                int number = 0;
                foreach (char c in line)
                {
                    if (c == '+')
                    {
                        number++;
                    }
                }
                if (number == 1)
                {
                    if (line.Substring(0, line.IndexOf(" ") - 1) == firstItem)
                    {
                        if (line.Substring(line.IndexOf("+") + 2, line.IndexOf("=") - 2) == secondItem)
                        {
                            string shortLine = line.Substring(line.IndexOf("+") + 1);
                            if (shortLine.Substring(shortLine.IndexOf("+") + 2, shortLine.IndexOf("=") - 2) == thirdItem)
                                foreach (Item item in mergedItems)
                                {
                                    if (item.Name == line.Substring(line.IndexOf("=") + 2))
                                    {
                                        player.Add(item);
                                    }
                                }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You can't merge those items");
                }
            }
        }

        public string PrintDescription(Room room)
        {
            return room.Description;
        }

        public string PrintInventory()
        {
            string inventory = string.Empty;
            foreach (Item item in player.Inventory)
            {
                inventory += $"{item}\n";
            }
            return inventory;
        }
    }
}