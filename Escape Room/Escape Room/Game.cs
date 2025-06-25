using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
      internal class Game
    {
        public Player Player { get; private set; }
        public Room CurrentRoom { get; private set; }

        public BedroomRoom bedroom;
        public BasementRoom basement;

        public Game()
        {
            Player = new Player();

            bedroom = new BedroomRoom(Player, this);
            basement = new BasementRoom(Player, this);

            CurrentRoom = bedroom; 
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                CurrentRoom.Enter();
                CurrentRoom.ShowActions();

                Console.Write("> ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "exit")
                {
                    Console.WriteLine("Exiting game. Goodbye!");
                    break;
                }

                CurrentRoom.HandleInput(input);
                string forward = Console.Readline();
            }
        }

        public void MoveToRoom(Room room)
        {
            CurrentRoom = room;
        }
    }
}
