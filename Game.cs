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
        protected internal Player[] PlayerList
        {
            get { return playerList; }
            protected set { playerList = value; }
        }
        internal abstract string[] Pieces
        {
            get; set;
        }
        protected internal abstract string Rules
        {
            get;
        }

        internal abstract int CurrentPlayerIndex
        {
            get; set;
        }

        internal abstract Player CurrentPlayer
        { get; }

        internal abstract int GameboardHeight
        {
            get;
        }

        internal abstract int GameboardWidth
        {
            get;
        }

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
            CheckWinner();
            if (!GameOver) //does not log winning turn
            {
                logger.Log(gameboard, CurrentPlayer.PlayerID);
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Length;
            }
        }
        internal abstract void CreateHumanPlayer(int index);

        internal abstract void CreateComputerPlayer(int index, Game game);
        
    }
}