namespace Escape_Room
{
    internal class Program
    {
        static void Main(string[] args)
        {
    List<Item> inventory = new List<Item>();

    Item childhoodReward = new Item(
        "Childhood Toy and Bobby Pin",
        "Your favorite toy from childhood and a bobby pin that might help later.");

    Puzzle wardrobePuzzle = new Puzzle(
        "wardrobe_puzzle",
        "You arranged your clothes Red, Green, Blue, Yellow. You hid your toy behind the third one, but you counted as a child looking in a mirror behind your back. Which color was it?",
        "Green",
        childhoodReward
    );

    Console.WriteLine("Room 1 - Childhood: Locked In My Bedroom");
    Console.WriteLine("You approach the wardrobe filled with your old clothes...");
    Console.WriteLine("Puzzle: " + wardrobePuzzle.Description);
    Console.Write("Enter your answer: ");
    string input = Console.ReadLine();

    if (!string.IsNullOrEmpty(input) && input.Trim().ToLower() == wardrobePuzzle.Answer.ToLower())
    {
        wardrobePuzzle.Solved = true;
        inventory.Add(wardrobePuzzle.Reward);
        Console.WriteLine("\nCorrect! You've unlocked a part of your past.");
        Console.WriteLine("Reward: " + wardrobePuzzle.Reward);
    }
    else
    {
        Console.WriteLine("\nIncorrect. You can't quite piece it together yet.");
    }

    
    Item equation = new Item("Equation", "A math equation your parents used to lock the door.");
    Puzzle chestPuzzle = new Puzzle(
        "chest_unlock",
        "The chest is locked. You remember using a bobby pin to open it when grounded.",
        "unlocked",
        equation
    );

    bool done = false;
    while (!done)
    {
        Console.WriteLine("\nWhat would you like to investigate?");
        Console.WriteLine("1. Investigate Chest");
        Console.WriteLine("2. Investigate Carpet");
        Console.WriteLine("3. Open Chest");
        Console.WriteLine("4. Investigate Equation");
        Console.WriteLine("5. Open Glass Box");
        Console.WriteLine("6. Open Door");
        Console.WriteLine("7. Exit Game");
        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("\nThe box with the only kid’s things your parents ever bought you.");
                Console.WriteLine("Locked, but maybe there's something in the room to pick the lock.");
                break;

            case "2":
                Console.WriteLine("\nYou look under the carpet hoping to find the equation... but with no luck.");
                break;

            case "3":
                if (!chestPuzzle.Solved)
                {
                    bool hasBobbyPin = inventory.Exists(item => item.Name.ToLower().Contains("bobby pin"));

                    if (hasBobbyPin)
                    {
                        chestPuzzle.Solved = true;
                        inventory.Add(chestPuzzle.Reward);
                        Console.WriteLine("\nYou pick the lock and open the box. That felt familiar.");
                        Console.WriteLine("Inside the chest, you find a piece of paper — the equation!");
                        Console.WriteLine("Reward: " + chestPuzzle.Reward);
                    }
                    else
                    {
                        Console.WriteLine("\nCan’t open. Maybe there’s something in the room you can use to pick the lock.");
                    }
                }
                else
                {
                    Console.WriteLine("\nThe chest is already open. You’ve taken the equation.");
                }
                break;

            case "4":
                if (chestPuzzle.Solved)
                {
                    Console.WriteLine("\nYou examine the equation. It looks like a simple math puzzle: Look at this series: 53, 53, 40, 40, 27, 27, … What number should come next?");
                    Console.Write("Solve it: ");
                    string eqInput = Console.ReadLine();
                    if (eqInput.Trim() == "14")
                    {
                        Console.WriteLine("\nCorrect! This must be the code to the glass box.");
                    }
                    else
                    {
                        Console.WriteLine("\nThat's not quite right. Think about the order of operations.");
                    }
                }
                else
                {
                    Console.WriteLine("\nYou haven't found the equation yet.");
                }
                break;

            case "5":
                if (wardrobePuzzle.Solved && chestPuzzle.Solved)
                {
                    Console.Write("\nEnter the code from the equation: ");
                    string code = Console.ReadLine();
                    if (code.Trim() == "14")
                    {
                        Item key = new Item("Key", "The key to your bedroom.");
                        inventory.Add(key);
                        Console.WriteLine("\nYou’ve opened the glass box. The key is yours.");
                        Console.WriteLine("Reward: " + key);
                    }
                    else
                    {
                        Console.WriteLine("\nWrong code. Think again.");
                    }
                }
                else
                {
                    Console.WriteLine("\nYou still don’t have all the pieces to open the glass box.");
                }
                break;

            case "6":
                bool hasKey = inventory.Exists(item => item.Name.ToLower() == "key");
                if (hasKey)
                {
                    Console.WriteLine("\nYou open the door and exit with a sense of relief.");
                    done = true;
                }
                else
                {
                    Console.WriteLine("\nThe door is locked. You need the key from the glass box.");
                }
                break;

            case "7":
                Console.WriteLine("\nThanks for playing!");
                done = true;
                break;

            default:
                Console.WriteLine("\nInvalid choice.");
                break;
        }
    }
}
    }
}
