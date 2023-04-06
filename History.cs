using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class History
    {
        //fields
        private const string SAVE_FILE_NAME = "Save_File.txt";
        //properties

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