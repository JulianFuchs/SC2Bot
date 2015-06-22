using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace SC2Bot
{
    static class ScreenCapture
    {
        private static int PrimaryScreenX, PrimaryScreenY;
        private static Size size;
        private static CopyPixelOperation op;
        private static Graphics GFX;
        public static Bitmap BMP;

        public static void ScreenCaptureInit()
        {
            PrimaryScreenX = Screen.PrimaryScreen.Bounds.X;
            PrimaryScreenY = Screen.PrimaryScreen.Bounds.Y;
            size = Screen.PrimaryScreen.Bounds.Size;
            op = CopyPixelOperation.SourceCopy;
        }

        public static void CaptureScreen()
        {
            BMP = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GFX = Graphics.FromImage(BMP);

            GFX.CopyFromScreen(PrimaryScreenX, PrimaryScreenY, 0, 0, size, op);

        }

        public static void cleanUp()
        {
            GFX.Dispose();
            BMP.Dispose();

        }

    }
}
