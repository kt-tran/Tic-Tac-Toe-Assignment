using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    /// <summary>
    /// Player abstract class
    /// </summary>
    abstract class Player
    {
        //fields
        //properties
        internal abstract string Input
        {
            get; set;
        }
        internal int PlayerID { get; set; }

        //methods
        internal abstract void GetMove();
        //constructor
    }
}