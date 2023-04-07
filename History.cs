using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class History
    {
        //fields
        private const string SAVE_FILE_NAME = "Save_File.txt"; //.txt? how do I save an array?
        private Gameboard[] boardHistory;
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
        public FileStream MakeSaveFile()
        {
            FileStream outFile = new FileStream(SAVE_FILE_NAME, FileMode.Create, FileAccess.Write);
            return outFile;
        }
        public void Log(FileStream outFile, Gameboard currentboard)
        {
            StreamWriter writer = new StreamWriter(outFile);
            writer.WriteLine(currentboard.ToString());
        }
        public void LoadSaveFile()
        {
            //TODO
        }
        //constructor
    }
}