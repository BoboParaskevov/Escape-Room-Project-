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
    private List<Item> mergedItems = new List<Item>();

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

        if (File.Exists("merged_items.txt"))
        {
            using (StreamReader sr = new StreamReader("merged_items.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string name = line.Substring(0, line.IndexOf(" "));
                    string description = line.Substring(line.IndexOf("-") + 2);
                    Item item = new Item(name, description);
                    mergedItems.Add(item);
                }
            }
        }
    }

    public void Start()
    {
        EnterRoom(location);
        while (roomsSolved < rooms.Count)
        {
            Console.WriteLine("\nYou have another room to explore.");
            EnterRoom(location);
        }
        Console.WriteLine("Congratulations! You've completed all rooms.");
    }

    public void EnterRoom(int number)
    {
        Room currentRoom = rooms[number];
        Console.WriteLine("\n" + currentRoom.Description);

        bool roomCompleted = false;
        while (!roomCompleted)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Type puzzle key(wardrobe, chest, equation)");
            Console.WriteLine("2. Inventory");
            Console.WriteLine("3. Use Item(Bobby Pin, Equation, Key)");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "inventory")
            {
                Console.WriteLine(" ");
                Console.WriteLine(player.CheckInventory());
                continue;
            }

            if (input.StartsWith("use "))
            {
                string itemToUse = input.Substring(4).Trim();
                if (itemToUse == "bobby pin")
                {
                    Puzzle chestPuzzle = currentRoom.Puzzles.Find(p => p.Key == "chest");
                    if (chestPuzzle != null && !chestPuzzle.Solved)
                    {
                        bool hasBobbyPin = player.Inventory.Exists(i => i.Name.ToLower().Contains("bobby pin"));
                        if (hasBobbyPin)
                        {
                            chestPuzzle.Solved = true;
                            player.Add(chestPuzzle.Reward); 
                            Console.WriteLine("\nYou unlock the chest with the bobby pin. Inside, you find a math equation.");
                        }
                        else
                        {
                            Console.WriteLine("\nYou don't have a bobby pin to pick the lock.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThe chest is already unlocked or doesn't exist here.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nYou can't use {itemToUse} here.");
                }
                continue;
            }

            Puzzle puzzle = currentRoom.Puzzles.Find(p => p.Key.ToLower() == input);
            if (puzzle != null && !puzzle.Solved)
            {
                Console.WriteLine("\nPuzzle: " + puzzle.Description);
                bool solved = false;

                while (!solved)
                {
                    Console.Write("Enter your answer: ");
                    string answer = Console.ReadLine().Trim();

                    if (answer.Equals(puzzle.Answer, StringComparison.OrdinalIgnoreCase))
                    {
                        puzzle.Solved = true;
                        player.Add(puzzle.Reward);
                        Console.WriteLine($"\nCorrect! You received: {puzzle.Reward.Name} - {puzzle.Reward.Description}");
                        solved = true;

                        if (puzzle.Key == "equation")
                        {
                            Console.WriteLine("\nYou solved the math equation! You can now open the glass box.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThat's not correct. Try again.");
                    }
                }
            }
            else if (input == currentRoom.Key.Name.ToLower())
            {
                bool hasKey = player.Inventory.Exists(i => i.Name.ToLower() == currentRoom.Key.Name.ToLower());
                if (!hasKey)
                {
                    Console.WriteLine("\nThe door is locked. You need the key.");
                }
                else if (currentRoom.Puzzles.Exists(p => !p.Solved))
                {
                    Console.WriteLine("\nYou must solve all puzzles in this room before exiting.");
                }
                else
                {
                    Console.WriteLine("\nYou unlock the door and exit the room.");
                    currentRoom.Solved = true;
                    location++;
                    roomsSolved++;
                    roomCompleted = true;
                }
            }
            else if (input == "equation")
            {
                bool hasEquation = player.Inventory.Exists(i => i.Name.ToLower() == "equation");
                if (!hasEquation)
                {
                    Console.WriteLine("\nYou don't have the equation yet.");
                }
                else
                {
                    Console.WriteLine("\nLook at this series: 53, 53, 40, 40, 27, 27, … What number should come next?");
                    Console.Write("Your answer: ");
                    string eqAnswer = Console.ReadLine().Trim();

                    if (eqAnswer == "14")
                    {
                        Console.WriteLine("\nCorrect! You solved the equation and find a key inside the glass box.");
                        
                        Item key = new Item("Key", "The key to your bedroom.");
                        if (!player.Inventory.Exists(i => i.Name.ToLower() == "key"))
                        {
                            player.Add(key);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThat's not quite right. Think about the sequence carefully.");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nInput not recognized or puzzle already solved.");
            }
        }
        Console.WriteLine("Exiting room...");
        Thread.Sleep(2000);
    }
}
