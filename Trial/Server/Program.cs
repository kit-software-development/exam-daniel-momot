using System;

using MSD.Server.TCP;


namespace MSD.Server
{


    class Program
    {
        static void Main(string[] args)
        {
            MyTCPServer server = new MyTCPServer(8100);
            server.Listen();

            Console.WriteLine("kek");




        }
    }
}
