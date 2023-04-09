using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    /// <summary>
    /// Human player class 
    /// </summary>
    internal class HumanPlayer : Player
    {
        //fields
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