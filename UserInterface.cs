﻿using System;
using System.Numerics;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class UserInterface
    {
        //implement switching game modes later
        private Game currentGame;
        private bool playerQuit = false;
        private bool validInput = false;

        //fields
        private const string WELCOME_MESSAGE = "++++++++++++++++++++++++++++++++++++++++++++++\n" +
            "Welcome to Katie Tran's game application!\n" +
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
        private const string ASK_LOAD_SAVE = "Would you like to continue the game from last time?\n" +
            "1) Yes\n" +
            "2) No\n" +
            "Your input:";
        private const string MENU_MESSAGE = "Please enter:\n" +
            "- 'place' to make a move,\n" +
            "- 'help' for assistance\n" +
            "- 'undo' to undo your last move\n" +
            "- 'redo' to redo your last undone move, or\n" +
            "- 'QUIT' to exit.\n\n" +
            "Your input:";
        private const string INVALID_MOVE = "That was an invalid input. Try again, or type '" + HELP + "' for more assistance.\n";
        private const string LEAVE_GAME = "The game is over now. Goodbye!";
        private const string QUIT = "QUIT";
        private const string HELP = "help";
        private const string REQUEST_MOVE = "place";
        private const string UNDO = "undo";
        private const string REDO = "redo";

        //properties

        //methods
        /// <summary>
        /// Begins the game, querying the user for initial configuration
        /// </summary>
        internal void StartApp()
        {
            WriteLine(WELCOME_MESSAGE);

            RequestGameMode();
            RequestPlayerType();
            RequestLoadSave();

        }

        /// <summary>
        /// Main loop of the game, will check if the game is over and displays the current state of the board.
        /// </summary>
        internal void MainApp()
        {
            while (!currentGame.GameOver)
            {
                UpdateScreen();

                if (currentGame.CurrentPlayer.GetType() == typeof(HumanPlayer)) //if the player is a human
                    Menu();
                else
                    currentGame.GameTurn();

                UpdateScreen();
                WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
            UpdateScreen();
            WriteLine("The winner is player {0}. Congratulations! Well played.", currentGame.CurrentPlayer.PlayerID);
            WriteLine(LEAVE_GAME);
            Environment.Exit(0);
        }

        /// <summary>
        /// Returns the state of the gameboard
        /// </summary>
        /// <returns></returns>
        private void UpdateScreen()
        {
            WriteLine(currentGame.gameboard);
        }

        /// <summary>
        /// Displays options for user to play game, get help, quit, etc...
        /// </summary>
        private void Menu()
        {
            while (!playerQuit)
            {
                Write(MENU_MESSAGE);
                currentGame.CurrentPlayer.GetMove();
                switch (currentGame.CurrentPlayer.Input)
                {
                    case REDO:
                        currentGame.RedoMove();
                        return;
                    case UNDO:
                        currentGame.UndoMove();
                        WriteLine(currentGame.gameboard);
                        break;
                    case QUIT:
                        playerQuit = true;
                        WriteLine(LEAVE_GAME);
                        Environment.Exit(0);
                        break;
                    case HELP:
                        bool help = true;
                        while (help)
                        {
                            WriteLine(currentGame.helpS.Welcome);
                            currentGame.CurrentPlayer.GetMove();
                            switch (currentGame.CurrentPlayer.Input)
                            {
                                case "rules":
                                    WriteLine(currentGame.helpS.GameRules);
                                    break;
                                case "moves":
                                    WriteLine(currentGame.helpS.AvailableCommands);
                                    break;
                                case "close":
                                    help = false;
                                    break;
                            }
                            
                        }
                        break;
                    case REQUEST_MOVE:
                        currentGame.GameTurn();
                        return;
                    default:
                        WriteLine(INVALID_MOVE);
                        break;

                }
            }

        }

        /// <summary>
        /// Queries user for game mode
        /// </summary>
        private void RequestGameMode()
        {
            bool validPick = false;
            while (!validPick)
            {
                Write(PICK_GAME);
                string request = ReadLine();
                int choice;
                validPick = int.TryParse(request, out choice);
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
                    if (request == QUIT)
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


        /// <summary>
        /// Asks user which player types they would like for their game.
        /// </summary>
        private void RequestPlayerType()
        {
            bool validPick = false;
            while (!validPick)
            {
                Write(HUMAN_OR_COMPUTER);
                string Request = ReadLine();
                int choice;
                if (int.TryParse(Request, out choice))
                {
                    if (choice == 1)
                    {
                        WriteLine("You picked Human vs Human.\n");
                        validPick = true;
                        currentGame.CreateHumanPlayer(0);
                        currentGame.CreateHumanPlayer(1);
                        currentGame.GamePlayerMode = "Human";
                    }
                    else if (choice == 2)
                    {
                        WriteLine("You picked Computer vs Human.\n");
                        validPick = true;
                        currentGame.CreateHumanPlayer(0);
                        currentGame.CreateComputerPlayer(1, currentGame);
                        currentGame.GamePlayerMode = "Computer";
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
                    if (Request == QUIT)
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

        /// <summary>
        /// Queries player if they would like to load previous save.
        /// </summary>
        private void RequestLoadSave()
        {
            while (!playerQuit && !validInput)
            {
                Write(ASK_LOAD_SAVE);
                currentGame.CurrentPlayer.GetMove();
                switch (currentGame.CurrentPlayer.Input)
                {
                    case "1":
                        if (currentGame.logger.CheckSaveFile())
                        {
                            currentGame.logger.LoadSaveFile();
                            WriteLine("Previous game loaded successfully.");
                            validInput = true;
                        }
                        else
                            WriteLine("A save file does not exist. Previous game not found.");
                        break;
                    case "2":
                        WriteLine("You have selected to start a new game.");
                        currentGame.logger.MakeSaveFile();
                        currentGame.logger.SaveToFile(currentGame.gameboard, 2); //log an empty board with player 2 as "last player" so next player will be player 1
                        validInput = true;
                        break;
                    case QUIT:
                        WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break; //won't reach this but syntactically required
                    default:
                        WriteLine(INVALID_MOVE);
                        break;
                }
            }
        }
        //constructor
    }
}
