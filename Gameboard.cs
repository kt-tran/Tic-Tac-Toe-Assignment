using static System.Console;
using System;

namespace Tic_Tac_Toe_Assignment
{
    internal class Gameboard
    {
        //fields
        //consider making the data type more extensible, e.g. if this was chess then the board needs to accept those pieces
        private string[,] board;

        //properties
        internal string[,] Board
        {
            get { return board; }
        }

        //methods
        internal void PlacePiece(int x, int y, string piece)
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
                    printableBoard += Convert.ToString(board[i, j]);
                    printableBoard += "|";
                }
                printableBoard += "\n";
            }
            return printableBoard;
        }

        //constructor
        internal Gameboard(int x, int y)
        {
            board = new string[x, y];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "0";
                }

            }
        }
        internal Gameboard(string[,] SaveFileBoard)
        {
            // board = SaveFileBoard
            board = new string[SaveFileBoard.GetLength(0), SaveFileBoard.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = SaveFileBoard[i, j];
                }
            }
        }
    }
}