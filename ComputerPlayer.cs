using System;
using System.IO;
using System.Numerics;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class ComputerPlayer : Player
    {
        //fields
        Random rnd;
        private Game computerGame;

        //properties
        internal override string Input //will not use this but is mandatory anyway
        {
            get; set;
        }
        //methods
        internal override void GetMove()
        {
            Random rnd = new Random(); //can potentially generate identical values due to default seed value being time-dependent
            bool correctPiece = false;
            int index = 0;
            while (!correctPiece)
            {
                index = rnd.Next(computerGame.Pieces.Length); //generate random indexes for pieces
                int generatedPiece = int.Parse(computerGame.Pieces[index]);
                if (generatedPiece == 0) continue; // Check for 0
                correctPiece = generatedPiece % 2 == PlayerID % 2;
            }

            for (int x = 0; x < computerGame.gameboard.Board.GetLength(0); x++)
            {
                for (int y = 0; y < computerGame.gameboard.Board.GetLength(1); y++)
                {
                    if (computerGame.gameboard.Board[x, y] == "0")
                    {
                        computerGame.gameboard.PlacePiece(x, y, computerGame.Pieces[index]); //place on the first available spot found
                        computerGame.Pieces[index] = "0"; //update list of available pieces
                        return;
                    }
                }
            }
        }
        //constructor
        internal ComputerPlayer(Game game) 
        {
            rnd = new Random();
            computerGame = game;
        }
    }
}