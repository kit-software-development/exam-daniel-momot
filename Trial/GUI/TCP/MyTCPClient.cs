using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace MSD.Client.TCP
{
    // в данный модуль вынесено все взаимодействие клиента по сети TCP


    internal static class Extensions
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
        internal event EventHandler<ExceptionArgs> TCPExceptionOccured;
        private TcpClient client;

        public MyTCPClient()
        {
            // создаем клиент
            client = new TcpClient();
        }

        public void Connect()
        {
            // пытаемся соединиться с сервером
            try
            {
                if (!client.Connected)
                    client.Connect(IPAddress.Loopback, 8100);
            }
            catch (SocketException e)
            {
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs(e.Message, "Ошибка подключения к серверу"));
            }
        }

        // отправка сообщения
        public void SendMessage(string message)
        {
            try
            {
                if (!client.Connected)
                    client.Connect(IPAddress.Loopback, 8100);

                client.Send(message);
            }
            catch (SocketException e)
            {
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs(e.Message, "Ошибка отправки данных серверу"));

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
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs(e.Message, "Ошибка отправки данных серверу"));
            }
        }

    }
}
