using System;

using MSD.Library.TCP;
using MSD.Server.GameImplemantation;

namespace MSD.Server
{


    class Program
    {
        static void Main(string[] args)
        {
            // Запускаем адаптер игрового движка (инкапсулирует сам движок)
            ServerGameAdapter gameController = new ServerGameAdapter();

            // При возникновении ошибок, сообщаем о них в консоли
            gameController.Receiver.TCPExceptionOccured += ShowException;
            gameController.Sender.TCPExceptionOccured += ShowException;
        }

        /// <summary>
        /// Информирование об исключении
        /// </summary>
        private static void ShowException(object sender, ExceptionArgs ex_args)
        {
            Console.WriteLine(ex_args.Text);
        }

    }
}
