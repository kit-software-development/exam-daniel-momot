
using System;

namespace MSD.Library.Core.Commands
{
    /// <summary>
    /// Аргументы события, вызываемого при необходимости отправить сообщение игры
    /// </summary>
    public class GameEventArgs : EventArgs
    {
        /// <summary>
        /// Соответствует событию игры
        /// </summary>
        public AbstractCommand Command { get; }

        public GameEventArgs(AbstractCommand command)
        {
            Command = command;
        }
    }

}
