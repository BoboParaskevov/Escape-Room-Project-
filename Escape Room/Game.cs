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

        internal Player Player { get => player; set => player = value; }
        internal List<Room> Rooms { get => rooms; set => rooms = value; }
        internal List<Item> MergedItems { get => mergedItems; set => mergedItems = value; }

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
                Item item = new Item (name, description, disappear);
                mergedItems.Add(item);
            }
            roomNames.AddRange(new List<string>() { "Childhood Room.txt"});
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
                    else if (substring == "Open")
                    {
                        string interName = line.Substring(line.IndexOf(":") + 2, line.IndexOf(";") - line.IndexOf(":") + 2);
                        line = line.Substring(line.IndexOf(";") + 2);
                        string condition = line.Substring(0, line.IndexOf(";"));
                        line = line.Substring(line.IndexOf(";") + 2);
                        string without_condition = line.Substring(0, line.IndexOf(";"));
                        line = line.Substring(line.IndexOf(";") + 2);
                        Interactable interactable = new Interactable(interName, condition, without_condition); 
                        if (line.Substring(0, line.IndexOf(":")) == "Text")
                        {
                            line = line.Substring(line.IndexOf(':') + 2);
                            if (line.Contains("Puzzle"))
                            {
                                string text = line.Substring(0, line.IndexOf(";"));
                                line = line.Substring(line.IndexOf(':') + 2);
                                string puzzleDescription = line.Substring(0, line.IndexOf(";"));
                                line = line.Substring(line.IndexOf(";") + 2);
                                string answer = line.Substring(0, line.IndexOf(';'));
                                line = line.Substring(line.IndexOf(";") + 2);
                                List<Item> reward = new List<Item>();
                                Puzzle puzzle = new Puzzle(puzzleDescription, answer, reward);
                                if (line.Contains("),"))
                                {
                                    string itemName1 = line.Substring(0, line.IndexOf(" -"));
                                    line = line.Substring(line.IndexOf("-") + 2);
                                    string itemDescription1 = line.Substring(0, line.IndexOf("(") - 1);
                                    string disappear1 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                    Item item1 = new Item(itemName1, itemDescription1, disappear1);
                                    reward.Add(item1);
                                    line = line.Substring(line.IndexOf(",") + 2);
                                    string itemName2 = line.Substring(0, line.IndexOf(" -"));
                                    line = line.Substring(line.IndexOf("-") + 2);
                                    string itemDescription2 = line.Substring(0, line.IndexOf("(") - 1);
                                    string disappear2 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                    Item item2 = new Item(itemName2, itemDescription2, disappear2);
                                    reward.Add(item2);
                                }
                                else
                                {
                                    string itemName = line.Substring(0, line.IndexOf(" -"));
                                    line = line.Substring(line.IndexOf("-") + 2);
                                    string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
                                    string disappear = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                    Item item = new Item(itemName, itemDescription, disappear);
                                    reward.Add(item);
                                }
                                line = line.Substring(line.IndexOf(");") + 3);
                                string puzzleSolved = line.Substring(0);
                                interactable.Text = text;
                                interactable.Puzzle = puzzle;
                                interactable.PuzzleSolved = puzzleSolved;
                                interactables[1].Add(interactable);
                            }
                            else
                            {
                                string text = line.Substring(0);
                                interactable.Text = text;
                                interactables[1].Add(interactable);
                            }
                        }
                        else if (line.Substring(0, line.IndexOf(":")) == "Puzzle")
                        {
                            line = line.Substring(line.IndexOf(':') + 2);
                            string puzzleDescription = line.Substring(0, line.IndexOf(";"));
                            line = line.Substring(line.IndexOf(";") + 2);
                            string answer = line.Substring(0, line.IndexOf(';'));
                            line = line.Substring(line.IndexOf(";") + 2);
                            List<Item> reward = new List<Item>();
                            Puzzle puzzle = new Puzzle(puzzleDescription, answer, reward);
                            if (line.Contains("),"))
                            {
                                string itemName1 = line.Substring(0, line.IndexOf(" -"));
                                line = line.Substring(line.IndexOf("-") + 2);
                                string itemDescription1 = line.Substring(0, line.IndexOf("(") - 1);
                                string disappear1 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                Item item1 = new Item(itemName1, itemDescription1, disappear1);
                                reward.Add(item1);
                                line = line.Substring(line.IndexOf(",") + 2);
                                string itemName2 = line.Substring(0, line.IndexOf(" -"));
                                line = line.Substring(line.IndexOf("-") + 2);
                                string itemDescription2 = line.Substring(0, line.IndexOf("(") - 1);
                                string disappear2 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                Item item2 = new Item(itemName2, itemDescription2, disappear2);
                                reward.Add(item2);                            }
                            else
                            {
                                string itemName = line.Substring(0, line.IndexOf(" -"));
                                line = line.Substring(line.IndexOf("-") + 2);
                                string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
                                string disappear = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                Item item = new Item(itemName, itemDescription, disappear);
                                reward.Add(item);
                            }
                            line = line.Substring(line.IndexOf(");") + 3);
                            string puzzleSolved = line.Substring(0);
                            interactable.Puzzle = puzzle;
                            interactable.PuzzleSolved = puzzleSolved;
                            interactables[1].Add(interactable);
                        }
                    }
                    else if (substring == "Investigate")
                    {
                        string interName = line.Substring(line.IndexOf(":") + 2, line.IndexOf(";") - line.IndexOf(":") + 2);
                        line = line.Substring(line.IndexOf(";") + 2);
                        string condition = line.Substring(0, line.IndexOf(";"));
                        line = line.Substring(line.IndexOf(";") + 2);
                        string without_condition = line.Substring(0, line.IndexOf(";"));
                        line = line.Substring(line.IndexOf(";") + 2);
                        Interactable interactable = new Interactable(interName, condition, without_condition);
                        if (line.Substring(0, line.IndexOf(":")) == "Text")
                        {
                            line = line.Substring(line.IndexOf(':') + 2);
                            if (line.Contains("Puzzle"))
                            {
                                string text = line.Substring(0, line.IndexOf(";"));
                                line = line.Substring(line.IndexOf(':') + 2);
                                string puzzleDescription = line.Substring(0, line.IndexOf(";"));
                                line = line.Substring(line.IndexOf(";") + 2);
                                string answer = line.Substring(0, line.IndexOf(';'));
                                line = line.Substring(line.IndexOf(";") + 2);
                                List<Item> reward = new List<Item>();
                                Puzzle puzzle = new Puzzle(puzzleDescription, answer, reward);
                                if (line.Contains("),"))
                                {
                                    string itemName1 = line.Substring(0, line.IndexOf(" -"));
                                    line = line.Substring(line.IndexOf("-") + 2);
                                    string itemDescription1 = line.Substring(0, line.IndexOf("(") - 1);
                                    string disappear1 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                    Item item1 = new Item(itemName1, itemDescription1, disappear1);
                                    reward.Add(item1);
                                    line = line.Substring(line.IndexOf(",") + 2);
                                    string itemName2 = line.Substring(0, line.IndexOf(" -"));
                                    line = line.Substring(line.IndexOf("-") + 2);
                                    string itemDescription2 = line.Substring(0, line.IndexOf("(") - 1);
                                    string disappear2 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                    Item item2 = new Item(itemName2, itemDescription2, disappear2);
                                    reward.Add(item2);
                                }
                                else
                                {
                                    string itemName = line.Substring(0, line.IndexOf(" -"));
                                    line = line.Substring(line.IndexOf("-") + 2);
                                    string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
                                    string disappear = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                    Item item = new Item(itemName, itemDescription, disappear);
                                    reward.Add(item);
                                }
                                line = line.Substring(line.IndexOf(");") + 3);
                                string puzzleSolved = line.Substring(0);
                                interactable.Text = text;
                                interactable.Puzzle = puzzle;
                                interactable.PuzzleSolved = puzzleSolved;
                                interactables[0].Add(interactable);
                            }
                            else
                            {
                                string text = line.Substring(0);
                                interactable.Text = text;
                                interactables[0].Add(interactable);
                            }
                        }
                        else if (line.Substring(0, line.IndexOf(":")) == "Puzzle")
                        {
                            line = line.Substring(line.IndexOf(':') + 2);
                            string puzzleDescription = line.Substring(0, line.IndexOf(";"));
                            line = line.Substring(line.IndexOf(";") + 2);
                            string answer = line.Substring(0, line.IndexOf(';'));
                            line = line.Substring(line.IndexOf(";") + 2);
                            List<Item> reward = new List<Item>();
                            Puzzle puzzle = new Puzzle(puzzleDescription, answer, reward);
                            if (line.Contains("),"))
                            {
                                string itemName1 = line.Substring(0, line.IndexOf(" -"));
                                line = line.Substring(line.IndexOf("-") + 2);
                                string itemDescription1 = line.Substring(0, line.IndexOf("(") - 1);
                                string disappear1 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                Item item1 = new Item(itemName1, itemDescription1, disappear1);
                                reward.Add(item1);
                                line = line.Substring(line.IndexOf(",") + 2);
                                string itemName2 = line.Substring(0, line.IndexOf(" -"));
                                line = line.Substring(line.IndexOf("-") + 2);
                                string itemDescription2 = line.Substring(0, line.IndexOf("(") - 1);
                                string disappear2 = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                Item item2 = new Item(itemName2, itemDescription2, disappear2);
                                reward.Add(item2);
                            }
                            else
                            {
                                string itemName = line.Substring(0, line.IndexOf(" -"));
                                line = line.Substring(line.IndexOf("-") + 2);
                                string itemDescription = line.Substring(0, line.IndexOf("(") - 1);
                                string disappear = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")"));
                                Item item = new Item(itemName, itemDescription, disappear);
                                reward.Add(item);
                            }
                            line = line.Substring(line.IndexOf(");") + 3);
                            string puzzleSolved = line.Substring(0);
                            interactable.Puzzle = puzzle;
                            interactable.PuzzleSolved = puzzleSolved;
                            interactables[0].Add(interactable);
                        }
                    }
                    else if (line.Substring(0, line.IndexOf(":")) == "Key")
                    {
                        line = line.Substring(line.IndexOf(":") + 2);
                        string key = line.Substring(0);
                        room.Key = key;
                    }
                }
            }
        }

        public void Start()
        {
            Console.WriteLine("Rules and Mechanics:\nIn this game you will be able to perform certain actions by typing them and the object you want to interact with.Those actions are:\nInvestigate - look at and search a certain object\nOpen - try to open something\nYou will have an inventory with items. You can see those items by typing \"Check Inventory\"\nThe last important thing is that if you want to see the description of the room you are in again you just need to type \"Room Description\"");
            rooms[0].Start(player.Inventory);
            rooms[1].Start(player.Inventory);
            rooms[2].Start(player.Inventory);
        }

        public void Merge (string firstItem,  string secondItem)
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
