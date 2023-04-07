﻿using System;
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

        //methods
        public abstract bool makeMove(Player player);

        public abstract bool isGameOver();
        public bool gameTurn(Player currentPlayer)
        {

            bool turnComplete = false;
            while (!turnComplete)
            {
                turnComplete = makeMove(currentPlayer);
            }
            logger.Log(outFile, gameboard);
            return isGameOver();
        }
        public abstract void CreateHumanPlayer(int index);

        public abstract void CreateComputerPlayer(int index, Game game);

        //constructor: TODO implement abstract constructor & have children inherit
    }
}