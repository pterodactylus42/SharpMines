using Gtk;
using System;

class MineGui : Window
{

    private MineField minefield;
    private GTKPanel panel;
    private GTKLabel label;
    public MatrixButton[][] buttons;

    private void initButtonMatrix()
    {
        for (int x = 0; x < this.minefield.xsize; x++)
        {
            for (int y = 0; y < this.minefield.ysize; y++)
            {
                this.buttons[x][y] = new MatrixButton();
                this.buttons[x][y].setPos(x, y);
                // not quite sure why this is necessary...
                //this.buttons[x][y].mf = mf;
            }
        }

    }

    public MineGui(MineField mf)
    {
        Window::setLabel("SharpMines");
        this.minefield = mf;
        this.buttons = new MatrixButton[minefield.xsize][minefield.ysize];
        this.initButtonMatrix();
        Window::setSize(70 * minefield.xsize, 70 * minefield.ysize);
        for (int x = 0; x < mf.xsize; x++)
        {
            for (int y = 0; y < mf.ysize; y++)
            {
                buttons[x][y].setLabel("?");
                //panel.add(buttons[x][y]);
            }
        }
    }
}
