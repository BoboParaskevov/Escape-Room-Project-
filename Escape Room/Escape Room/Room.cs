using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
           abstract class Room
    {
        protected Player Player;
        protected Game Game;
        protected List<Interactable> Interactables = new();

        public Room(Player player, Game game)
        {
            Player = player;
            Game = game;
        }

        public abstract void Enter();
        public abstract void ShowActions();
        public abstract void HandleInput(string input);
    }
}
