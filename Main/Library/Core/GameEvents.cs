using System;
using System.Collections.Generic;

namespace MSD.Library.Core
{
    /// <summary>
    /// Аргументы события одновременного подъема нескольких новых мертвецов
    /// </summary>
    public class RiseNewEventArgs : EventArgs
    {
        public List<int> Places { get; }
        public RiseNewEventArgs(List<int> places)
        {
            Places = places;
        }
    }
    /// <summary>
    /// Аргументы события прогресса поднимающегося мертвеца
    /// </summary>
    public class RiseProgressEventArgs : EventArgs
    {
        public int Place { get; }
        public Stage Stage_new { get; }

        public RiseProgressEventArgs(int place, Stage stage_new)
        {
            Place = place;
            Stage_new = stage_new;
        }
    }
    /// <summary>
    /// Аргументы события изменения очков здоровья
    /// </summary>
    public class ChangeHpEventArgs : EventArgs
    {
        public int Hp_new { get; }

        public ChangeHpEventArgs(int hp_new)
        {
            Hp_new = hp_new;
        }
    }
    /// <summary>
    /// Аргументы события конца игры
    /// </summary>
    public class LoseEventArgs : EventArgs
    {
        /// <summary>
        /// Результат - количество очков
        /// </summary>
        public int Result { get; }

        public LoseEventArgs(int result)
        {
            Result = result;
        }
    }

}
