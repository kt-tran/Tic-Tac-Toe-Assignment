using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    abstract class Game
    {
        //fields
        private bool gameOver = false;
        private Player[] playerList;
        public Gameboard gameboard;
        public HelpSystem helpSystem;
        public History logger;
        protected FileStream outFile;

        //properties
        public abstract bool IsGameOver
        {
            get; set;
        }
        public Player[] PlayerList
        {
            get { return playerList; }
            set { playerList = value; }
        }
        public abstract string[] Pieces
        {
            get; set;
        }
        public abstract string Rules
        {
            get;
        }

        public abstract int PlayersCount
        {
            get;
        }

        public abstract int CurrentPlayerIndex
        {
            get; protected set;
        }
        //methods
        public abstract bool makeMove(Player player);

        public abstract void isGameOver();
        public void gameTurn()
        {
            bool turnComplete = false;
            while (!turnComplete)
            {
                turnComplete = makeMove(PlayerList[CurrentPlayerIndex]);
            }
            logger.Log(outFile, gameboard);
            isGameOver();
            if (!IsGameOver)
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Length;
        }
        public abstract void CreateHumanPlayer(int index);

        public abstract void CreateComputerPlayer(int index, Game game);
        //constructor: TODO implement abstract constructor & have children inherit
    }
}