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

            Gameboard gameboard = new Gameboard();
            gameboard.createBoard();
            WriteLine(gameboard.ToString());

        }
    }

    //all classes are internal by default but this is explicitly written for clarity

    internal class UserInterface
    {
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
        

        //constructor - currently using default constructor
    }

    internal class Gameboard
    {
        //fields
        //consider making the data type more extensible, e.g. if this was chess then the board needs to accept those pieces
        private int[,] board;

        //properties

        //methods
        public int[,] createBoard()
        {
            board = new int [,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            return board;
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

    }

    interface IGame
    {
        //an interface cannot have fields
        //properties

        //methods
        /*
         * void initializeGame()
        {
            return createBoard();
        }
        */

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

    internal class NumericalTicTacToe : IGame
    {
        //fields
        int playersCount = 2;
        char[] piece;

        //properties

        //methods

        //constructor
    }
}