using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
    public MainWindow(): base (Gtk.WindowType.Toplevel)
    {
        Build();

        Gdk.Window window = Gdk.Global.DefaultRootWindow;
        if (window!=null)
        {           
            Gdk.Pixbuf pixBuf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8, 
                                               window.Screen.Width, window.Screen.Height);          
            pixBuf.GetFromDrawable(window, Gdk.Colormap.System, 0, 0, 0, 0, 
                                   window.Screen.Width, window.Screen.Height);          
            pixBuf.ScaleSimple(400, 300, Gdk.InterpType.Bilinear);
            pixBuf.Save("screenshot0.jpeg", "jpeg");
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
