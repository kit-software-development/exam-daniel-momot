using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace ConsoleClient
{
    // в данный модуль вынесено все взаимодействие клиента по сети TCP

    public static class Extensions
    {
        public static void Send(this TcpClient client, string message)
        {
            client.Client.Send(message.toByteArray());
        }
        public static byte[] toByteArray(this string text)
        {
            using (MemoryStream memory = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(memory))
            {
                writer.WriteLine(text);
                writer.Flush();
                return memory.ToArray();
            }
        }
    }


    internal class MyTCPClient : IDisposable
    {
        private TcpClient client;

        public MyTCPClient()
        {
            // создаем клиент
            client = new TcpClient();

            // пытаемся соединиться с сервером
            try
            {
                client.Connect(IPAddress.Loopback, 8100);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

            // отправка сообщения
        public void SendMessage(string message)
        {
            try
            {
                client.Connect(IPAddress.Loopback, 8100);
                client.Send(message);
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
