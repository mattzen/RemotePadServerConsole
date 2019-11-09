using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
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
            StartListener();
            Console.WriteLine("Hello World!");
        }
    }
}
