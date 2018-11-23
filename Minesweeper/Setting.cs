using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Setting : Form
    {
        public static int DEF_ROWS = 8;
        public static int DEF_COLUMNS = 8;
        public static int DEF_MINES = 10;

        public struct Data
        {
            public int Rows { get; set; }
            public int Cols { get; set; }
            public int Mines { get; set; }

            public static bool operator ==(Data data1, Data data2) => Equals(data1, data2);
            public static bool operator !=(Data data1, Data data2) => !Equals(data1, data2);
            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                var data = (Data)obj;
                return (Rows == data.Rows) && (Cols == data.Cols) && (Mines == data.Mines);
            }
            public override int GetHashCode() => Rows.GetHashCode() ^ Cols.GetHashCode() ^ Mines.GetHashCode();
        };

        public Data oldData;

        public Setting()
        {
            InitializeComponent();
        }

        //Load default values into the struct
        private void Setting_Load(object sender, EventArgs e)
        {
            oldData.Rows = Convert.ToInt32(Math.Round(rowsNumberBox.Value, 0));
            oldData.Cols = Convert.ToInt32(Math.Round(columnsNumberBox.Value, 0));
            oldData.Mines = Convert.ToInt32(Math.Round(minesNumberBox.Value, 0));
        }

        //Sets default values when button is clicked
        private void DefaultButton_Click(object sender, EventArgs e)
        {
            rowsNumberBox.Value = DEF_ROWS;
            columnsNumberBox.Value = DEF_COLUMNS;
            minesNumberBox.Value = DEF_MINES;
        }

        private void rowsNumberBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void columnsNumberBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void minesNumberBox_ValueChanged(object sender, EventArgs e)
        {

        }

        //Function for when form is closing
        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data newData = new Data {
                Rows = Convert.ToInt32(Math.Round(rowsNumberBox.Value, 0)),
                Cols = Convert.ToInt32(Math.Round(columnsNumberBox.Value, 0)),
                Mines = Convert.ToInt32(Math.Round(minesNumberBox.Value, 0))
            };

            //Compare old and new data. Displays message on attempted close if they aren't equal
            if (newData != oldData)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save your changes", "Confirm", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    SaveValues();
                    e.Cancel = true;
                    Hide();
                }
                else
                {
                    RevertValues(oldData);
                }
            }
        }

        //Save values to Board globals before closing menu
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SaveValues();
            Hide();
        }

        //Sets data to values passed into this function
        private void RevertValues(Data data)
        {
            rowsNumberBox.Value = data.Rows;
            columnsNumberBox.Value = data.Cols;
            minesNumberBox.Value = data.Mines;
        }

        //Saves values to Board variables
        private void SaveValues()
        {
            Board.Rows = Convert.ToInt32(Math.Round(rowsNumberBox.Value, 0));
            Board.Cols = Convert.ToInt32(Math.Round(columnsNumberBox.Value, 0));
            Board.Mines = Convert.ToInt32(Math.Round(minesNumberBox.Value, 0));
        }

        //EXCEPTION HANDLER FOR MINES > ROWS * COLS
    }
}
