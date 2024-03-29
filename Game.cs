﻿using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    /**
     * Template class, manages main game loop and game dependent systems.
     * Can be extended to create different types of games. 
     */
    internal abstract class Game
    {
        //fields
        private Player[] playerList;
        public Gameboard gameboard;
        public HelpSystem helpS;
        public History logger;
        protected Gameboard[] boardHistory;
        protected Gameboard[] boardRedo;
        protected string[] pieceHistory;
        protected string[] pieceRedo;

        //properties
        internal abstract Gameboard[] BoardHistory
        {
            get; set;
        }
        internal abstract string GamePlayerMode
        {
            get; set;
        }
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

        internal abstract void UndoMove();

        internal abstract void RedoMove();

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
                logger.SaveToFile(gameboard, CurrentPlayer.PlayerID);
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Length;
            }
        }
        internal abstract void CreateHumanPlayer(int index);

        internal abstract void CreateComputerPlayer(int index, Game game);
        
    }
}