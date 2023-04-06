using System;
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
            string validatedInput = "";
            bool playerQuit = false;

            while (!playerQuit)
            {
                validatedInput = menu();
                if (validatedInput == QUIT)
                {
                    playerQuit = true;
                    WriteLine(LEAVE_GAME);
                    Environment.Exit(0);
                }
                if (validatedInput == HELP)
                {
                    //helpSystem.printCommands(); 
                }
                if (validatedInput == REQUEST_MOVE)
                {
                    gameRound();
                }

            }
        }
        private string updateScreen()
        {
            WriteLine(currentGame.gameboard);
            return currentGame.gameboard.ToString();
        }

        private string menu() //eventually implement undo & redo
        {
            while (true)
            {
                Write(MENU_MESSAGE);
                string input = ReadLine();
                switch (input)
                {
                    case QUIT:
                        return QUIT;
                    case HELP:
                        return HELP;
                    case REQUEST_MOVE:
                        return REQUEST_MOVE;
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
                        currentGame.CreatePlayers("Human");
                    }
                    else if (choice == 2)
                    {
                        WriteLine("You picked Computer vs Human.\n");
                        validPick = true;
                        currentGame.CreatePlayers("Computer");
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
        private void gameRound()
        {
            bool gameIsOver = false;
            for (int i = 0; i < currentGame.PlayerList.Length; i++)
            {
                updateScreen();
                WriteLine("The number of turns that have been made are: {0}", TurnCounter);
                TurnCounter += 1;
                gameIsOver = currentGame.gameTurn(currentGame.PlayerList[i]);
                if (gameIsOver)
                {
                    updateScreen();
                    WriteLine("The winner is player {0}. Congratulations! Well played.", currentGame.PlayerList[i].PlayerID); //or i+1
                    WriteLine(LEAVE_GAME);
                    Environment.Exit(0);
                }
                updateScreen();
            }
        }
        //constructor - currently using default constructor
    }
}
