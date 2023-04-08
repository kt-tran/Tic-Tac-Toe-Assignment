using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class History
    {
        //fields
        private const string SAVE_FILE_NAME = "Save_File.txt"; //.txt? how do I save an array?
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
        public void MakeSaveFile()
        {
            FileStream outFile = new FileStream(SAVE_FILE_NAME, FileMode.Create);
            outFile.Close();
        }
        public void Log(Gameboard currentboard)
        {
            FileStream outFile = File.Open(SAVE_FILE_NAME, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            for (int i = 0; i < currentboard.Board.GetLength(0); i++)
            {
                for (int j = 0; j < currentboard.Board.GetLength(1); j++)
                {
                    writer.Write(currentboard.Board[i, j]);
                    writer.Write(DELIM_COLUMNS);
                }
                writer.Write(DELIM_ROWS);
            }
            //writer.Close();
        }
        public void LoadSaveFile()
        {
            FileStream inFile = new FileStream(SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
        }
        //constructor
    }
}