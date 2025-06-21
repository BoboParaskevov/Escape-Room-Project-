using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room_Test
{
    class BedroomRoom : Room
    {
        private bool wardrobeOpened = false;
        private bool chestOpened = false;
        private bool glassBoxOpened = false;

        private Puzzle clothesPuzzle;
        private Puzzle equationPuzzle;

        private Item childhoodToy = new Item("Childhood Toy", "Your favorite toy from childhood.");
        private Item bobbyPin = new Item("Bobby Pin", "Useful for picking locks.");
        private Item key = new Item("Bedroom Key", "The key to your bedroom door.");

        private string glassBoxCode = "14";

        public BedroomRoom(Player player, Game game) : base(player, game)
        {
            clothesPuzzle = new Puzzle(
                "You arranged your clothes Red, Green, Blue, Yellow. You hid your toy behind the third one, " +
                "but you counted as a child looking in a mirror behind your back. Which color was it?",
                "green",
                "You move the green T-Shirt and find all your hidden things."
            );

            equationPuzzle = new Puzzle(
                "Look at this series: 53, 53, 40, 40, 27, 27, ... What number should come next?",
                "14",
                $"You solved the equation! The code to the glass box is {glassBoxCode}."
            );
        }

        public override void Enter()
        {
            Console.WriteLine("\n=== ROOM 1: CHILDHOOD BEDROOM ===");
            Console.WriteLine("You wake up in your childhood bed, drenched with sweat. Everything is just as it was back then.");
            Console.WriteLine("You see your wardrobe, a locked chest with puzzles inside, a carpet, and a glass box with the key to your door.");
        }

        public override void ShowActions()
        {
            Console.WriteLine("\nAvailable actions:");
            Console.WriteLine("- open wardrobe");
            Console.WriteLine("- investigate chest");
            Console.WriteLine("- open chest");
            Console.WriteLine("- investigate carpet");
            Console.WriteLine("- investigate equation");
            Console.WriteLine("- open glass box");
            Console.WriteLine("- open door");
            Console.WriteLine("- inventory");
            Console.WriteLine("- exit");
        }

        public override void HandleInput(string input)
        {
            switch (input)
            {
                case "open wardrobe":
                    if (wardrobeOpened)
                    {
                        Console.WriteLine("You’ve already got what you need from the wardrobe.");
                    }
                    else
                    {
                        Console.WriteLine(clothesPuzzle.Question);
                        string ans = Console.ReadLine() ?? "";
                        if (clothesPuzzle.Attempt(ans))
                        {
                            Player.AddItem(childhoodToy);
                            Player.AddItem(bobbyPin);
                            wardrobeOpened = true;
                        }
                    }
                    break;

                case "investigate chest":
                    Console.WriteLine("The box contains the only toys your parents bought you, but it's locked tight.");
                    break;

                case "open chest":
                    if (!Player.HasItem("Bobby Pin"))
                    {
                        Console.WriteLine("You can't open the chest without something to pick the lock.");
                    }
                    else if (chestOpened)
                    {
                        Console.WriteLine("You've already taken what you needed from the chest.");
                    }
                    else
                    {
                        Console.WriteLine("You pick the lock with your Bobby Pin and open the chest.");
                        Console.WriteLine(equationPuzzle.Question);
                        string ans = Console.ReadLine() ?? "";
                        if (equationPuzzle.Attempt(ans))
                        {
                            Player.AddItem(new Item("Equation", "The equation that reveals the code to the glass box."));
                            chestOpened = true;
                        }
                    }
                    break;

                case "investigate carpet":
                    Console.WriteLine("You look under the carpet hoping to find the equation but find nothing.");
                    break;

                case "investigate equation":
                    if (Player.HasItem("Equation"))
                    {
                        Console.WriteLine($"You have the equation solved. The glass box code is {glassBoxCode}.");
                    }
                    else
                    {
                        Console.WriteLine("You don’t have the equation yet. Keep searching.");
                    }
                    break;

                case "open glass box":
                    if (!Player.HasItem("Equation"))
                    {
                        Console.WriteLine("You don’t have the code yet. Maybe solving the equation will help.");
                    }
                    else if (glassBoxOpened)
                    {
                        Console.WriteLine("You already opened the glass box and took the key.");
                    }
                    else
                    {
                        Console.WriteLine("Enter the code to the glass box:");
                        string codeInput = Console.ReadLine() ?? "";
                        if (codeInput == glassBoxCode)
                        {
                            Console.WriteLine("You opened the glass box and took the key.");
                            Player.AddItem(key);
                            glassBoxOpened = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong code. Try again.");
                        }
                    }
                    break;

                case "open door":
                    if (!Player.HasItem("Bedroom Key"))
                    {
                        Console.WriteLine("The door is locked. You need the key from the glass box.");
                    }
                    else
                    {
                        Console.WriteLine("You open the door and exit with relief. You’re not a kid anymore.");
                        Game.MoveToRoom(Game.basement);
                    }
                    break;

                case "inventory":
                    ShowInventory();
                    break;

                default:
                    Console.WriteLine("Invalid action.");
                    break;
            }
        }

        private void ShowInventory()
        {
            Console.WriteLine("Inventory:");
            if (Player.Inventory.Count == 0)
            {
                Console.WriteLine("- Empty");
            }
            else
            {
                foreach (var item in Player.Inventory)
                    Console.WriteLine($"- {item.Name}: {item.Description}");
            }
        }
    }
}

