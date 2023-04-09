using static System.Console;
using System;

namespace Tic_Tac_Toe_Assignment
{
    /// <summary>
    /// Class which manages the gameboard 
    /// </summary>
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
        /// <summary>
        /// Places a piece on the board
        /// </summary>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <param name="piece">Piece to be placed</param>
        internal void PlacePiece(int x, int y, string piece)
        {
            board[x, y] = piece;
        }

        /// <summary>
        /// Displays the board in a human readable format.
        /// </summary>
        /// <returns>A human readable gameboard</returns>
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
        /// <summary>
        /// Creates a blank gameboard of all 0's
        /// </summary>
        /// <param name="x">No. rows</param>
        /// <param name="y">No. columns</param>
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

        /// <summary>
        /// Creates a gameboard class with a predefined board
        /// </summary>
        /// <param name="SaveFileBoard">Board to be initialised with</param>
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