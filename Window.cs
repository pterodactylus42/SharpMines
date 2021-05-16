using System;
using Gtk;

namespace SharpMines
{
    public partial class Window : Gtk.Window
    {
        private static int XSIZE = 20;
        private static int YSIZE = 20;
        private MineField mineField;
        public MatrixButton[][] buttons;

        public Window() :
        base(Gtk.WindowType.Toplevel)
        {
            this.mineField = new MineField(XSIZE, YSIZE, this);
            this.initButtons();
            this.DeleteEvent += delete_event;
            this.Build();
        }

        static void callback(object obj, EventArgs args)
        {
            MatrixButton b = (MatrixButton)obj;
            Console.WriteLine("x - {0} y - {1} was pressed", b.getXPosition().ToString(), b.getYPosition().ToString());
            if (b.mf.isExplosive(b.getXPosition(), b.getYPosition()))
            {
                b.Label = "X";
                Console.WriteLine("you lost ;-(");
                Gdk.Color col = new Gdk.Color();
                Gdk.Color.Parse("red", ref col);
                b.ModifyBg(StateType.Normal, col);
                MessageDialog loserDialog = new MessageDialog(b.mf.GetWindow(),
                                                                DialogFlags.DestroyWithParent,
                                                                MessageType.Question,
                                                                ButtonsType.YesNo, "Replay?");
                ResponseType result = (ResponseType)loserDialog.Run();
                if(result == ResponseType.Yes)
                {
                    Console.Write("Yes");
                    b.mf.reset();
                    b.mf.GetWindow().resetButtons();
                    loserDialog.Destroy();
                }
                else
                {
                    Console.Write("No");
                    loserDialog.Destroy();
                }
            }
            else
            {
                if (b.mf.explosiveNeighbors(b.getXPosition(), b.getYPosition()) == 0)
                {
                    b.mf.expandZero(b.getXPosition(), b.getYPosition());
                }
                else
                {
                    b.Label = b.mf.explosiveNeighbors(b.getXPosition(), b.getYPosition()).ToString();
                }
            }
        }

        /* another event */
        static void delete_event(object obj, DeleteEventArgs args)
        {
            Application.Quit();
        }

        static void exit_event(object obj, EventArgs args)
        {
            Application.Quit();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        static public int getXSIZE()
        {
            return XSIZE;
        }

        static public int getYSIZE()
        {
            return YSIZE;
        }

        private void initButtons()
        {
            // jagged array stuff
            this.buttons = new MatrixButton[XSIZE][];
            for (int i = 0; i < XSIZE; i++)
            {
                this.buttons[i] = new MatrixButton[YSIZE];
            }
            Gdk.Color col = new Gdk.Color();
            Gdk.Color.Parse("white", ref col);
            for (int x = 0; x < XSIZE; x++)
            {
                for (int y = 0; y < YSIZE; y++)
                {
                    this.buttons[x][y] = new MatrixButton("?", x, y, this.mineField);
                    this.buttons[x][y].Clicked += callback;
                    this.buttons[x][y].ModifyBg(StateType.Normal, col);
                }
            }
        }

        private void resetButtons()
        {
            Gdk.Color col = new Gdk.Color();
            Gdk.Color.Parse("white", ref col);
            for (int x = 0; x < XSIZE; x++)
            {
                for (int y = 0; y < YSIZE; y++)
                {
                    this.buttons[x][y].Label = "?";
                    this.buttons[x][y].ModifyBg(StateType.Normal, col);
                }
            }
        }

        public MatrixButton GetButton(int x, int y)
        {
            if (x < XSIZE && y < YSIZE) { return this.buttons[x][y]; }
            else { return null; }
        }




    }
}