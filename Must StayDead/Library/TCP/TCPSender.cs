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
                client = new TcpClient();
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        client.Connect(endpoint);
                        break;
                    }
                    catch (Exception) { }
                }
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        client.Send(message);
                        break;
                    }
                    catch (Exception) { }
                }
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        client.Client.Disconnect(false);
                        client.Dispose();
                        break;
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception e)
            {
                TCPExceptionOccured?.Invoke(this, new ExceptionArgs(e.Message, "Ошибка передачи данных"));
            }
        }

    }

}
