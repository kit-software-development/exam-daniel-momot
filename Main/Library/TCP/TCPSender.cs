using System;
using System.Net;
using System.Net.Sockets;

namespace MSD.Library.TCP
{
    /// <summary>
    /// Позволяет отправлять TCP-пакеты
    /// </summary>
    public class TCPSender
    {
        public event EventHandler<ExceptionArgs> TCPExceptionOccured;
        private TcpClient client;
        private readonly int Port;

        /// <summary>
        /// От клиента серверу указывается порт 8010, от сервера клиенту 8020
        /// </summary>
        /// <param name="port"></param>
        public TCPSender(int port)
        {
            client = new TcpClient();
            Port = port;
        }

        /// <summary>
        /// Отправка сообщения по TCP
        /// </summary>
        /// <param name="message">Текст сообщения. В рамках данного приложения - команда</param>
        public void SendMessage(string message)
        {
            try
            {
                client = new TcpClient();
                

                while(!client.Client.Connected)
                    client.Connect(IPAddress.Loopback, Port);
                client.Send(message);
                client.Close();
            }
            catch (Exception e)
            {
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs(e.Message, "Ошибка отправки данных"));
            }
        }

    }

}
