using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class History
    {
        //fields
        private Game historyGame;
        private const string SAVE_FILE_NAME = "Save_File.txt";
        private char DELIM_COLUMNS = ',';
        private char DELIM_ROWS = '|';
        private Gameboard[] boardHistory = new Gameboard[9];
        private string[,] pieceHistory; 
        private Gameboard[] backupUndo;
        private Gameboard[] backupRedo;

        //properties
        public string[,] PieceHistory
        {
            get { return pieceHistory; }
            set { pieceHistory = value; }
        }
        public Gameboard[] BoardHistory
        {
            get { return boardHistory; }
            set { boardHistory = value; }
        }
        public Gameboard[] BackupUndo
        {
            get { return backupUndo; }
            set { backupUndo = value; }
        }

        public Gameboard[] BackupRedo
        {
            get { return backupRedo; }
            set { backupRedo = value; }
        }
        //methods
        internal void MakeSaveFile()
        {
            FileStream outFile = new FileStream(SAVE_FILE_NAME, FileMode.Create);
            outFile.Close();
        }
        internal void Log(Gameboard currentboard)
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
            writer.Write(Environment.NewLine);
            writer.Close();
        }
        internal Gameboard LoadSaveFile()
        {
            FileStream inFile = new FileStream(SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string boardIn = reader.ReadLine();
            string[,] reassembledBoard = new string[historyGame.GameboardHeight, historyGame.GameboardWidth];
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
                    }
                }
                boardIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();

            return new Gameboard(reassembledBoard);
        }

        internal bool CheckSaveFile()
        {
            FileInfo fInfo = new FileInfo(SAVE_FILE_NAME);
            return fInfo.Exists;

        }
        //constructor
        internal History(Game game)
        {
            historyGame = game;
        }
    }
}