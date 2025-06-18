using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
     internal class Puzzle
 {
     private string key;
     private string description;
     private string answer;
     private Item reward;
     private bool solved;

     public string Key { get => key; set => key = value; }
     public string Description { get => description; set => description = value; }
     public string Answer { get => answer; set => answer = value; }
     public bool Solved { get => solved; set => solved = value; }
     internal Item Reward { get => reward; set => reward = value; }

     public Puzzle(string key, string description, string answer, Item reward)
     {
         this.key = key;
         this.description = description;
         this.answer = answer;
         this.reward = reward;
         solved = false;
     }
 }
