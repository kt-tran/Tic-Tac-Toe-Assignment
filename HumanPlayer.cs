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
        public override string Input
        {
            get; set;
        }
        //methods
        public override void getMove()
        {
            Input = ReadLine();
        }
        //constructor
    }
}