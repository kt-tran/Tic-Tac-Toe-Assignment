// IFN563 Assignment
// Created by Katie Tran - n11159243
using System;
using System.Data;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class Program
    {
        static void Main()
        {
            UserInterface UI = new UserInterface();
            WriteLine(UI.WelcomeMessage);
            UI.startGame();
            WriteLine(UI.updateScreen());

        }
    }

    //all classes are internal by default but this is explicitly written for clarity

    internal class UserInterface
    {
        //implement switching game modes later
        Game currentGame = new NumericalTicTacToe();

        //fields
        private string welcomeMessage = "Welcome to the Tic-Tac-Toe game!\n" + 
            "Created by Katie Tran.\n" +
            "Enjoy :)";
        private string invalidMove = "That was an invalid move. Try again, or type 'help' for more assistance.";

        //properties
        public string WelcomeMessage
        {
            get { return welcomeMessage; }
        }

        public string InvalidMove
        {
            get { return invalidMove; }
        }

        //methods

        public void startGame() 
        {
            currentGame.initializeGame();
        }
        public string updateScreen()
        {
            return currentGame.Gameboard.ToString();
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
        public Gameboard()
        {
            board = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }


    }

    abstract class Game
    {
        //fields
        private int playersCount;
        private string rules;
        private char[] piece;

        //properties

        //methods
        public abstract void initializeGame();
        

        /*
         * mainGame()
        {   bool gameEnd = false;
            bool playerQuit = false;

            while (!gameEnd)
            {
                updateboard?
            }
        }
        */

        //constructor
    }

    internal class NumericalTicTacToe : Game
    {
        //fields
        string rules = "Numerical Tic Tac Toe is a variation of the classic Tic-Tac-Toe game.\n" +
            "Two players take turns placing the numbers 1 to 9 on a 3x3 board. The first player plays with the odd numbers, the second player plays with the even numbers.\n" +
            "All numbers can be used only once.\n" +
            "The player who first puts down exactly 15 points in a line (sum of a horizontal, vertical, or diagonal row of three numbers) wins the game.";
        int playersCount = 2;
        char[] pieces = { '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        int playerID; //player 1 (odd) is number 0. player 2 (even) is 1.

        //properties

        //methods
        public override void initializeGame()
        {
            Gameboard gameboard = new Gameboard();
        }

        bool validateMove(int x, int y, char piece)
        {
            bool playerMatch = false;
            string Piece = piece.ToString();

            bool isItAnInteger = int.TryParse(Piece, out int intPiece);
            playerMatch = (playerID + 1 == intPiece % 2);

            bool spareMove = false;
            for (int i =  0; i < pieces.Length; i++)
            {
                if(piece == pieces[i])
                {
                    spareMove = true;
                }
            }

            if (isItAnInteger & playerMatch & spareMove)
            {
                return true;
            }
            else { return  false; }
        }

        //constructor
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
        string printCommands()
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