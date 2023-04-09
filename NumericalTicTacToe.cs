using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class NumericalTicTacToe : Game
    {
        //fields
        private const int HEIGHT_OF_GAMEBOARD = 3;
        private const int WIDTH_OF_GAMEBOARD = 3;
        private const int WIN_TOTAL = 15;
        private string[] NTTpieces = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private int moveCounter = 1;
        private int undoCounter = 0;

        //properties

        internal override int GameboardHeight
        {
            get { return HEIGHT_OF_GAMEBOARD; }
        }

        internal override int GameboardWidth
        {
            get { return WIDTH_OF_GAMEBOARD;  }
        }

        internal override bool GameOver
        {
            get; set;
        }
        internal override int CurrentPlayerIndex
        {
            get; set;
        }
        internal override Player CurrentPlayer
        { 
            get { return PlayerList[CurrentPlayerIndex]; } 
        }
        internal override string[] Pieces
        {
            get
            {
                return NTTpieces;
            }
            set
            {
                NTTpieces = value;
            }
        }

        protected internal override string Rules
        {
            get
            {
                return "\nNumerical Tic Tac Toe is a variation of the classic Tic-Tac-Toe game.\n\n" +
                        "  1. Two players take turns placing the numbers 1 to 9 on a 3x3 board.\n" +
                        "  2. The first player plays with the odd numbers, the second player plays with the even numbers.\n" +
                        "  3. All numbers can be used only once.\n\n" +
                        "The player who first puts down exactly 15 points in a line\n" +
                        "(sum of a horizontal, vertical, or diagonal row of three numbers) " +
                        "wins the game.\n" +
                        "\n" +
                        "You can make a turn on the board using the row number and column number.\n" +
                        "For example, the number 7 has been placed on row 2, column 3.\n" +
                        "\n" +
                        "          column\n" +
                        "          1 2 3\r\n  row 1  |0|0|0|\r\n  row 2  |0|0|7|\r\n  row 3  |0|0|0|" +
                        "\n";
            }
        }

        protected override int PlayersCount
        {
            get { return 2; }
        }
        //methods
        internal override void CreateHumanPlayer(int index)
        {
            PlayerList[index] = new HumanPlayer();
            PlayerList[index].PlayerID = index + 1;
        }

        internal override void CreateComputerPlayer(int index, Game game)
        {
            PlayerList[index] = new ComputerPlayer(game); //check if this works
            PlayerList[index].PlayerID = index + 1;
        }
        private bool ValidateMove(int x, int y, string piece, int player)
        {
            bool playerMatch = false;
            int intPiece2 = int.Parse(piece);
            playerMatch = (player % 2 == intPiece2 % 2); //player 1 (odd), player 2 (even)

            bool spareMove = false;
            for (int i = 0; i < Pieces.Length; i++)
            {
                if (piece == Pieces[i]) //if that piece has not been used yet
                {
                    if (gameboard.Board[x, y] == "0") //if that cell on the gameboard is empty
                    {
                        spareMove = true;
                        Pieces[i] = "0";
                    }

                }
            }

            if (playerMatch & spareMove)
            {
                return true;
            }
            else { return false; }
        }

        protected override bool MakeMove(Player player)
        {
            WriteLine("It's Player {0}'s turn.\n", player.PlayerID);

            if (player.GetType() == typeof(ComputerPlayer)) //if the player is a computer
            {
                player.GetMove();
                SaveMove(player.Input);
                // ClearRedoList();
                return true;
            }
            else
            {
                bool turnSuccess = false;
                bool withinGrid = false;
                bool xCheckType = false;
                bool yCheckType = false;
                bool pieceCheckType = false;

                int x = -1;
                int y = -1;
                int piece = -1;

                while (!withinGrid || !xCheckType)
                {
                    Write("Enter the row of your move:");
                    player.GetMove();
                    xCheckType = int.TryParse(player.Input, out x);

                    if (!xCheckType)
                    {
                        WriteLine("That was not an integer. Please try again.");
                        continue;
                    }

                    if (x > WIDTH_OF_GAMEBOARD || x < 1)
                    {
                        WriteLine("That is outside of the board. Please try again.");
                        continue;
                    }
                    x -= 1;
                    withinGrid = true;
                }

                withinGrid = false;
                while (!withinGrid || !yCheckType)
                {
                    Write("Enter the column of your move:");
                    player.GetMove();
                    yCheckType = int.TryParse(player.Input, out y);

                    if (!yCheckType)
                    {
                        WriteLine("That was not an integer. Please try again.");
                        continue;
                    }

                    if (y > HEIGHT_OF_GAMEBOARD || y < 1)
                    {
                        WriteLine("That is outside of the board. Please try again.");
                        continue;
                    }
                    y -= 1;
                    withinGrid = true;
                }


                while (!pieceCheckType)
                {
                    Write("Enter the number you wish to place:");
                    player.GetMove();
                    pieceCheckType = int.TryParse(player.Input, out piece);
                    if (!pieceCheckType)
                    {
                        WriteLine("That was not a number. Please try again.");
                        continue;
                    }
                }

                if (!ValidateMove(x, y, player.Input, player.PlayerID))
                {
                    WriteLine("\nThat was not a valid move. Either:\n" +
                        "- that move has been used, or\n" +
                        "- you cannot use that piece, or\n" +
                        "- that cell on the gameboard is taken.\n" +
                        "Please try again.\n");
                    return turnSuccess;
                }
                else
                {
                    gameboard.PlacePiece(x, y, player.Input);
                    SaveMove(piece.ToString());
                    ClearRedoList();
                    return turnSuccess = true;
                }
            }
        }

        protected override void CheckWinner()
        {
            int total = 0;
            //check horizontally for a win
            for (int x = 0; x < gameboard.Board.GetLength(0); x++)
            {
                for (int y = 0; y < gameboard.Board.GetLength(1); y++)
                {
                    total += int.Parse(gameboard.Board[x, y]);
                    if (total == WIN_TOTAL)
                        GameOver = true;
                }
                total = 0; //reset after each row
            }

            //check vertically for a win
            for (int y = 0; y < gameboard.Board.GetLength(1); y++)
            {
                for (int x = 0; x < gameboard.Board.GetLength(0); x++)
                {
                    total += int.Parse(gameboard.Board[x, y]);
                    if (total == WIN_TOTAL)
                        GameOver = true;
                }
                total = 0; //reset after each column
            }

            //check diagonally for a win
            for (int x = 0, y = 0; x < gameboard.Board.GetLength(0); x++, y++) //checks \ diagonal, from top left to bottom right 
            {
                total += int.Parse(gameboard.Board[x, y]);
                if (total == WIN_TOTAL)
                    GameOver = true;
            }
            total = 0; //reset before checking the other diagonal direction

            for (int x = 0, y = 2; x < gameboard.Board.GetLength(0); x++, y--) //checks / diagonal, from bottom left to top right
            {
                total += int.Parse(gameboard.Board[x, y]);
                if (total == WIN_TOTAL)
                    GameOver = true;
            }
        }
        private void SaveMove(string piece)
        {
            boardHistory[moveCounter] = new Gameboard(gameboard.Board);
            pieceHistory[moveCounter-1] = piece;
            moveCounter++;
        }

        internal override void UndoMove()
        {
            if (moveCounter == 1)
            {
                WriteLine("No moves have been made, cannot undo!");
            }
            else
            {
                boardRedo[undoCounter] = new Gameboard(gameboard.Board);
                pieceRedo[undoCounter] = pieceHistory[moveCounter - 2];
                undoCounter++;

                moveCounter--;
                gameboard = new Gameboard(boardHistory[moveCounter - 1].Board);
                for (int i = 0; i < NTTpieces.Length; i++)
                {
                    if (NTTpieces[i] == "0")
                    {
                        NTTpieces[i] = pieceHistory[moveCounter - 1];
                        break;
                    }
                }
                logger.SaveToFile(gameboard, CurrentPlayer.PlayerID);
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Length;
            }
        }

        internal override void RedoMove()
        {
            if (undoCounter == 0)
            {
                WriteLine("No moves have been undone, cannot redo!");
            }
            else
            {
                undoCounter--;
                gameboard = new Gameboard(boardRedo[undoCounter].Board);
                for (int i = 0; i < NTTpieces.Length; i++)
                {
                    if (NTTpieces[i] == pieceRedo[undoCounter])
                    {
                        NTTpieces[i] = "0";
                        break;
                    }
                }
                logger.SaveToFile(gameboard, CurrentPlayer.PlayerID);
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Length;
                SaveMove(pieceRedo[undoCounter]);
            }
        }

        private void ClearRedoList()
        {
            undoCounter = 0;
        }

        //constructor
        internal NumericalTicTacToe()
        {
            gameboard = new Gameboard(HEIGHT_OF_GAMEBOARD, WIDTH_OF_GAMEBOARD);
            PlayerList = new Player[PlayersCount];
            helpS = new HelpSystem();
            helpS.GameRules = Rules;

            boardHistory = new Gameboard[GameboardHeight * GameboardWidth + 1];
            boardHistory[0] = new Gameboard(GameboardHeight, GameboardWidth);
            boardRedo = new Gameboard[GameboardHeight * GameboardWidth + 1];

            pieceHistory = new string[NTTpieces.Length];
            pieceRedo = new string[NTTpieces.Length]; 

            base.logger = new History(this); // Not sure where else to put this
        }
    }
}