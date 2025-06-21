using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room_Test
{
    class BasementRoom : Room
    {
        private Puzzle laptopPuzzle;
        private Puzzle doorPuzzle;
        private bool crateOpened = false;

        public BasementRoom(Player player, Game game) : base(player, game)
        {
            laptopPuzzle = new Puzzle(
                "Laptop password hint: What kept you going through those nights?",
                "hope",
                "The laptop hums to life and shows the exit code: 7295."
            );

            doorPuzzle = new Puzzle(
                "Enter the 4-digit code to open the door:",
                "7295",
                "The door clicks open. You step into the light. You are free."
            );
        }

        public override void Enter()
        {
            Console.WriteLine("\n ROOM 2: THE BASEMENT ");
            Console.WriteLine("You find yourself in a cold basement. A staircase with an electronic lock blocks your way.");
            Console.WriteLine("Around you are a dusty laptop, a wooden crate, and a tattered diary.");
        }

        public override void ShowActions()
        {
            Console.WriteLine("\nAvailable actions:");
            Console.WriteLine("- investigate diary");
            Console.WriteLine("- investigate crate");
            Console.WriteLine("- open crate");
            Console.WriteLine("- use laptop");
            Console.WriteLine("- open door");
            Console.WriteLine("- inventory");
            Console.WriteLine("- exit");
        }

        public override void HandleInput(string input)
        {
            switch (input)
            {
                case "investigate diary":
                    Console.WriteLine("The diary contains sorrowful words. One line stands out: 'HOPE kept me going.'");
                    break;

                case "investigate crate":
                    Console.WriteLine("The wooden crate looks locked tight. Maybe you can pick the lock.");
                    break;

                case "open crate":
                    if (!Player.HasItem("Bobby Pin"))
                    {
                        Console.WriteLine("You need a Bobby Pin to pick this lock.");
                    }
                    else if (crateOpened)
                    {
                        Console.WriteLine("The crate is already open.");
                    }
                    else
                    {
                        Console.WriteLine("You pick the lock and open the crate with the Bobby Pin. Inside you find a USB stick.");
                        Player.AddItem(new Item("USB Stick", "Use this to access the laptop."));
                        crateOpened = true;
                    }
                    break;

                case "use laptop":
                    if (!Player.HasItem("USB Stick"))
                    {
                        Console.WriteLine("You need the USB stick to use the laptop.");
                    }
                    else if (laptopPuzzle.Solved)
                    {
                        Console.WriteLine("The laptop is already unlocked. It shows the exit code: 7295.");
                    }
                    else
                    {
                        Console.WriteLine(laptopPuzzle.Question);
                        string answer = Console.ReadLine() ?? "";
                        laptopPuzzle.Attempt(answer);
                    }
                    break;

                case "open door":
                    if (!laptopPuzzle.Solved)
                    {
                        Console.WriteLine("The door is locked with a 4-digit code. You need to unlock the laptop first.");
                    }
                    else
                    {
                        Console.WriteLine(doorPuzzle.Question);
                        string code = Console.ReadLine() ?? "";
                        if (doorPuzzle.Attempt(code))
                        {
                            Console.WriteLine("You have escaped! Congratulations!");
                            Environment.Exit(0);
                        }
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
