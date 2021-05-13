using System;

public class MineField
{
    private bool[][] field;
    private MineGui gui;
    public int xsize, ysize;

    public MineField()
    {
        this.field = null;
        this.gui = null;
        this.xsize = 0;
        this.ysize = 0;
    }

    private MineField construct(int xSize, int ySize)
    {
        this.field = new bool[xSize][ySize];
        this.xsize = xSize;
        this.ysize = ySize;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                this.field[i][j] = false;
            }
        }
        return this;
    }

    private void spreadMines()
    {
        Random r = new Random();
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (((int)r.Next) % 7 == 0)
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
            if ((this.gui.buttons[x][y].getLabel() != "0") && (this.explosiveNeighbors(x, y) == 0))
            {
                this.gui.buttons[x][y].setLabel("0");
                //System.out.println("x: " + (x) + " y: " + (y) + " 0 explosive neighbors");
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((x + i >= 0) && (x + i < this.xsize) && (y + j >= 0) && (y + j < this.ysize))
                        {
                            if ((this.explosiveNeighbors(x + i, y + j) == 0) && (this.gui.buttons[x + i][y + j].getLabel() != "0"))
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

    public void expandZero(int x, int y)
    {
        if ((x >= 0) && (x < this.xsize) && (y >= 0) && (y < this.ysize))
        {
            if ((this.gui.buttons[x][y].getLabel() != "0") && (this.explosiveNeighbors(x, y) == 0))
            {
                this.gui.buttons[x][y].setLabel("0");
                //System.out.println("x: " + (x) + " y: " + (y) + " 0 explosive neighbors");
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((x + i >= 0) && (x + i < this.xsize) && (y + j >= 0) && (y + j < this.ysize))
                        {
                            if ((this.explosiveNeighbors(x + i, y + j) == 0) && (this.gui.buttons[x + i][y + j].getLabel() != "0"))
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
                    this.gui.buttons[x + i][y + j].setLabel(String.valueOf(this.explosiveNeighbors(x + i, y + j)));
                }
            }
        }

    }
    public static void main(String[] args)
    {
        runGame();
    }

    private static void runGame()
    {
        // todo: port this to c# and gtk .....
        MineField field = new MineField();
        field = field.construct(20, 20);
        field.spreadMines();
        field.gui = new MineGui(field);
        for (int x = 0; x < field.xsize; x++)
        {
            for (int y = 0; y < field.ysize; y++)
            {
                field.gui.buttons[x][y].addActionListener(new ActionListener()
                {

                    public void actionPerformed(ActionEvent arg0)
                {
                    final MatrixButton b = (MatrixButton)arg0.getSource();
                    if (b.mf.isExplosive(b.getXPos(), b.getYPos()))
                    {
                        b.setLabel("X");

                        JDialog dialog = new JDialog(b.mf.gui, "Loser!", true);
                        JPanel panel = new JPanel();
                        panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));

                        panel.add(new JLabel("Das war wohl nix. Du bist tot!"));
                        JButton button = new JButton("nochmal");
                        button.addActionListener(new ActionListener()
                        {

                                public void actionPerformed(ActionEvent e)
                        {
                            runGame();
                            b.mf.gui.dispose();
                        }
                    });
        panel.add(button);
        dialog.add(panel);
        dialog.setSize(300, 100);
        dialog.setVisible(true);
    }
						else {
							if(b.mf.explosiveNeighbors(b.getXPos(),b.getYPos())==0) {
								b.mf.expandZero(b.getXPos(),b.getYPos());
							}

                            else
{
    b.setLabel(String.valueOf(b.mf.explosiveNeighbors(b.getXPos(), b.getYPos())));
}
						}
					}
				});
			}
		}
		field.gui.setVisible(true);
	}

}


}