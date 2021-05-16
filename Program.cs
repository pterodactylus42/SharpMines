using System;
using Gtk;

namespace SharpMines
{
    public class MainClass
    {

        public static void Main(string[] args)
        {
            Application.Init();
            Window win = new Window();
            win.BorderWidth = 20;
            Table table = new Table((uint)SharpMines.Window.getXSIZE(), (uint)SharpMines.Window.getYSIZE(), true);
            for(int x = 0; x < Window.getXSIZE(); x++)
            {
                for(int y = 0; y < Window.getYSIZE(); y++)
                {
                    table.Attach(win.GetButton(x, y), (uint)x, (uint)x + 1, (uint)y, (uint)y + 1);
                    win.GetButton(x, y).Show();
                }
            }
            win.Add(table);
            table.Show();
            win.Show();
            Application.Run();
        }

    }
}
