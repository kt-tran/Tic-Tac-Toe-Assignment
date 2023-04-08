﻿using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class HelpSystem
    {
        //fields
        private static string WELCOME = "This is the Help System.\n" +
            "Enter 'rules' to see the rules of the game you're currently playing.\n" +
            "Enter 'moves' to view more information on how to make a move.\n" +
            "Enter 'close' to exit the help system.";
        private static string AVAILABLE_COMMANDS = "These are your possible moves.\n" +
                        "When it is your turn, you can enter:\n" +
                        "- 'save' to save the current game\n" +
                        "- 'undo' to undo both your and your opponent's previous move\n" +
                        "- 'redo' to redo a move you previously undid\n" +
                        "- 'place' to make a move\n" +
                        "- 'help' for assistance\n" +
                        "- 'QUIT' to exit\n";

        private string gameRules;

        //properties
        public string Welcome { get { return WELCOME; } }
        public string AvailableCommands
        {
            get { return AVAILABLE_COMMANDS; }
        }
        public string GameRules
        {
            get; set;
        }
        //methods
        public string printWelcome()
        {
            return WELCOME;
        }
        public string printCommands()
        {
            return AvailableCommands;
        }
        public string printRules()
        {
            return GameRules;
        }

        //constructor
    }
}