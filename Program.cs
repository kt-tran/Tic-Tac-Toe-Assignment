// IFN563 Assignment
// Created by Katie Tran - n11159243
using System;
using static System.Console;
using System.IO;

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
            bool gameIsOver = false;

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
                    updateScreen();
                    WriteLine("The number of turns that have been made are: {0}", TurnCounter);
                    TurnCounter += 1;
                    gameIsOver = currentGame.gameTurn();
                    if (gameIsOver)
                    {
                        updateScreen();
                        switch (currentGame.CurrentPlayerID) //allows modularity for games with more than 2 players in future
                        {
                            case 0:
                                WriteLine("The winner is player 2. Congratulations!"); //because player 2 is stored as 0 in CurrentPlayerID
                                break;
                            case 1:
                                WriteLine("The winner is player 1. Congratulations!");
                                break;
                        }
                        WriteLine(LEAVE_GAME);
                        Environment.Exit(0);
                    }
                    updateScreen();
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
                    }
                    else if (choice == 2)
                    {
                        WriteLine("You picked Computer vs Human.\n");
                        validPick = true;
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

    internal class Gameboard
    {
        //fields
        //consider making the data type more extensible, e.g. if this was chess then the board needs to accept those pieces
        private string[,] board;

        //properties
        public string[,] Board 
        { 
            get { return board; }
        }

        //methods
        public void placePiece(int x, int y, string piece)
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
            board = new string[x,y];
            for (int i = 0;i < board.GetLength(0);i++)
            {
                for (int j = 0;j < board.GetLength(1);j++)
                {
                    board[i,j] = "0";
                }
                
            }
        }


    }

    abstract class Game
    {
        //fields
        private bool gameOver = false;
        private int currentPlayerID;
        public Gameboard gameboard;
        public HelpSystem helpSystem;
        public History logger;

        //properties
        public abstract string[] Pieces
        {
            get; set;
        }
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

        /*
        public abstract Player[] PlayerList
        {
            get; set;
        }
        */

        //methods
        public abstract bool makeMove();

        public abstract bool isGameOver();
        public bool gameTurn()
        {
           
            bool turnComplete = false;
            while (!turnComplete)
            {
                turnComplete = makeMove();
            }
            logger.Log(outFile, gameboard);
            gameOver = isGameOver();
            if (!gameOver)
            {
                CurrentPlayerID = (CurrentPlayerID + 1) % PlayersCount;
                return gameOver;
            }
            else
                return gameOver = true;
            
        }

        //constructor: TODO implement abstract constructor & have children inherit
    }

    internal class NumericalTicTacToe : Game
    {
        //fields
        private const int PLAYERS_COUNT = 2;
        private const int HEIGHT_OF_GAMEBOARD = 3;
        private const int WIDTH_OF_GAMEBOARD = 3;
        private const int WIN_TOTAL = 15;
        private string[] NTTpieces = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };


        //properties
        public override string[] Pieces
        {
            get
            {
                return NTTpieces;
            }
            set
            {
                NTTpieces = value;
            }
        }

        public override string Rules
        {
            get
            {
                return "\nNumerical Tic Tac Toe is a variation of the classic Tic-Tac-Toe game.\n\n" +
                        "  1. Two players take turns placing the numbers 1 to 9 on a 3x3 board.\n" + 
                        "  2. The first player plays with the odd numbers, the second player plays with the even numbers.\n" +
                        "  3. All numbers can be used only once.\n\n" +
                        "The player who first puts down exactly 15 points in a line\n" +
                        "(sum of a horizontal, vertical, or diagonal row of three numbers) " +
                        "wins the game.\n" +
                        "\n" + 
                        "You can make a turn on the board using the row number and column number.\n" +
                        "For example, the number 7 has been placed on row 2, column 3.\n" +
                        "\n" +
                        "          column\n" +
                        "          1 2 3\r\n  row 1  |0|0|0|\r\n  row 2  |0|0|7|\r\n  row 3  |0|0|0|" +
                        "\n";
            }
        }

        public override int PlayersCount
        {
            get { return PLAYERS_COUNT; }
        }

        //methods

        private bool validateMove(int x, int y, string piece)
        {
            bool playerMatch = false;
            int intPiece2 = int.Parse(piece);
            playerMatch = (CurrentPlayerID == intPiece2 % 2); //player 1 (odd) is number 1. player 2 (even) is 0

            bool spareMove = false;
            for (int i =  0; i < Pieces.Length; i++)
            {
                if(piece == Pieces[i]) //if that piece has not been used yet
                {
                    if (gameboard.Board[x, y] == "0") //if that cell on the gameboard is empty
                    {
                        spareMove = true;
                        Pieces[i] = "0";
                    }
                    
                }
            }

            if (playerMatch & spareMove)
            {
                return true;
            }
            else { return  false; }
        }

        public override bool makeMove()
        {
            bool turnSuccess = false;
            bool withinGrid = false;
            bool isXAnInt = false;
            bool isYAnInt = false;
            bool isPieceAnInt = false;

            int xAsInt = -1;
            int yAsInt = -1;
            int intPiece = -1;
            string pieceAsString = "";

            switch(CurrentPlayerID) //allows modularity for games with more than 2 players in future
            {
                case 0:
                    Write("It's Player 2's turn.\n\n"); //because player 2 is stored as 0 in CurrentPlayerID
                    break;
                case 1:
                    Write("It's Player 1's turn.\n\n");
                    break;
            }

 
            while (!isPieceAnInt)
            {
                Write("Enter the number you wish to place:");
                pieceAsString = ReadLine();
                isPieceAnInt = int.TryParse(pieceAsString, out intPiece);
                if (!isPieceAnInt)
                {
                    WriteLine("That was not a number between 1 and 9 (inclusive). Please try again.");
                    continue;
                }
            }

            bool validMove = false;
            validMove = validateMove(xAsInt, yAsInt, pieceAsString);
            if (!validMove)
            {
                WriteLine("That was not a valid move. Either:\n" +
                    "- that move has been used, or\n" +
                    "- you cannot use that piece, or\n" +
                    "- that cell on the gameboard is taken.\n" +
                    "Please try again.\n");
                return turnSuccess;
            }
            else
            {
                gameboard.placePiece(xAsInt, yAsInt, pieceAsString);
                return turnSuccess = true;
            }

            
         }

        public override bool isGameOver()
        {
            int total = 0;
            //check horizontally for a win
            for (int x = 0; x < gameboard.Board.GetLength(0); x++)
            {
                for (int y = 0; y < gameboard.Board.GetLength(1); y++)
                {
                    total += int.Parse(gameboard.Board[x, y]);
                    if (total == 15)
                        return true;
                }
                total = 0; //reset after each row
            }

            //check vertically for a win
            for (int y = 0; y < gameboard.Board.GetLength(1); y++)
            {
                for (int x = 0; x < gameboard.Board.GetLength(0); x++)
                {
                    total += int.Parse(gameboard.Board[x, y]);
                    if (total == 15)
                        return true;
                }
                total = 0; //reset after each column
            }

            //check diagonally for a win
            for (int x = 0, y = 0; x < gameboard.Board.GetLength(0); x++, y++) //checks \ diagonal, from top left to bottom right 
            {
                total += int.Parse(gameboard.Board[x, y]);
                if (total == 15)
                    return true;
            }
            total = 0; //reset before checking the other diagonal direction

            for (int x = 0, y = 2; x < gameboard.Board.GetLength(0); x++, y--) //checks / diagonal, from bottom left to top right
            {
                total += int.Parse(gameboard.Board[x, y]);
                if (total == 15)
                    return true;
            }

            //after checking each possible win state
            return false;
        }

        //constructor
        public NumericalTicTacToe()
        {
            gameboard = new Gameboard(HEIGHT_OF_GAMEBOARD, WIDTH_OF_GAMEBOARD);
            helpSystem = new HelpSystem();
            logger = new History();
            FileStream outFile = logger.MakeSaveFile();
            CurrentPlayerID = 1;
        }
    }

    abstract class Player
    {
        //fields
        //properties
        public int playerID { get; set; }

        //methods
        public void getMove ()
        {
            string input = ReadLine();
        }
        //constructor
    }

    internal class HumanPlayer : Player
    {
        //fields
        //properties
        //methods
        //constructor
    }
    internal class ComputerPlayer : Player
    {
        //fields
        //properties
        //methods
        //constructor
    }
    internal class History
    {
        //fields
        private int saveFileID = 0;
        //properties
        public int SaveFileID
        {
            get; set;
        }

        //methods
        public FileStream MakeSaveFile()
        {
            string newFileName;
            newFileName = "Save File #" + SaveFileID;
            FileStream outFile = new FileStream(newFileName, FileMode.Create, FileAccess.Write);
            return outFile;
        }
        public void Log(FileStream outFile, Gameboard currentboard)
        {
            StreamWriter writer = new StreamWriter(outFile);
            writer.WriteLine(currentboard.ToString());
        }
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