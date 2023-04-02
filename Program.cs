// IFN563 Assignment
// Created by Katie Tran - n11159243
using System;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class Program
    {
        static void Main()
        {
            UserInterface UI = new UserInterface();
            UI.startApp();
            UI.mainApp();
        }
    }

    internal class UserInterface
    {
        //implement switching game modes later
        private Game currentGame;

        //fields
        private const string WELCOME_MESSAGE = "Welcome to the game application!\n" +
            "You can enter 'QUIT' to exit at any time.\n" +
            "Enjoy :)";
        private const string PICK_GAME = "What game would you like to play?\n" +
            "1) Numerical Tic-Tac-Toe\n" +
            "2) Wild Tic-Tac-Toe\n";
        private const string MENU_MESSAGE = "Enter:\n" +
            "1) 'place' to make a move,\n" +
            "2) 'help' for assistance, or\n" +
            "3) 'QUIT' to exit.\n" +
            "Your input:";
        private const string INVALID_MOVE = "That was an invalid input. Try again, or type '" + HELP + "' for more assistance.";
        private const string LEAVE_GAME = "The game is over now. Goodbye!";
        private const string QUIT = "QUIT";
        private const string HELP = "help";
        private const string REQUEST_MOVE = "place";

        //properties

        //methods
        public void startApp()
        {
            WriteLine(WELCOME_MESSAGE);

            bool validPick = false;
            while (!validPick)
            {
                WriteLine(PICK_GAME);
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
                        Environment.Exit(0);
                    }
                    else
                    {
                        WriteLine(INVALID_MOVE);
                    }

                }
            }
        }

        public void mainApp()
        {
            string validatedInput = menu();
            bool playerQuit = false;

            while (!playerQuit)
            {
                if (validatedInput == QUIT)
                {
                    playerQuit = true;
                    Environment.Exit(0);
                }
                if (validatedInput == HELP)
                {
                    //helpSystem.printCommands(); 
                }
                if (validatedInput == REQUEST_MOVE)
                {
                    currentGame.gameRound();
                    updateScreen();
                }

            }

            WriteLine(LEAVE_GAME);
        }
        public string updateScreen()
        {
            return currentGame.gameboard.ToString();
        }

        public int pickGame(string input)
        {
            bool validInput = false;
            int choice;

            validInput = int.TryParse(input, out choice);
            if (validInput && (choice == 1 || choice == 2)) 
            {
                return choice;
            }
            else
            {
                if (input == HELP)
                {
                    return 3;
                }
                if (input == QUIT)
                {
                    return 4;
                }
                
                return 0;
            }
        }
        public string menu() //eventually implement undo & redo
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
        //constructor - currently using default constructor
    }

    internal class Gameboard
    {
        //fields
        //consider making the data type more extensible, e.g. if this was chess then the board needs to accept those pieces
        private int[,] board;

        //properties
        public int[,] Board 
        { 
            get { return board; }
        }

        //methods
        public void placePiece(int x, int y, int piece)
        {
            board[x, y] = piece;
        }

        public override string ToString()
        {
            string printableBoard = "\n";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                printableBoard += "|";
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    printableBoard += Convert.ToString(board[i,j]);
                    printableBoard += "|";
                }
                printableBoard += "\n";
            }
            return printableBoard;
        }

        //constructor
        public Gameboard(int x, int y)
        {
            board = new int[x,y];
        }


    }

    abstract class Game
    {
        //fields
        private char[] piece;
        private bool gameOver = false;
        private int currentPlayerID;
        public Gameboard gameboard;
        public HelpSystem helpSystem;

        //properties
        public int CurrentPlayerID
        {
            get { return currentPlayerID; }
            set { currentPlayerID = value; }
        }
        public abstract string Rules
        {
            get;
        }

        public abstract int PlayersCount
        {
            get;
        }

        //methods
        public abstract void makeMove();
        public void gameRound()
        {   
            if (!gameOver)
            {
                makeMove();
                CurrentPlayerID = (CurrentPlayerID + 1) % PlayersCount;
            }
        }

        //constructor
    }

    internal class NumericalTicTacToe : Game
    {
        //fields
        //CONSIDER CHANGING TO CONST
        int playersCount = 2;
        char[] pieces = { '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        int HeightOfGameboard = 3;
        int WidthOfGameboard = 3;
        

        //properties
        public override string Rules
        {
            get
            {
                return "Numerical Tic Tac Toe is a variation of the classic Tic-Tac-Toe game.\n\n" +
                        "  1. Two players take turns placing the numbers 1 to 9 on a 3x3 board.\n" + 
                        "  2. The first player plays with the odd numbers, the second player plays with the even numbers.\n" +
                        "  3. All numbers can be used only once.\n\n" +
                        "The player who first puts down exactly 15 points in a line\n" +
                        "(sum of a horizontal, vertical, or diagonal row of three numbers) " +
                        "wins the game.\n";
            }
        }

        public override int PlayersCount
        {
            get { return playersCount; }
        }

        //methods

        bool validateMove(int x, int y, char piece)
        {
            bool playerMatch = false;
            playerMatch = (CurrentPlayerID == piece % 2); //player 1 (odd) is number 1. player 2 (even) is 0

            bool spareMove = false;
            for (int i =  0; i < pieces.Length; i++)
            {
                if(piece == pieces[i]) //if that piece has not been used yet
                {
                    if (gameboard.Board[x, y] == 0) //if that cell on the gameboard is empty
                    {
                        spareMove = true;
                        pieces[i] = '0';
                    }
                    
                }
            }

            if (playerMatch & spareMove)
            {
                return true;
            }
            else { return  false; }
        }

        public override void makeMove()
        {
            bool withinGrid = false;
            bool isXAnInt = false;
            bool isYAnInt = false;
            bool isPieceAnInt = false;

            int xAsInt = -1;
            int yAsInt = -1;
            int intPiece = -1;
            char pieceAsChar = '?';

            while (!withinGrid || !isXAnInt)
            {
                Write("Enter the row of your move:");
                string xAsString = ReadLine();
                isXAnInt = int.TryParse(xAsString, out xAsInt);

                if (!isXAnInt)
                {
                    WriteLine("That was not an integer. Please try again.");
                    continue;
                }

                if (xAsInt > WidthOfGameboard || xAsInt < 1)
                {
                    WriteLine("That is outside of the board. Please try again.");
                    continue;
                }
                withinGrid = true;
            }

            withinGrid = false;
            while (!withinGrid || !isYAnInt)
            {
                Write("Enter the column of your move:");
                string yAsString = ReadLine();
                isYAnInt = int.TryParse(yAsString, out yAsInt);

                if (!isYAnInt)
                {
                    WriteLine("That was not an integer. Please try again.");
                    continue;
                }

                if (yAsInt > HeightOfGameboard || yAsInt < 1)
                {
                    WriteLine("That is outside of the board. Please try again.");
                    continue;
                }
                withinGrid = true;
            }

            while (!isPieceAnInt)
            {
                Write("Enter the number you wish to place:");
                string pieceAsString = ReadLine();
                isPieceAnInt = int.TryParse(pieceAsString, out intPiece);
                if (!isPieceAnInt)
                {
                    WriteLine("That was not a number between 1 and 9 (inclusive). Please try again.");
                    continue;
                }

                pieceAsChar = (char)intPiece;
            }

            bool validMove = false;
            validMove = validateMove(xAsInt, yAsInt, pieceAsChar);
            if (!validMove)
            {
                WriteLine("That was not a valid move. Either that move has been used, you cannot use that piece or that cell on the gameboard is taken." +
                    "Please try again.");
            }
            else
            {
                gameboard.placePiece(xAsInt, yAsInt, pieceAsChar);
            }

            
         }


        //constructor
        public NumericalTicTacToe()
        {
            gameboard = new Gameboard(HeightOfGameboard, WidthOfGameboard);
            helpSystem = new HelpSystem();
            CurrentPlayerID = 1;
        }
    }

    internal class History
    {
        //fields

        //properties

        //methods

        //constructor
    }

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