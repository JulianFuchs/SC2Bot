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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void StartButtonClick(object sender, EventArgs e)
        {
            IntPtr SC2Handle = FindWindow("StarCraft II", "StarCraft II");
            SetForegroundWindow(SC2Handle);
            if (SC2Handle == IntPtr.Zero)
            {
                MessageBox.Show("Start SC2 pls and log in");
            }
            else
            {
                Thread t1 = new Thread(new ThreadStart(start));
                t1.Start();
                
            }
        }

        private static int[] RedEye = { 255, 199, 8 };
        private static int[] Minerals = { 126, 250, 251 };
        private static int[] DefaultProfilePic = { 160, 137, 120 };


        private void start()
        {
            IntPtr SC2Handle = FindWindow("StarCraft II", "StarCraft II");
            SetForegroundWindow(SC2Handle);
            wait(3000);
            LeftClick(269, 478); //Matchmaking
            wait(5000);

            Bitmap screen;
            Color colorMenu, colorGame;

            //String s; int i = 1;

            ScreenCapture.ScreenCaptureInit();

                      
            while (true)
            {
                ScreenCapture.CaptureScreen();

                screen = ScreenCapture.BMP;

                //s = i.ToString();
                //i = i + 1;
                //screen.Save("C:/Users/Skaldik/Pictures/sc2/stuff/sc2-" + s + ".bmp");

                colorMenu = screen.GetPixel(69, 1002); //get redEye
                //colorMenu = screen.GetPixel(85, 1023); //get DefaultProfilePicture
                colorGame = screen.GetPixel(1506, 18);

                if ((colorMenu.R == RedEye[0] && colorMenu.G == RedEye[1] && colorMenu.B == RedEye[2]) || 
                    (colorMenu.R == DefaultProfilePic[0] && colorMenu.G == DefaultProfilePic[1] && colorMenu.B == DefaultProfilePic[2]))
                {
                    doMenu();
                }
                else if (colorGame.R == Minerals[0] && colorGame.G == Minerals[1] && colorGame.B == Minerals[2])
                {
                    doGame();
                }

                ScreenCapture.cleanUp();
            }
        }

        private void doMenu()
        {
            LeftClick(1626, 73); //Close Matchfinish window
            wait(3000);

            LeftClick(359, 452); //1v1
            LeftClick(935, 548); //Random
            LeftClick(500, 859); //Play
            
        }

        private void doGame()
        {
            SendKeys.SendWait("{Enter}"); wait(1000);
            SendKeys.SendWait("gg");
            wait(1000);
            SendKeys.SendWait("{Enter}");
            wait(2000);
            SendKeys.SendWait("{F10}");
            wait(2000);
            SendKeys.SendWait("n");
            wait(1000);
            SendKeys.SendWait("s");
            wait(1000);
            wait(10000);    //wait for game to finish

        }

        // Get a handle to an application window.
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        # region Mouse constants
        // constants for the mouse_input() API function
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        # endregion

        public static void RightClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);

            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);

        }

        public static void LeftClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }


        public static void wait(int x)
        {
            System.Threading.Thread.Sleep(x);
        }
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        static public System.Drawing.Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                            (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
    }
}
