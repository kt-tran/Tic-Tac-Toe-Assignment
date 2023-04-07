using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    abstract class Player
    {
        //fields
        private string playerType = "";
        //properties
        public int PlayerID { get; set; }
        public string PlayerType { get; set; }

        //methods
        public abstract void getMove(int playerNumber);
        //constructor
    }
}