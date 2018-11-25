using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Timers;

namespace Minesweeper
{
    public partial class mainUI : Form
    {
        private Board board;
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        private int GameTime;
        delegate void SetTextCallback(string text);
        private int FlagsLeft;

        public mainUI()
        {
            InitializeComponent();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Rows: " + Board.Rows);
            Console.WriteLine("Cols: " + Board.Cols);
            Console.WriteLine("Mines: " + Board.Mines);
            NewGame();
            SetTimer();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settings = new Setting();
            settings.Show();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainUI_Load(object sender, EventArgs e)
        {
            Board.Rows = Setting.DEF_ROWS;
            Board.Cols = Setting.DEF_COLUMNS;
            Board.Mines = Setting.DEF_MINES;
        }

        private void NewGame()
        {
            board = new Board(Board.Rows, Board.Cols, Board.Mines);

            //UI Side Functions
            ClearTable();
            SetTable(Board.Rows, Board.Cols);

            //Gamebaord Side Functions
            board.InitializeBoard();
            FlagCountTextUpdate();
            Console.WriteLine(board.InitPrint());
        }

        //Set gameboard with a grid of buttons equal to parameters
        private void SetTable(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Fill,
                        Margin = Padding.Empty,
                        Tag = new Point(i, j),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        BackgroundImage = Minesweeper.Properties.Resources.closed,
                        AutoSize = true
                    };
                    button.MouseDown += Button_MouseDown;

                    //Add controls to button then set it
                    Gameboard.Controls.Add(button, j, i);
                }
            }
        }

        private void ClearTable()
        {
            //Clear styles to reset game
            Gameboard.Controls.Clear();
            Gameboard.RowStyles.Clear();
            Gameboard.ColumnStyles.Clear();

            //Set new row and column count
            Gameboard.RowCount = Board.Rows;
            Gameboard.ColumnCount = Board.Cols;

            for (int i = 0; i < Board.Rows; i++)
            {
                Gameboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            }
            for (int j = 0; j < Board.Cols; j++)
            {
                Gameboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            }
        }

        //Function for mouse actions
        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Point location = (Point)button.Tag;
            Console.WriteLine(" ------- ");
            Console.WriteLine("Tile at " + location.X + "," + location.Y + " clicked. ");

            //Check if game is ongoing
            if (board.GetBoardGameOver())
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                //Reveal
                Console.WriteLine("Reveal");
                board.Reveal(location.X, location.Y);
                SetRevealSpace(location.X, location.Y);
                Console.WriteLine(board.RevPrint());
            }
            else if (e.Button == MouseButtons.Right) 
            {
                //Flag
                Console.WriteLine("Flag");
                SetFlagSpace(location.X, location.Y);
                board.SetFlag(location.X, location.Y);
                Console.WriteLine(board.FlagPrint());
            }
            FlagCountTextUpdate();
        }

        public void SetFlagSpace( int row, int col )
        {
            var button = (Button)Gameboard.GetControlFromPosition(col, row);

            if (FlagsLeft > 0)
            {
                if (!board.IsRevealed(row, col))
                {
                    if (!board.IsFlagged(row, col))
                    {
                        button.BackgroundImage = Minesweeper.Properties.Resources.flag;
                    }
                    else
                    {
                        button.BackgroundImage = Minesweeper.Properties.Resources.closed;
                    }
                }
                FlagCountTextUpdate();
            }
        }

        public void SetRevealSpace(int row, int col)
        {
            var button = (Button)Gameboard.GetControlFromPosition(col, row);
            UpdateBoard();
            if (!board.IsFlagged(row, col))
            {
                if(!board.IsRevealed(row, col))
                {
                    switch (board.GetSurroundingMines(row, col))
                    {
                        case 0:
                            button.BackgroundImage = Minesweeper.Properties.Resources._0;
                            break;
                        case 1:
                            button.BackgroundImage = Minesweeper.Properties.Resources._1;
                            break;
                        case 2:
                            button.BackgroundImage = Minesweeper.Properties.Resources._2;
                            break;
                        case 3:
                            button.BackgroundImage = Minesweeper.Properties.Resources._3;
                            break;
                        case 4:
                            button.BackgroundImage = Minesweeper.Properties.Resources._4;
                            break;
                        case 5:
                            button.BackgroundImage = Minesweeper.Properties.Resources._5;
                            break;
                        case 6:
                            button.BackgroundImage = Minesweeper.Properties.Resources._6;
                            break;
                        case 7:
                            button.BackgroundImage = Minesweeper.Properties.Resources._7;
                            break;
                        case 8:
                            button.BackgroundImage = Minesweeper.Properties.Resources._8;
                            break;
                    }
                }
                if (board.IsMine(row, col))
                {
                    button.BackgroundImage = Minesweeper.Properties.Resources.mine2;
                    //Event to end game
                }
            }
        }

        public void SetMineSpace( int row, int col)
        {
            var button = (Button)Gameboard.GetControlFromPosition(col, row);
            button.BackgroundImage = Minesweeper.Properties.Resources.mine;
        }

        //UpdatesBoard after setting revealing numbers
        private void UpdateBoard()
        {
            for ( int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Cols; j++)
                {
                    var button = (Button)Gameboard.GetControlFromPosition(j, i);
                    if (board.IsRevealed(i, j))
                    {
                        switch (board.GetSurroundingMines(i, j))
                        {
                            case 0:
                                button.BackgroundImage = Minesweeper.Properties.Resources._0;
                                break;
                            case 1:
                                button.BackgroundImage = Minesweeper.Properties.Resources._1;
                                break;
                            case 2:
                                button.BackgroundImage = Minesweeper.Properties.Resources._2;
                                break;
                            case 3:
                                button.BackgroundImage = Minesweeper.Properties.Resources._3;
                                break;
                            case 4:
                                button.BackgroundImage = Minesweeper.Properties.Resources._4;
                                break;
                            case 5:
                                button.BackgroundImage = Minesweeper.Properties.Resources._5;
                                break;
                            case 6:
                                button.BackgroundImage = Minesweeper.Properties.Resources._6;
                                break;
                            case 7:
                                button.BackgroundImage = Minesweeper.Properties.Resources._7;
                                break;
                            case 8:
                                button.BackgroundImage = Minesweeper.Properties.Resources._8;
                                break;
                        }
                    }
                }
            }
        }

          /**********************************/
         /*** UI Text Box and Timer Code ***/
        /**********************************/
        private void SetTimer()
        {
            GameTime = 0;
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(GameTimeUpdate);
            timer.Start();
        }

        private void GameTimeUpdate(object source, ElapsedEventArgs e)
        {
            if (board.GetBoardGameOver())
            {
                timer.Stop();
            }
            GameTime++;
            SetTimerText(GameTime.ToString());
        }

        private void SetTimerText(string text)
        {
            // InvokeRequired compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.TimerBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTimerText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.TimerBox.Text = text;
            }
        }

        private void FlagCountTextUpdate()
        {
            FlagsLeft = board.FlagsLeft;
            Console.WriteLine("Flags Left: " + FlagsLeft);
            FlagsLeftText.Text = FlagsLeft.ToString();
        }
    }
}
