using System;

namespace Minesweeper
{
    public class Tile
    {
        public bool Flagged { get; set; }

        public bool Revealed { get; set; }

        public bool Mine { get; set; }

        public int SurroundingMines { get; set; }
    }
}