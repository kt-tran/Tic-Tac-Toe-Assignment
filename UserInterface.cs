﻿using System;
using System.Numerics;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class UserInterface
    {
        //implement switching game modes later
        private Game currentGame;

        //fields
        private const string WELCOME_MESSAGE = "++++++++++++++++++++++++++++++++++++++++++++++\n" +
            "Welcome to Katie Tran's game application!\n" +
            "You can enter 'QUIT' to exit at any time.\n" +
            "Enjoy :)\n" +
            "++++++++++++++++++++++++++++++++++++++++++++++\n";
        private const string PICK_GAME = "What game would you like to play?\n" +
            "1) Numerical Tic-Tac-Toe\n" +
            "2) Wild Tic-Tac-Toe\n\n" +
            "I would like to play:";
        private const string HUMAN_OR_COMPUTER = "Which game mode would you like?\n" +
            "1) Human vs Human\n" +
            "2) Computer vs Human\n\n" +
            "I would like to play:";
        private const string MENU_MESSAGE = "Please enter:\n" +
            "- 'place' to make a move,\n" +
            "- 'help' for assistance, or\n" +
            "- 'QUIT' to exit.\n\n" +
            "Your input:";
        private const string INVALID_MOVE = "That was an invalid input. Try again, or type '" + HELP + "' for more assistance.\n";
        private const string LEAVE_GAME = "The game is over now. Goodbye!";
        private const string QUIT = "QUIT";
        private const string HELP = "help";
        private const string REQUEST_MOVE = "place";
        private int turnCounter = 0;

        //properties
        public int TurnCounter
        {
            get; set;
        }

        //methods
        public void startApp()
        {
            WriteLine(WELCOME_MESSAGE);

            requestGameMode();
            requestPlayerType();
        }

        public void mainApp()
        {
            while (!currentGame.IsGameOver)
            {
                updateScreen();
                WriteLine("The number of turns that have been made are: {0}", TurnCounter);
                TurnCounter += 1;

                if (currentGame.PlayerList[currentGame.CurrentPlayerIndex].GetType() == typeof(HumanPlayer)) //if the player is a human
                    menu();
                else
                    currentGame.gameTurn();

                updateScreen();
            }
            updateScreen();
            WriteLine("The winner is player {0}. Congratulations! Well played.", currentGame.CurrentPlayerIndex + 1);
            WriteLine(LEAVE_GAME);
            Environment.Exit(0);
        }
        private string updateScreen()
        {
            WriteLine(currentGame.gameboard);
            return currentGame.gameboard.ToString();
        }

        private void menu() //eventually implement undo & redo
        {
            bool playerQuit = false;

            while (!playerQuit)
            {
                Write(MENU_MESSAGE);
                currentGame.PlayerList[currentGame.CurrentPlayerIndex].getMove();
                switch (currentGame.PlayerList[currentGame.CurrentPlayerIndex].Input)
                {
                    case QUIT:
                        playerQuit = true;
                        WriteLine(LEAVE_GAME);
                        Environment.Exit(0);
                        break;
                    case HELP:
                        currentGame.helpS.printWelcome();
                        currentGame.PlayerList[currentGame.CurrentPlayerIndex].getMove();
                        switch (currentGame.PlayerList[currentGame.CurrentPlayerIndex].Input)
                        {
                            case "rules":
                                currentGame.helpS.printRules();
                                //how do I make it go back to the welcome screen??
                            case "moves":
                                currentGame.helpS.printCommands();

                            case "close":
                                break;
                        }
                        break;
                    case REQUEST_MOVE:
                        currentGame.gameTurn();
                        return;
                    default:
                        WriteLine(INVALID_MOVE);
                        break;

                }

            }

        }

        private void requestGameMode()
        {
            bool validPick = false;
            while (!validPick)
            {
                Write(PICK_GAME);
                string pickInput = ReadLine();
                int choice;
                validPick = int.TryParse(pickInput, out choice);
                if (validPick)
                {
                    if (choice == 1)
                    {
                        currentGame = new NumericalTicTacToe();
                        WriteLine(currentGame.Rules);
                    }
                    else if (choice == 2)
                    {
                        WriteLine("That game is currently not supported at the moment. Please try a different game.\n");
                        validPick = false;
                        continue;
                    }
                    else
                    {
                        WriteLine(INVALID_MOVE);
                        validPick = false;
                        continue;
                    }
                }
                else
                {
                    if (pickInput == HELP)
                    {
                        //call help system
                        validPick = true; //TODO
                    }
                    else if (pickInput == QUIT)
                    {
                        WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        WriteLine(INVALID_MOVE);
                    }

                }
            }
        }

        private void requestPlayerType()
        {
            bool validPick = false;
            while (!validPick)
            {
                Write(HUMAN_OR_COMPUTER);
                string Request = ReadLine();
                int choice;
                bool isRequestAnInt = false;
                isRequestAnInt = int.TryParse(Request, out choice);
                if (isRequestAnInt)
                {
                    if (choice == 1)
                    {
                        WriteLine("You picked Human vs Human.\n");
                        validPick = true;
                        currentGame.CreateHumanPlayer(0);
                        currentGame.CreateHumanPlayer(1);
                    }
                    else if (choice == 2)
                    {
                        WriteLine("You picked Computer vs Human.\n");
                        validPick = true;
                        currentGame.CreateHumanPlayer(0);
                        currentGame.CreateComputerPlayer(1, currentGame);
                    }
                    else
                    {
                        WriteLine(INVALID_MOVE);
                        validPick = false;
                        continue;
                    }
                }
                else
                {
                    if (Request == HELP)
                    {
                        //call help system
                        validPick = true; //TODO
                    }
                    else if (Request == QUIT)
                    {
                        WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        WriteLine(INVALID_MOVE);
                    }

                }
            }
        }
        //constructor - currently using default constructor
    }
}
