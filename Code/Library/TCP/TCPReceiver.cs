using System;
using System.Net;
using System.Net.Sockets;

namespace MSD.Library.TCP
{
    /// <summary>
    /// Позволяет получать TCP-пакеты
    /// </summary>
    public class TCPReceiver
    {
        public event EventHandler<ExceptionArgs> TCPExceptionOccured;
        private TcpListener listener;
        private readonly IPEndPoint endpoint;

        /// <summary>
        /// От клиента серверу указывается порт 8010, от сервера клиенту 8020
        /// </summary>
        /// <param name="port"></param>
        public TCPReceiver(int port)
        {
            endpoint = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(endpoint);
        }

        /// <summary>
        /// Получения сообщения по TCP
        /// </summary>
        /// <returns>Текст сообщения</returns>
        public string GetMessage()
        {
            string message = "";
            try
            {
                listener.Start();
                TcpClient client;
                do client = listener.AcceptTcpClient();
                while (!client.Connected);
                // принимаем входящее сообщение
                // блокирует текущий поток исполнения до получения сообщения. может быть остановлен только извне
                if (client != null)
                {
                    message = Receive(client);
                    client.Close();
                }
                listener.Stop();
            }
            catch (Exception)
            {
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs("Socket error at port " + endpoint.Port, "Accepting message error"));
            }
            return message;
        }


        private string Receive(TcpClient client)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    return client.ReceiveTextMessage();
                }
                catch (Exception) { }
            }
            return null;
        }

    }
}