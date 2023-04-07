using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    abstract class Player
    {
        //fields
        //properties
        public int PlayerID { get; set; }

        //methods
        public abstract void getMove();
        //constructor
    }
}