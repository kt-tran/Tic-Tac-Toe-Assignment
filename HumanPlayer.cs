using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class HumanPlayer : Player
    {
        //fields
        private string input = "";
        //properties
        internal override string Input
        {
            get; set;
        }
        //methods
        internal override void GetMove()
        {
            Input = ReadLine();
        }
        //constructor
    }
}