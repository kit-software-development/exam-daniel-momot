using System.IO;
using System.Net.Sockets;

namespace MSD.Library.TCP
{
    /// <summary>
    /// Данный класс предоставляет функционал для классов TCPSender и TCPReceiver
    /// </summary>
    internal static class TCPExtensions
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

        public static string ReceiveTextMessage(this TcpClient client)
        {
            client.Client.ReceiveTimeout = 100;
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

}
