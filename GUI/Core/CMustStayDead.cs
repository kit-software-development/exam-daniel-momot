using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;


namespace MSD.Client.Core
{
    internal class CMustStayDead : IMustStayDead
    {
        // максимальная стадия, до которой доходит мертвец, прежде чем он оживет
        private static readonly Stage maxStage = Stage.GoesOut;
        private static readonly int hp_max = 5; // количество очков здоровья
        private static readonly int norm_population = 1; // количество одновременно поднимающихся зомби
        private static readonly int cooldown = 500; // период тика (восстание новых зомбей и прогресс вылазящих)
        private const int graveyard_size = 8;
        private Random random = new Random();
        private Timer timer;

        private Stage[] graveyard;
        private int hp;
        private int result; /// <summary>
        /// количество убитых зомбей
        /// </summary>

        public event EventHandler<RiseEventArgs> RiseEvent;
        public event EventHandler LoseEvent;
        public event EventHandler<ChangeHpEventArgs> ChangeHpEvent;

        public CMustStayDead()
        {
            graveyard = new Stage[graveyard_size];
            for (int i = 0; i < graveyard_size; i++)
                graveyard[i] = Stage.Sleeps;
            hp = hp_max;
            result = 0;
        }

        public void Start()
        {
            timer = new Timer(Tick, null, 0, cooldown);
        }

        public int Stop()
        {
            timer.Change(Timeout.Infinite, 0);
            return result;
        }

        public void Pause()
        {
            timer.Change(Timeout.Infinite, 0);
        }

        public void Push(int number)
        {
            if(graveyard[number] != Stage.Sleeps)
            {
                graveyard[number]--;
                RiseEvent?.Invoke(this, new RiseEventArgs(number, graveyard[number]));
                if (graveyard[number] == Stage.Sleeps)
                    result++;
            }
        }

        // очередная итерация игры
        private void Tick(object obj)
        {
            for (int number = 0; number < graveyard_size; number++)
                if (graveyard[number] != Stage.Sleeps)
                    Rise(number);
            RiseNew();
        }

        // подъем мертвецов в случайно выбранных клетках (где их раньше не было)
        private void RiseNew()
        {
            int alive_count = 0;
            foreach (Stage s in graveyard)
                if (s != Stage.Sleeps)
                    alive_count++;

            List<int> graves_nums = new List<int>();
            while (alive_count < norm_population)
            {
                int r;
                do r = random.Next(graveyard_size);
                while (graveyard[r] != Stage.Sleeps);

                graves_nums.Add(r);
                graveyard[r]++;
                alive_count++;
            }
            RiseEvent?.Invoke(this, new RiseEventArgs(graves_nums));
        }

        // подъем мертвеца на 1 стадию вперед в данной клетке
        private void Rise(int number)
        {
            if (number >= graveyard_size || number < 0)
                throw new ArgumentException();

            if (graveyard[number] < maxStage)
                graveyard[number]++;
            else
            {
                graveyard[number] = 0;
                ChangeHp(hp - 1);
            }
            RiseEvent?.Invoke(this, new RiseEventArgs(number, graveyard[number]));
        }

        // уменьшение очков здоровья и проверка, не закончились ли они
        private void ChangeHp(int hp_new)
        {
            hp = hp_new;
            ChangeHpEvent?.Invoke(this, new ChangeHpEventArgs(hp));
            if (hp == 0)
            {
                LoseEvent?.Invoke(this, EventArgs.Empty);
                Stop();
            }
        }

    }
}
