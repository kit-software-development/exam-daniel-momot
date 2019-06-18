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
        private readonly IPEndPoint endpoint;

        /// <summary>
        /// От клиента серверу указывается порт 8010, от сервера клиенту 8020
        /// </summary>
        /// <param name="port"></param>
        public TCPSender(int port)
        {
            client = new TcpClient();
            endpoint = new IPEndPoint(IPAddress.Loopback, port);
        }

        /// <summary>
        /// Отправка сообщения по TCP
        /// </summary>
        /// <param name="message">Текст сообщения. В рамках данного приложения - команда</param>
        public void SendMessage(string message)
        {
            try
            {
                Connect();
                Send(message);
                Disconnect();
            }
            catch(Exception e)
            {
                // Срабатывает, если какое-либо из действий блока try не удалось выполнить после трех попыток
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs("Socket error at port " + endpoint.Port + ": " + e.Message, "Sending message error"));
            }
        }

        private void Connect()
        {
            client = new TcpClient();
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    client.Connect(endpoint);
                    break;
                }
                catch (Exception) { }
            }
            client.Connect(endpoint);
        }
        private void Send(string message)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    client.Send(message);
                    break;
                }
                catch (Exception) { }
            }
            client.Send(message);
        }
        private void Disconnect()
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    client.Client.Disconnect(false);
                    client.Dispose();
                    break;
                }
                catch (Exception) { }
            }
            client.Client.Disconnect(false);
            client.Dispose();
        }



    }

}
