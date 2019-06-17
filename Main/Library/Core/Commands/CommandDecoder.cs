using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Library.Core.Commands
{
    /// <summary>
    /// Восстанавливает команды из строкового представления
    /// </summary>
    public static class CommandDecoder
    {
        /// <summary>
        /// Восстановление команды из строкового представления. Если команда не распознана, возвращает null
        /// </summary>
        /// <param name="message">Строковое представление команды вида Stop или Push:5</param>
        /// <returns></returns>
        public static AbstractCommand Decode(string message)
        {
            // message = "|Push:5|/n/r"
            message = message.Substring(1, message.IndexOf('|', 1) - 1);
            // message = "Push:5"
            string type = message.Substring(0, message.IndexOf(':'));

            if (type == new StopCommand().Type)
                return new StopCommand();
            if (type == new StartCommand().Type)
                return new StartCommand();
            if (type == new PauseCommand().Type)
                return new PauseCommand();
            if (type == new RiseNewCommand(new List<int>()).Type)
            {
                List<int> places = new List<int>();
                string rest = message.Substring(message.IndexOf(':') + 1);
                while (rest.Length > 0)
                {
                    string cur;
                    if (rest.IndexOf(':') == -1)
                        cur = rest;
                    else
                        cur = rest.Substring(0, rest.IndexOf(':'));
                    places.Add(Int32.Parse(cur));

                    rest = rest.Substring(cur.Length);
                }
                return new RiseNewCommand(places);
            }
            if (type == new RiseProgressCommand(0, 0).Type)
            {
                string rest = message.Substring(message.IndexOf(':') + 1);

                int border = rest.IndexOf(':');
                int place = Int32.Parse(rest.Substring(0, border));
                int stage = Int32.Parse(rest.Substring(border + 1));

                return new RiseProgressCommand(place, (Stage)stage);
            }
            if (type == new PushCommand(0).Type)
            {
                string rest = message.Substring(message.IndexOf(':') + 1);

                int number = Int32.Parse(rest);

                return new PushCommand(number);
            }
            if (type == new LoseCommand(0).Type)
            {
                string rest = message.Substring(message.IndexOf(':') + 1);

                int result = Int32.Parse(rest);

                return new LoseCommand(result);
            }
            if (type == new ChangeHpCommand(0).Type)
            {
                string rest = message.Substring(message.IndexOf(':') + 1);

                int hp = Int32.Parse(rest);

                return new ChangeHpCommand(hp);
            }
            return null;
        }
    }
}
