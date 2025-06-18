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
        private List<Room> rooms;
        private List<Item> mergedItems;
        private List<string> roomNames;

        public Player Player { get => player; set => player = value; }
        public List<Room> Rooms { get => rooms; set => rooms = value; }
        public List<Item> MergedItems { get => mergedItems; set => mergedItems = value; }

        public Game()
        {
            List<Item> inventory = new List<Item>();
            player = new Player(inventory);
            StreamReader sr = new StreamReader("merged_items.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string name = line.Substring(0, line.IndexOf(" ") - 1);
                string description = line.Substring(line.IndexOf("-") + 2, line.IndexOf(";") - line.IndexOf("-") + 2);
                string disappear = line.Substring(line.IndexOf(";"));
                Item item = new Item(name, description, disappear);
                mergedItems.Add(item);
            }
            roomNames.AddRange(new List<string>() { "Childhood Room.txt" });
            for (int i = 0; i < 3; i++)
            {
                Room room = new Room();
                rooms.Add(room);
                StreamReader rr = new StreamReader(roomNames[i]);
                while (!rr.EndOfStream)
                {
                    List<List<Interactable>> interactables = new List<List<Interactable>>(2);
                    room.Interactables = interactables;
                    string line = rr.ReadLine();
                    string substring = line.Substring(0, line.IndexOf(":"));
                    if (substring == "Description")
                    {
                        string roomDescription = line.Substring(line.IndexOf(":") + 2);
                        room.Description = roomDescription;
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
            }
        }

        public static void ReadAction(string line, List<List<Interactable>> interactables, int n)
        {
            string interName = line.Substring(line.IndexOf(":") + 2, line.IndexOf(";") - line.IndexOf(":") + 2);
            line = line.Substring(line.IndexOf(";") + 2);
            string condition = line.Substring(0, line.IndexOf(";"));
            line = line.Substring(line.IndexOf(";") + 2);
            string without_condition = line.Substring(0, line.IndexOf(";"));
            line = line.Substring(line.IndexOf(";") + 2);
            bool final = false;
            if (line.Substring(0, line.IndexOf(";")) == "true")
            {
                final = true;
            }
            Interactable interactable = new Interactable(interName, condition, without_condition, final);
            line = line.Substring(line.IndexOf(";") + 2);
            if (line.Substring(0, line.IndexOf(":")) == "Text")
            {
                line = line.Substring(line.IndexOf(':') + 2);
                string text = line.Substring(0, line.IndexOf(";"));
                interactable.Text = text;
                if (line.Contains("Puzzle"))
                {
                    List<Item> reward = new List<Item>();
                    line = line.Substring(line.IndexOf(":") + 2);
                    string puzzleDescription = line.Substring(0, line.IndexOf(";"));
                    line = line.Substring(line.IndexOf(";") + 2);
                    string answer = line.Substring(0, line.IndexOf(";"));
                    line = line.Substring(line.IndexOf(";") + 2);
                    string congratulation = line.Substring(0, line.IndexOf(";"));
                    line = line.Substring(line.IndexOf(";") + 2);
                    string itemName = line.Substring(0, line.IndexOf(" -"));
                    line = line.Substring(line.IndexOf("-") + 2);
                    string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
                    string disappear = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                    Item item = new Item(itemName, itemDescription, disappear);
                    reward.Add(item);
                    Puzzle puzzle = new Puzzle(puzzleDescription, answer, congratulation, reward);
                    if (line.Contains("),"))
                    {
                        line = line.Substring(line.IndexOf(",") + 2);
                        string itemName2 = line.Substring(0, line.IndexOf(" -"));
                        line = line.Substring(line.IndexOf("-") + 2);
                        string itemDescription2 = line.Substring(0, line.IndexOf("(") - 1);
                        string disappear2 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                        Item item2 = new Item(itemName2, itemDescription2, disappear2);
                        reward.Add(item2);
                    }
                    line = line.Substring(line.IndexOf(";") + 2);
                    string puzzleSolved = line.Substring(0);
                    interactable.Puzzle = puzzle;
                    interactable.PuzzleSolved = puzzleSolved;
                }
            }
            else if (line.Substring(0, line.IndexOf(":")) == "Puzzle")
            {
                List<Item> reward = new List<Item>();
                line = line.Substring(line.IndexOf(":") + 2);
                string puzzleDescription = line.Substring(0, line.IndexOf(";"));
                line = line.Substring(line.IndexOf(";") + 2);
                string answer = line.Substring(0, line.IndexOf(";"));
                line = line.Substring(line.IndexOf(";") + 2);
                string congratulation = line.Substring(0, line.IndexOf(";"));
                line = line.Substring(line.IndexOf(";") + 2);
                string itemName = line.Substring(0, line.IndexOf(" -"));
                line = line.Substring(line.IndexOf("-") + 2);
                string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
                string disappear = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                Item item = new Item(itemName, itemDescription, disappear);
                reward.Add(item);
                Puzzle puzzle = new Puzzle(puzzleDescription, answer, congratulation, reward);
                if (line.Contains("),"))
                {
                    line = line.Substring(line.IndexOf(",") + 2);
                    string itemName2 = line.Substring(0, line.IndexOf(" -"));
                    line = line.Substring(line.IndexOf("-") + 2);
                    string itemDescription2 = line.Substring(0, line.IndexOf("(") - 1);
                    string disappear2 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                    Item item2 = new Item(itemName2, itemDescription2, disappear2);
                    reward.Add(item2);
                }
                line = line.Substring(line.IndexOf(";") + 2);
                string puzzleSolved = line.Substring(0);
                interactable.Puzzle = puzzle;
                interactable.PuzzleSolved = puzzleSolved;
            }
            interactables[n].Add(interactable);
        }

        public void Start()
        {
            Console.WriteLine("Rules and Mechanics:\nIn this game you will be able to perform certain actions by typing them and the object you want to interact with.Those actions are:\nInvestigate - look at and search a certain object\nOpen - try to open something\nYou will have an inventory with items. You can see those items by typing \"Check Inventory\"\nThe last important thing is that if you want to see the description of the room you are in again you just need to type \"Room Description\"");
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