using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace RemotePadServerConsole
{
    class Program
    {
        private const int listenPort = 51315;

        private static void StartListener()
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);

                    Console.WriteLine($"Received broadcast from {groupEP} :");
                    Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }


        static void Main(string[] args)
        {

            //Right Click
            //var l  = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.RightDown);
            //l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.RightUp);

            //Scroll up
            //var l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.LeftDown);
            //l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.LeftUp);
            //l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.Wheel);
            //l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);

            //Start new process
            //Process pr = new Process();
            //pr.StartInfo.FileName = "notepad.exe";
            //pr.Start();
            
            //Keyboard a and capital A
            //var l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.LeftDown);
            //l = Win32.GetCursorPosition();
            //Win32.SetCursorPos(l.X, l.Y);
            //Win32.MouseEvent(Win32.MouseEventFlags.LeftUp);
            //Keyboard k = new Keyboard();
            //k.Send(Keyboard.ScanCodeShort.KEY_A);
            //k.SendWithShift(Keyboard.ScanCodeShort.KEY_A);


            Console.WriteLine("Hello World!");


            

        }


        public static class Win32
        {
            [DllImport("User32.dll")]
            public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName,
                string lpWindowName);

            // Activate an application window.
            [DllImport("USER32.DLL")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);


            [DllImport("User32.Dll")]
            public static extern long SetCursorPos(int x, int y);

            //[DllImport("user32.dll")]
            //public static extern bool GetCursorPos(out POINT lpPoint);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(out MousePoint lpMousePoint);

            public static MousePoint GetCursorPosition()
            {
                MousePoint currentMousePoint;
                var gotPoint = GetCursorPos(out currentMousePoint);
                if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
                return currentMousePoint;
            }

            public static void MouseEvent(MouseEventFlags value)
            {
                MousePoint position = GetCursorPosition();

                mouse_event
                    ((int)value,
                        position.X,
                        position.Y,
                        1000,
                        0);
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MousePoint
            {
                public int X;
                public int Y;

                public MousePoint(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }

            [Flags]
            public enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                Move = 0x00000001,
                Absolute = 0x00008000,
                RightDown = 0x00000008,
                RightUp = 0x00000010,
                Wheel = 0x0800
            }

            //[StructLayout(LayoutKind.Sequential)]
            //public struct POINT
            //{
            //    public int x;
            //    public int y;
            //}
        }


    }
}
