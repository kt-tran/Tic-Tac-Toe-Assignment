using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class HelpSystem
    {
        //fields
        private string availableCommands = "This is the Help System.\n" +
            "Enter 'rules' to see the rules of the game you're currently playing.\n" +
            "Enter 'undo' to undo a move.\n" +
            "Enter 'redo' to redo a move you previously undid.\n" +
            "Enter 'help' for more assistance.";

        //properties
        public string AvailableCommands
        {
            get { return availableCommands; }
        }

        //methods
        public string printCommands()
        {
            return AvailableCommands;
        }

        /*
         * string printRulesHelp()
        {

        }
        */

        //constructor
    }
}