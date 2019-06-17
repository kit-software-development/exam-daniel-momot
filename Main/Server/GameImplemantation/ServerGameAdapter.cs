using System;
using System.Threading;

using MSD.Library.TCP;
using MSD.Library.Core.Commands;
using MSD.Library.Core;

namespace MSD.Server.GameImplemantation
{
    /// <summary>
    /// Отвечает за взаимодействие игрового движка с клиентом
    /// </summary>
    internal class ServerGameAdapter
    {
        /// <summary>
        /// Отправляет команды клиенту
        /// </summary>
        public TCPSender Sender { get; }
        /// <summary>
        /// Принимает команды от клиента
        /// </summary>
        public TCPReceiver Receiver { get; }
        /// <summary>
        /// Игровой движок
        /// </summary>
        private ServerGameEngine Game;
        private Thread listener;
        /// <summary>
        /// Когда true, не слушаем сообщения от клиента
        /// </summary>
        private bool listener_stop = false;
        /// <summary>
        /// Событие вызывается для отправки собщения клиенту
        /// </summary>
        private event Library.TCP.EventHandler<GameEventArgs> GameEvent;

        /// <summary>
        /// Отвечает за взаимодействие по TCP игрового движка с клиентом
        /// </summary>
        public ServerGameAdapter()
        {
            Game = new ServerGameEngine();
            Sender = new TCPSender(8020);
            Receiver = new TCPReceiver(8010);

            // На случай появления команд от клиента, запускаем поток, слушающий TCP-сообщения
            listener = new Thread(Listen);
            listener.Start();
            listener.IsBackground = false;

            // При появлении внутриигровых событий, влияющих на интерфейс, генерируется единое событие GameEvent...
            Game.ChangeHpEvent += (object sender, ChangeHpEventArgs args) =>
                GameEvent?.Invoke(this, new GameEventArgs(new ChangeHpCommand(args.Hp_new)));
            Game.LoseEvent += (object sender, LoseEventArgs args) =>
                GameEvent?.Invoke(this, new GameEventArgs(new LoseCommand(args.Result)));
            Game.RiseNewEvent += (object sender, RiseNewEventArgs args) =>
                GameEvent?.Invoke(this, new GameEventArgs(new RiseNewCommand(args.Places)));
            Game.RiseProgressEvent += (object sender, RiseProgressEventArgs args) =>
                GameEvent?.Invoke(this, new GameEventArgs(new RiseProgressCommand(args.Place, args.Stage_new)));

            // ... которое затем отправляется клиенту
            GameEvent += Send;
        }

        private void Listen()
        {
            while (!listener_stop)
            {
                string message = Receiver.GetMessage();
                if (message != "")
                {
                    AbstractCommand command = CommandDecoder.Decode(message);
                    if (command != null)
                    {
                        if (command is StartCommand) Game.Start();
                        else if (command is PauseCommand) Game.Pause();
                        else if (command is StopCommand) Game.Stop();
                        else if (command is PushCommand)
                            (Game as ServerGameEngine).Push((command as PushCommand).Number);
                        // Других случаев быть не может, т.к. нераспознанная команда равна null
                    }
                    Console.WriteLine(message); // в целях отладки
                }
            }
        }

        private void Send(object sender, GameEventArgs args)
        {
            Console.WriteLine("Sending message: " + '|' + args.Command.Type + ':' + args.Command.Arguments + '|' + " ...");
            Sender.SendMessage('|' + args.Command.Type + ':' + args.Command.Arguments + '|');
            Console.WriteLine(" ...successfully sent");
        }

    }
}
