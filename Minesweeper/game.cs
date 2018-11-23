using System;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Game
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainUI());
        }
    }
}