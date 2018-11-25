using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Timers;

namespace Minesweeper
{
    public partial class Board : Form
    {
        public static int Rows { get; set; }
        public static int Cols { get; set; }
        public static int Mines { get; set; }
        private int FlaggedTiles;
        private int FlaggedMines;
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        public int FlagsLeft;
        private bool GameOver;

        public Tile[,] board;

          /***************************************/
         /** Set True to Enable Console Prints **/
        /***************************************/
        private bool debug = true;

        //Populates the board
        public Board(int rows, int cols, int mines)
        {
            Rows = rows;
            Cols = cols;
            Mines = mines;
        }

        public void InitializeBoard()
        {
            board = new Tile[Rows, Cols];
            CreateBoard(Rows, Cols);
            LayMines(Mines);
            CalcAdjTiles();
        }

        //Sets up the board based on the settings
        private void CreateBoard(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j] = new Tile();
                }
            }
        }

        //Sets the amount of mines on board
        private void LayMines(int numMines)
        {
            var random = new Random();
            var minesLeft = numMines;

            while (minesLeft != 0)
            {
                var row = random.Next() % Rows;
                var col = random.Next() % Cols;
                if (!board[row, col].Mine)
                {
                    board[row, col].Mine = true;
                    minesLeft--;
                }
            }
            FlagsLeft = Mines;
        }

        //Counts number of surrounding mines around each space
        private void CalcAdjTiles()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if(board[i, j].Mine)
                    {
                        board[i, j].SurroundingMines = 9;
                    }
                    else
                    {
                        board[i, j].SurroundingMines = SurroundingMines(i, j);
                    }
                }
            }
        }

        //Counts each space for surrounding mines
        private int SurroundingMines(int row, int col)
        {
            int count = 0;
            for (int i = row - 1 ; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if(CheckBounds(i, j) && board[i, j].Mine)
                    {
                        //Console.WriteLine("Checking: " + i + ", " + j + " : " + CheckBounds(i, j));
                        count++;
                    }
                }
            }
            return count;
        }

        private bool CheckBounds(int row, int col)
        {
            return (row >= 0 && row < Rows && col >= 0 && col < Cols);
        }

        //Reveals a space and any surrounding spaces that are free
        public bool Reveal(int row, int col)
        {
            //Make sure tile hasnt been revealed yet
            if (debug)
            {
                Console.WriteLine("-----");
                Console.WriteLine("Checking tile at " + row + "," + col + ".");
                Console.WriteLine("Revealed Status: " + board[row, col].Revealed);
                Console.WriteLine("Surrounding Mines: " + board[row, col].SurroundingMines);
            }

            if (!board[row, col].Flagged && !GameOver)
            {
                if (board[row, col].Revealed)
                {
                    return false;
                }
                board[row, col].Revealed = true;

                if (board[row, col].Mine)
                {
                    SetGameOver();
                    return true;
                }
                else
                {
                    if (board[row, col].SurroundingMines == 0)
                    {
                        for (int i = row - 1; i <= row + 1; i++)
                        {
                            for (int j = col - 1; j <= col + 1; j++)
                            {
                                //Keep revealing
                                if (CheckBounds(i, j) && !board[i, j].Mine)
                                {
                                    Reveal(i, j);
                                }
                            }
                        }
                    }
                }
                //Check surrounding space and reveal if no mines are there
                return true;
            }
            return false;
        }

        //Set flag on space
        public void SetFlag(int row, int col)
        {
            //Only flag a space if there are flags left to use
            if (FlagsLeft > 0 && !GameOver)
            {
                if (!board[row, col].Flagged && !board[row, col].Revealed)
                {
                    board[row, col].Flagged = true;
                    FlagsLeft--;
                    //Check if game is won
                    if (CheckGameWon())
                    {
                        SetGameOver();
                    }
                }
                else if (board[row, col].Flagged && !board[row, col].Revealed)
                {
                    board[row, col].Flagged = false;
                    FlagsLeft++;
                }
                //Check tile if flag is correct
                CheckTile(row, col);
            }
        }

        public bool IsFlagged(int row, int col)
        {
            return board[row, col].Flagged;
        }

        public bool IsRevealed( int row, int col)
        {
            return board[row, col].Revealed;
        }

        public bool IsMine(int row, int col)
        {
            return board[row, col].Mine;
        }

        public int GetSurroundingMines(int row, int col)
        {
            return board[row, col].SurroundingMines;
        }

        //Only called after setting flag
        private void CheckTile( int row, int col)
        {
            if(!board[row, col].Revealed)
            {
                if (board[row, col].Flagged)
                {
                    if (board[row, col].Mine)
                    {
                        FlaggedMines++;
                        FlaggedTiles++;
                    }
                    else
                    {
                        FlaggedTiles++;
                    }
                }
                else
                {
                    if (board[row, col].Mine)
                    {
                        FlaggedMines--;
                        FlaggedTiles--;
                    }
                    else
                    {
                        FlaggedTiles--;
                    }
                }
            }
        }


        //Returns true if the number of flagged mines equals total mines and flagged spaces
        private bool CheckGameWon()
        {
            return (FlaggedMines == Mines) && (FlaggedMines == FlaggedTiles);
        }

        private void SetGameOver()
        {
            timer.Stop(); 
            GameOver = true;
        }

        public bool GetBoardGameOver()
        {
            return GameOver;
        }

          /********************************/
         /* Test functions for debugging */
        /********************************/

        public string InitPrint()
        {
            string pboard = "";

            if(debug)
            {
                Console.WriteLine(" ------- ");
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (board[i, j].Mine)
                        {
                            pboard += "[*]";
                        }
                        else if (board[i, j].SurroundingMines > 0)
                        {
                            pboard += "[" + board[i, j].SurroundingMines + "]";
                        }
                        else
                        {
                            pboard += "[ ]";
                        }
                    }
                    pboard += Environment.NewLine;
                }
            }
            return pboard;
        }

        public string FlagPrint()
        {
            string pboard = "";

            if (debug)
            {
                Console.WriteLine(" ------- ");
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (!board[i, j].Flagged)
                        {
                            if (board[i, j].Mine)
                            {
                                pboard += "[*]";
                            }
                            else if (board[i, j].SurroundingMines > 0)
                            {
                                pboard += "[" + board[i, j].SurroundingMines + "]";
                            }
                            else
                            {
                                pboard += "[ ]";
                            }
                        }
                        else
                        {
                            pboard += "[F]";
                        }
                    }
                    pboard += Environment.NewLine;
                }
                Console.WriteLine("Flagged Mines: " + FlaggedMines);
                Console.WriteLine("Flagged Tiles: " + FlaggedTiles);
            }
            return pboard;
        }

        public string RevPrint()
        {
            string pboard = "";

            if (debug)
            {
                Console.WriteLine(" ------- ");
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (!board[i, j].Revealed)
                        {
                            if (board[i, j].Mine)
                            {
                                pboard += "[*]";
                            }
                            else if (board[i, j].SurroundingMines > 0)
                            {
                                pboard += "[" + board[i, j].SurroundingMines + "]";
                            }
                            else
                            {
                                pboard += "[ ]";
                            }
                        }
                        else
                        {
                            pboard += "[R]";
                        }
                    }
                    pboard += Environment.NewLine;
                }
            }
            return pboard;
        }
    }
}