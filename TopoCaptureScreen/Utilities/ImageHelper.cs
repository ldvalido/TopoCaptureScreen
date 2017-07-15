using System.Drawing;
using System.Windows.Forms;

namespace TopoCaptureScreen.Utilities
{
    public class ImageHelper
    {
        public static Image GetCurrentScreen()
        {
            Image returnValue;
            Size s = Screen.PrimaryScreen.Bounds.Size;
            Bitmap bmp = new Bitmap(s.Width, s.Height);
            Graphics gr = Graphics.FromImage(bmp);
            gr.CopyFromScreen(0, 0, 0, 0, s);
            returnValue = Image.FromHbitmap((bmp.GetHbitmap()));
            return returnValue;
        }
    }
}
