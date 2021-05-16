//using System;
using Gtk;

namespace SharpMines
{
    public class MatrixButton : Gtk.Button
    {
        public int xpos, ypos;
        public MineField mf;

        public MatrixButton(string label, int x, int y, MineField mineField) : base(label)
        {
            this.xpos = x;
            this.ypos = y;
            this.mf = mineField;
        }

        public int getXPosition()
        {
            return this.xpos;
        }
        public int getYPosition()
        {
            return this.ypos;
        }
    }
}
