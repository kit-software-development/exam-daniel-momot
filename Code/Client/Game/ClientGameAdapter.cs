using MSD.Library.Core;
using MSD.Library.Core.Commands;
using MSD.Library.TCP;

using System.Threading;

namespace MSD.Client.Game
{
    /// <summary>
    /// Инкапсулирует GameController. Расшифровывает сообщения с TCP-сервера и передает их игре
    /// </summary>
    internal class ClientGameAdapter
    {
        /// <summary>
        /// На клиенте реализуется классом ClientGameController
        /// </summary>
        public IMustStayDead Game { get; }
        /// <summary>
        /// Отправляет TCP-пакеты
        /// </summary>
        public TCPSender Sender { get; }
        /// <summary>
        /// Принимает TCP-пакеты
        /// </summary>
        public TCPReceiver Receiver { get; }
        /// <summary>
        /// Когда true, не слушаем сообщения от сервера
        /// </summary>
        private bool listener_stop = false;
        Thread listener;

        /// <summary>
        /// Инкапсулирует GameController. Расшифровывает сообщения с TCP-сервера и передает их игре
        /// </summary>
        public ClientGameAdapter()
        {
            Game = new ClientGameController();
            Sender = new TCPSender(8010);
            Receiver = new TCPReceiver(8020);

            // При выполнении пользовательских действий в игре отправляется TCP-сообщение серверу
            (Game as ClientGameController).GameEvent += Send;

            // На случай появления команд от клиента, запускаем поток, слушающий TCP-сообщения
            listener = new Thread(Listen);
            listener.Start();
            listener.IsBackground = false;
        }

        public void Stop()
        {
            listener_stop = true;
            listener.IsBackground = true;
        }


        private void Send(object sender, GameEventArgs args)
        {
            Sender.SendMessage('|' + args.Command.Type + ':' + args.Command.Arguments + '|');
        }

        /// <summary>
        /// Получает очередное сообщение от сервера и, если идентифицирует команду, обращается к GameController, который инициирует соответствующее событие
        /// </summary>
        private void Listen()
        {
            while (!listener_stop)
            {
                string message = Receiver.GetMessage();
                if (message != null && message != "")
                {
                    AbstractCommand command = CommandDecoder.Decode(message);
                    if (command != null)
                    {
                        if (command is StartCommand) Game.Start();
                        else if (command is PauseCommand) Game.Pause();
                        else if (command is StopCommand) Game.Stop();
                        else if (command is RiseNewCommand)
                            (Game as ClientGameController).RiseNew((command as RiseNewCommand).Places);
                        else if (command is PushCommand)
                            (Game as ClientGameController).Push((command as PushCommand).Number);
                        else if (command is LoseCommand)
                        {
                            (Game as ClientGameController).Lose((command as LoseCommand).Result);
                        }
                        else if (command is ChangeHpCommand)
                        {
                            (Game as ClientGameController).ChangeHp((command as ChangeHpCommand).Hp);
                        }
                        else if (command is RiseProgressCommand)
                        {
                            (Game as ClientGameController).RiseProgress(
                                (command as RiseProgressCommand).Place,
                                (command as RiseProgressCommand).Stage);
                        }
                    }
                }
            }
        }

    }
}
