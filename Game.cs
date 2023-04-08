using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal abstract class Game
    {
        //fields
        private Player[] playerList;
        public Gameboard gameboard;
        public HelpSystem helpS;
        public History logger;

        //properties
        protected abstract int PlayersCount
        {
            get;
        }
        internal abstract bool GameOver
        {
            get; set;
        }
        protected Player[] PlayerList
        {
            get { return playerList; }
            set { playerList = value; }
        }
        internal abstract string[] Pieces
        {
            get; set;
        }
        protected internal abstract string Rules
        {
            get;
        }

        protected abstract int CurrentPlayerIndex
        {
            get; set;
        }

        internal abstract Player CurrentPlayer
        { get; }

        //methods
        protected abstract bool MakeMove(Player player);

        protected abstract void CheckWinner();
        internal void GameTurn()
        {
            bool turnComplete = false;
            while (!turnComplete)
            {
                turnComplete = MakeMove(PlayerList[CurrentPlayerIndex]);
            }
            logger.Log(gameboard);
            CheckWinner();
            if (!GameOver)
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Length;
        }
        internal abstract void CreateHumanPlayer(int index);

        internal abstract void CreateComputerPlayer(int index, Game game);
        
    }
}