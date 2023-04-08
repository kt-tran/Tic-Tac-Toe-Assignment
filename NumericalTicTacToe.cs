using System;
using System.IO;
using static System.Console;

namespace Tic_Tac_Toe_Assignment
{
    internal class NumericalTicTacToe : Game
    {
        //fields
        private const int PLAYERS_COUNT = 2;
        private const int HEIGHT_OF_GAMEBOARD = 3;
        private const int WIDTH_OF_GAMEBOARD = 3;
        private const int WIN_TOTAL = 15;
        private string[] NTTpieces = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };


        //properties
        public override bool IsGameOver
        {
            get; set;
        }
        public override int CurrentPlayerIndex
        {
            get; protected set;
        }
        public override string[] Pieces
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

        public override string Rules
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

        public override int PlayersCount
        {
            get { return PLAYERS_COUNT; }
        }

        //methods
        public override void CreateHumanPlayer(int index)
        {
            PlayerList[index] = new HumanPlayer();
            PlayerList[index].PlayerID = index + 1;
        }

        public override void CreateComputerPlayer(int index, Game game)
        {
            PlayerList[index] = new ComputerPlayer(game); //check if this works
            PlayerList[index].PlayerID = index + 1;
        }
        private bool validateMove(int x, int y, string piece, int player)
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

        public override bool makeMove(Player player)
        {
            WriteLine("It's Player {0}'s turn.\n", player.PlayerID);

            if (player.GetType() == typeof(ComputerPlayer)) //if the player is a computer
            {
                player.getMove();
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
                    player.getMove();
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
                    player.getMove();
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
                    player.getMove();
                    pieceCheckType = int.TryParse(player.Input, out piece);
                    if (!pieceCheckType)
                    {
                        WriteLine("That was not a number between 1 and 9 (inclusive). Please try again.");
                        continue;
                    }
                }

                if (!validateMove(x, y, player.Input, player.PlayerID))
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
                    gameboard.placePiece(x, y, player.Input);
                    return turnSuccess = true;
                }
            }
        }

        public override void isGameOver()
        {
            int total = 0;
            //check horizontally for a win
            for (int x = 0; x < gameboard.Board.GetLength(0); x++)
            {
                for (int y = 0; y < gameboard.Board.GetLength(1); y++)
                {
                    total += int.Parse(gameboard.Board[x, y]);
                    if (total == 15)
                        IsGameOver = true;
                }
                total = 0; //reset after each row
            }

            //check vertically for a win
            for (int y = 0; y < gameboard.Board.GetLength(1); y++)
            {
                for (int x = 0; x < gameboard.Board.GetLength(0); x++)
                {
                    total += int.Parse(gameboard.Board[x, y]);
                    if (total == 15)
                        IsGameOver = true;
                }
                total = 0; //reset after each column
            }

            //check diagonally for a win
            for (int x = 0, y = 0; x < gameboard.Board.GetLength(0); x++, y++) //checks \ diagonal, from top left to bottom right 
            {
                total += int.Parse(gameboard.Board[x, y]);
                if (total == 15)
                    IsGameOver = true;
            }
            total = 0; //reset before checking the other diagonal direction

            for (int x = 0, y = 2; x < gameboard.Board.GetLength(0); x++, y--) //checks / diagonal, from bottom left to top right
            {
                total += int.Parse(gameboard.Board[x, y]);
                if (total == 15)
                    IsGameOver = true;
            }
        }

        //constructor
        public NumericalTicTacToe()
        {
            gameboard = new Gameboard(HEIGHT_OF_GAMEBOARD, WIDTH_OF_GAMEBOARD);
            PlayerList = new Player[PLAYERS_COUNT];
            helpS = new HelpSystem();
            helpS.GameRules = Rules;
            base.logger = new History();
            base.outFile = base.logger.MakeSaveFile();
        }
    }
}