using System;
namespace SharpMines
{
    public class MineField
    {
        private bool[][] field;
        private int xsize, ysize;
        private Window gui;

        public MineField(int xSize, int ySize, Window w)
        {
            this.gui = w;
            // using jagged array in c#
            this.field = new bool[xSize][];
            for (int i = 0; i < xSize; i++)
            {
                this.field[i] = new bool[ySize];
            }
            this.xsize = xSize;
            this.ysize = ySize;
            this.spreadMines();
        }

        private void spreadMines()
        {
            Random r = new Random();
            for (int i = 0; i < this.xsize; i++)
            {
                for (int j = 0; j < this.ysize; j++)
                {

                    if (((int)r.Next()) % 7 == 0)
                    {
                        this.field[i][j] = true;
                    }
                    else
                    {
                        this.field[i][j] = false;
                    }
                }
            }
        }

        public bool isExplosive(int x, int y)
        {
            if (((x >= 0) && (x < this.xsize)) && ((y >= 0) && (y < this.ysize)))
            {
                return this.field[x][y];
            }
            else
            {
                return false;
            }
        }

        public int explosiveNeighbors(int x, int y)
        {
            int n = 0;
            if (this.isExplosive(x - 1, y - 1)) { n++; }
            if (this.isExplosive(x - 1, y)) { n++; }
            if (this.isExplosive(x - 1, y + 1)) { n++; }
            if (this.isExplosive(x, y - 1)) { n++; }
            if (this.isExplosive(x, y + 1)) { n++; }
            if (this.isExplosive(x + 1, y - 1)) { n++; }
            if (this.isExplosive(x + 1, y)) { n++; }
            if (this.isExplosive(x + 1, y + 1)) { n++; }
            return n;
        }

        public void expandZero(int x, int y)
        {
            if ((x >= 0) && (x < this.xsize) && (y >= 0) && (y < this.ysize))
            {
                if ((this.gui.buttons[x][y].Label != "0") && (this.explosiveNeighbors(x, y) == 0))
                {
                    this.gui.buttons[x][y].Label = "0";
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if ((x + i >= 0) && (x + i < this.xsize) && (y + j >= 0) && (y + j < this.ysize))
                            {
                                if ((this.explosiveNeighbors(x + i, y + j) == 0) && (this.gui.buttons[x + i][y + j].Label != "0"))
                                {
                                    this.expandZero(x + i, y + j);
                                    this.unmaskNeighbors(x + i, y + j);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void unmaskNeighbors(int x, int y)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((x + i >= 0) && (x + i < this.xsize) && (y + j >= 0) && (y + j < this.ysize))
                    {
                        this.gui.buttons[x + i][y + j].Label = this.explosiveNeighbors(x + i, y + j).ToString();
                    }
                }
            }

        }

        public Window GetWindow()
        {
            return this.gui;
        }

        public void reset()
        {
            this.spreadMines();
        }

    }
}