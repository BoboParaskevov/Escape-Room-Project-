namespace Escape_Room
{
       internal class Program
    {
        static void Main(string[] args)
        {
            Item key = new Item("Key", "The key to your bedroom door.");
            Item childhoodReward = new Item("Childhood Toy and Bobby Pin", "Your favorite toy from childhood and a bobby pin that might help later.");
            Item equation = new Item("Equation", "A math equation your parents used to lock the door.");

            Puzzle wardrobePuzzle = new Puzzle(
                "wardrobe",
                "You arranged your clothes in this order: Red, Green, Blue, Yellow. You hid your toy behind the third one, but when looking in the mirror behind your back, which color did you actually see?",
                "Green",
                childhoodReward);

            Puzzle chestPuzzle = new Puzzle(
                "chest",
                "The chest is locked tight. You remember using a bobby pin to open it when you were grounded.",
                "unlock",
                equation);

            List<Item> inventory = new List<Item>();
            Player player = new Player(inventory);

            List<Puzzle> puzzles = new List<Puzzle> { wardrobePuzzle, chestPuzzle };
            Room childhoodRoom = new Room("Room 1 - Childhood: Locked In My Bedroom", puzzles, key);

            List<Room> rooms = new List<Room> { childhoodRoom };
            Game game = new Game(player, rooms);

            game.Start();
        }
    }
}
