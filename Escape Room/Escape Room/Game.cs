using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Game
    {
        private Player player;
        private List<Room> rooms;
        private int location;
        private int roomsSolved;
        private List<Item> mergedItems;

        internal Player Player { get => player; set => player = value; }
        internal List<Room> Rooms { get => rooms; set => rooms = value; }
        public int Location { get => location; set => location = value; }
        internal List<Item> MergedItems { get => mergedItems; set => mergedItems = value; }

        public Game(Player player, List<Room> rooms)
        {
            this.player = player;
            this.rooms = rooms;
            location = 0;
            roomsSolved = 0;
            StreamReader sr = new StreamReader("merged_items.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string name = line.Substring(0, line.IndexOf(" ") - 1);
                string description = line.Substring(line.IndexOf("-") + 2);
                Item item = new Item (name, description);
                mergedItems.Add(item);
            }
        }

        public void Start()
        {
            Console.WriteLine("I want to play a game...(pravila)");
            EnterRoom(location);
            while (roomsSolved < rooms.Count)
            {
                Console.WriteLine("You've entered the next room");
                EnterRoom(location);
            }
            Console.WriteLine("You've escaped with the ... in your posession");
        }

        public void EnterRoom(int number)
        {
            Console.WriteLine(rooms[number].Description);
            bool checkInput = false;
            while (!rooms[number].Solved)
            {
                string input = Console.ReadLine();
                foreach (Puzzle puzzle in rooms[number].Puzzles)
                {
                    if (input == puzzle.Key)
                    {
                        checkInput = true;
                        Console.WriteLine(puzzle.Description);
                        while (!puzzle.Solved)
                        {
                            string guess = Console.ReadLine();
                            if (guess == puzzle.Answer)
                            {
                                puzzle.Solved = true;
                                player.Add(puzzle.Reward);
                                rooms[number].PuzzlesSolved++;
                                Console.WriteLine($"You got a {puzzle.Reward.Name} - {puzzle.Reward.Description}");
                            }
                            else
                            {
                                Console.WriteLine("Wrong answer");
                            }
                        }
                    }
                }
                if (input == rooms[number].Key.Name)
                {
                    checkInput = true;
                    bool posession = false;
                    foreach (Item item in player.Inventory)
                    {
                        if (item.Name == input)
                        {
                            posession = true;
                        }
                    }
                    if (posession == false)
                    {
                        Console.WriteLine("You do not have this item");
                        
                    }
                    else if (rooms[number].PuzzlesSolved < rooms[number].Puzzles.Count)
                    {
                        Console.WriteLine("There are still puzzles to solve in this room");
                    }
                    else
                    {
                        rooms[number].Solved = true;
                    }

                }
                else if (checkInput == false)
                {
                    Console.WriteLine("This input does nothing");
                }
            }
            Console.WriteLine("Exiting room...");
            location++;
            Thread.Sleep(3000);
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
    }
}
