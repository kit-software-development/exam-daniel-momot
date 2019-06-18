using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Library.Core.Commands
{
    /// <summary>
    /// Команда "ручного" окончания игры
    /// </summary>
    public class StopCommand : AbstractCommand
    {
        public StopCommand()
        {
            Type = "Stop";
            Arguments = "";
        }
    }
    /// <summary>
    /// Команда начала игры
    /// </summary>
    public class StartCommand : AbstractCommand
    {
        public StartCommand()
        {
            Type = "Start";
            Arguments = "";
        }
    }
    /// <summary>
    /// Команда паузы
    /// </summary>
    public class PauseCommand : AbstractCommand
    {
        public PauseCommand()
        {
            Type = "Pause";
            Arguments = "";
        }
    }
}
