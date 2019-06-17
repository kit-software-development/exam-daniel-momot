using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MSD.Server.TCP
{
    static class Extensions
    {
        public static string ReceiveTextMessage(this TcpClient client)
        {
            byte[] data = client.Client.ReceiveAllAvailableData();
            return data.ToText();
        }
        static byte[] ReceiveAllAvailableData(this Socket socket)
        {
            //создаем объект для накопления прочитанных данных 
            using (MemoryStream memory = new MemoryStream())
            {
                //создаем буфер для чтения данных 
                byte[] buffer = new byte[1024];
                //создаем переменную для хранения количества прочитанных данных за одну опереацию 
                int len;
                //читать данные 
                while ((len = socket.Receive(buffer)) > 0)
                {
                    //записываем прочитанные данные в память 
                    memory.Write(buffer, 0, len);
                }
                //инициируем сохранение данных оставшихся в буфере 
                memory.Flush();
                //возвращаем двоичный массив, сформированный в памяти 
                return memory.ToArray();
            }
        }
        static string ToText(this byte[] data)
        {
            using (MemoryStream memory = new MemoryStream(data))
            using (StreamReader reader = new StreamReader(memory))
            {
                return reader.ReadToEnd();
            }
        }

    }

    internal class MyTCPServer
    {
        private TcpListener listener;
        public MyTCPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            try
            {
                listener.Start(); // связываем слушатель с портом
                Console.WriteLine("Server is ready for incoming connections");
            }
            catch (SocketException)
            {
                Console.WriteLine("Another server is currently listening at port " + port);
            }
        }
        public void Listen()
        {
            while (true)
            {
                try
                {
                    listener.Start(); // связываем слушатель с портом
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }


                TcpClient client = listener.AcceptTcpClient();
                // принимаем входящее сообщение
                // блокирует текущий поток исполнения до получения сообщения. может быть остановлен только извне
                if (client != null)
                {
                    string message = client.ReceiveTextMessage();
                    Console.WriteLine("Accepted message: " + message);
                    client.Close();
                }
            }
        }

        public void Stop()
        {
            if (listener.Server.Connected)
                listener.Stop();
        }
    }
}
