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
            //StartListener();
            Win32.POINT p = new Win32.POINT();


            Win32.POINT l;
            Win32.GetCursorPos(out l);

            Win32.SetCursorPos(l.x + 200, l.y +200);

            //Process pr = new Process();
            //pr.StartInfo.FileName = "visual studiocmd";

            //pr.StartInfo.UseShellExecute = false;7
            //pr.StartInfo.CreateNoWindow = false;

            //pr.Start();

            Keyboard k = new Keyboard();


            k.Send(Keyboard.ScanCodeShort.KEY_7);

            

            Console.WriteLine("Hello World!");




        }


        public class Win32
        {

        


            [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName,
                string lpWindowName);

            // Activate an application window.
            [DllImport("USER32.DLL")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);


            [DllImport("User32.Dll")]
            public static extern long SetCursorPos(int x, int y);

            [DllImport("user32.dll")]
            public static extern bool GetCursorPos(out POINT lpPoint); 

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;
            }
        }


    }
}
