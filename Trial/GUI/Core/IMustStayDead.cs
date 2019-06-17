using System;
using System.Collections.Generic;

namespace MSD.Client.Core
{
    // стадии пробуждения
    internal enum Stage
    {
        Sleeps = 0,
        Wakes = 1,
        Rises1 = 2,
        Rises2 = 3,
        Rises3 = 4,
        GetsUp = 5,
        GoesOut = 6
    }
    internal class RiseEventArgs : EventArgs
    {
        public List<int> Places { get; }
        public Stage Stage_new { get; }

        public RiseEventArgs(int place, Stage stage_new)
        {
            Places = new List<int>();
            Places.Add(place);
            Stage_new = stage_new;
        }
        public RiseEventArgs(List<int> places)
        {
            Places = places;
            Stage_new = Stage.Wakes;
        }
    }
    internal class ChangeHpEventArgs : EventArgs
    {
        public int Hp_new { get; }

        public ChangeHpEventArgs(int hp_new)
        {
            Hp_new = hp_new;
        }
    }


    internal interface IMustStayDead
    {
        event EventHandler<RiseEventArgs> RiseEvent; // событие подъема мертвеца
        event EventHandler LoseEvent; // событие проигрыша - закончились очки здоровья
        event EventHandler<ChangeHpEventArgs> ChangeHpEvent; // событие уменьшения жизней

        // начало игры
        void Start();

        // конец игры
        int Stop();

        // пауза
        void Pause();

        // попытка приструнить зомби
        void Push(int number);
    }
}
