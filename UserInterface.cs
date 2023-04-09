using System;
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
            "- 'help' for assistance, or\n" +
            "- 'QUIT' to exit.\n\n" +
            "Your input:";
        private const string INVALID_MOVE = "That was an invalid input. Try again, or type '" + HELP + "' for more assistance.\n";
        private const string LEAVE_GAME = "The game is over now. Goodbye!";
        private const string QUIT = "QUIT";
        private const string HELP = "help";
        private const string REQUEST_MOVE = "place";

        //properties
        private int TurnCounter
        {
            get; set;
        }

        //methods
        internal void StartApp()
        {
            WriteLine(WELCOME_MESSAGE);

            RequestGameMode();
            RequestPlayerType();
            RequestLoadSave();

        }

        internal void MainApp()
        {
            while (!currentGame.GameOver)
            {
                UpdateScreen();
                WriteLine("The number of turns that have been made are: {0}", TurnCounter);
                TurnCounter += 1;

                if (currentGame.CurrentPlayer.GetType() == typeof(HumanPlayer)) //if the player is a human
                    Menu();
                else
                    currentGame.GameTurn();

                UpdateScreen();
            }
            UpdateScreen();
            WriteLine("The winner is player {0}. Congratulations! Well played.", currentGame.CurrentPlayer.PlayerID);
            WriteLine(LEAVE_GAME);
            Environment.Exit(0);
        }
        private string UpdateScreen()
        {
            WriteLine(currentGame.gameboard);
            return currentGame.gameboard.ToString();
        }

        private void Menu() //eventually implement undo & redo
        {
            while (!playerQuit)
            {
                Write(MENU_MESSAGE);
                currentGame.CurrentPlayer.GetMove();
                switch (currentGame.CurrentPlayer.Input)
                {
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

        private void RequestGameMode()
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
                            currentGame.gameboard = currentGame.logger.LoadSaveFile();
                            WriteLine("Previous game loaded successfully.");
                            validInput = true;
                        }
                        else
                            WriteLine("A save file does not exist. Previous game not found.");
                        break;
                    case "2":
                        WriteLine("You have selected to start a new game.");
                        currentGame.logger.MakeSaveFile();
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
        //constructor - currently using default constructor
    }
}
