using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    /// <summary>
    /// Help system for player convenience, contains information about the game and how to use the application.
    /// </summary>
    internal class HelpSystem
    {
        //fields
        private static string WELCOME = "\nThis is the Help System.\n" +
            "Enter 'rules' to see the rules of the game you're currently playing.\n" +
            "Enter 'moves' to view more information on how to make a move.\n" +
            "Enter 'close' to exit the help system.\n" +
            "Your input:";
        private static string AVAILABLE_COMMANDS = "\nThese are your possible moves.\n" +
                        "When it is your turn, you can enter:\n" +
                        "- 'save' to save the current game\n" +
                        "- 'undo' to undo both your and your opponent's previous move\n" +
                        "- 'redo' to redo a move you previously undid\n" +
                        "- 'place' to make a move\n" +
                        "- 'help' for assistance\n" +
                        "- 'QUIT' to exit\n";

        //properties
        internal string Welcome 
        { 
            get { return WELCOME; } 
        }
        internal string AvailableCommands
        {
            get { return AVAILABLE_COMMANDS; }
        }
        internal string GameRules
        {
            get; set;
        }
        //methods

        //constructor
    }
}