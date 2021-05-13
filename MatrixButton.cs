using Gtk;

public class MatrixButton : Button
{
    public int xpos, ypos;
    public MineField mf;

    public MatrixButton(string label, int x, int y)
    {
        this.xpos = x;
        this.ypos = y;
        Button::setLabel(label);
        return this;
    }

    public void setMinefield(MineField minefield)
    {
        this.mf = minefield;
    }

    public MineField GetMineField()
    {
        return this.mf;
    }

    public void setPosition(int x, int y)
    {
        this.xpos = x;
        this.ypos = y;
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