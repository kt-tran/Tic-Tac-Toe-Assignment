using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    /// <summary>
    /// Class which manages the Save file functionality of the program
    /// </summary>
    internal class History
    {
        //fields
        private Game game;
        private const string SAVE_FILE_NAME = "Save_File.txt";
        private char DELIM_COLUMNS = ',';
        private char DELIM_ROWS = '|';
        private char DELIM_PLAYER = '#';

        //properties

        //methods
        /// <summary>
        /// Creates a save file
        /// </summary>
        internal void MakeSaveFile()
        {
            FileStream outFile = new FileStream(SAVE_FILE_NAME, FileMode.Create);
            outFile.Close();
        }

        /// <summary>
        /// Saves current state of the game to the save file
        /// </summary>
        /// <param name="currentboard">Current gameboard</param>
        /// <param name="playerID">Current playerID</param>
        internal void SaveToFile(Gameboard currentboard, int playerID)
        {
            FileStream outFile = File.Open(SAVE_FILE_NAME, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            for (int i = 0; i < currentboard.Board.GetLength(0); i++)
            {
                for (int j = 0; j < currentboard.Board.GetLength(1); j++)
                {
                    writer.Write(currentboard.Board[i, j]);
                    if (j < 2)
                        writer.Write(DELIM_COLUMNS);
                }
                if (i < 2)
                    writer.Write(DELIM_ROWS);
            }
            writer.Write(DELIM_PLAYER);
            writer.Write(playerID);
            writer.Close();
        }

        /// <summary>
        /// Loads game from save file
        /// </summary>
        internal void LoadSaveFile()
        {
            FileStream inFile = new FileStream(SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string boardAndPlayer = reader.ReadLine();
            string[] bAndPlayer = boardAndPlayer.Split(DELIM_PLAYER);
            string boardIn = bAndPlayer[0];
            int lastPlayer = int.Parse(bAndPlayer[1]);
            string[,] reassembledBoard = new string[game.GameboardHeight, game.GameboardWidth];
            string[] rows;
            string[] columnsInRow;
            while (boardIn != null)
            {
                rows = boardIn.Split(DELIM_ROWS);
                for (int x = 0; x < reassembledBoard.GetLength(0); x++)
                {
                    columnsInRow = rows[x].Split(DELIM_COLUMNS);
                    for (int y = 0; y < reassembledBoard.GetLength(1); y++)
                    {
                        reassembledBoard[x, y] = columnsInRow[y];
                        if (!(columnsInRow[y] == "0"))
                        {
                            for(int i = 0; i < game.Pieces.Length; i++)
                            {
                                if (game.Pieces[i] == columnsInRow[y])
                                    game.Pieces[i] = "0";
                            }
                        }
                    }
                }
                boardIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();
            game.gameboard = new Gameboard(reassembledBoard);
            game.BoardHistory[0] = new Gameboard(reassembledBoard);
            game.CurrentPlayerIndex = lastPlayer % game.PlayerList.Length;
        }

        /// <summary>
        /// Checks if save file exists
        /// </summary>
        /// <returns>True if file found, false otherwise</returns>
        internal bool CheckSaveFile()
        {
            FileInfo fInfo = new FileInfo(SAVE_FILE_NAME);
            return fInfo.Exists;

        }

        
        //constructor
        internal History(Game game)
        {
            this.game = game;
        }
    }
}