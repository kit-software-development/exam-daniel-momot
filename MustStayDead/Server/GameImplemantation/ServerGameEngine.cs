using System;
using System.Collections.Generic;
using System.Threading;

using MSD.Library.Core;

namespace MSD.Server.GameImplemantation
{
    /// <summary>
    /// Игровой движок
    /// </summary>
    internal class ServerGameEngine : IMustStayDead
    {
        private Random random = new Random();
        private Timer timer;

        /// <summary>
        /// Состояние кладбища (для каждой могилы - на какой стадии пробуждения находится мертвец)
        /// </summary>
        private Stage[] graveyard;
        /// <summary>
        /// Текущее количество очков здоровья
        /// </summary>
        private int hp;
        /// <summary>
        /// Текущее количество убитых зомби
        /// </summary>
        private int result;

        /// <summary>
        /// Событие одновременного подъема нескольких новых мертвецов
        /// </summary>
        public event EventHandler<RiseNewEventArgs> RiseNewEvent;
        /// <summary>
        /// Событие прогресса поднимающегося мертвеца
        /// </summary>
        public event EventHandler<RiseProgressEventArgs> RiseProgressEvent;
        /// <summary>
        /// Событие конца игры, содержит результат
        /// </summary>
        public event EventHandler<LoseEventArgs> LoseEvent;
        /// <summary>
        /// Событие изменения количества жизней
        /// </summary>
        public event EventHandler<ChangeHpEventArgs> ChangeHpEvent;

        /// <summary>
        /// Конструктор игрового движка
        /// </summary>
        public ServerGameEngine()
        {
            graveyard = new Stage[GameDefaults.GraveyardSize];
            for (int i = 0; i < graveyard.Length; i++)
                graveyard[i] = Stage.Sleeps;
            hp = GameDefaults.MaxHp;
            result = 0;
        }

        /// <summary>
        /// Старт игры
        /// </summary>
        public void Start()
        {
            timer = new Timer(Tick, null, 0, GameDefaults.Сooldown);
        }

        /// <summary>
        /// Окончание игры
        /// </summary>
        public void Stop()
        {
            timer.Change(Timeout.Infinite, 0);
            LoseEvent?.Invoke(this, new LoseEventArgs(result));
        }

        /// <summary>
        /// Пауза игры
        /// </summary>
        public void Pause()
        {
            timer.Change(Timeout.Infinite, 0);
        }

        /// <summary>
        /// Уменьшение прогресса поднимающегося мертвеца на 1 единицу
        /// </summary>
        /// <param name="number">Номер могилы</param>
        public void Push(int number)
        {
            if(graveyard[number] != Stage.Sleeps)
            {
                graveyard[number]--;
                RiseProgressEvent?.Invoke(this, new RiseProgressEventArgs(number, graveyard[number]));
                if (graveyard[number] == Stage.Sleeps)
                    result++;
            }
        }

        /// <summary>
        /// Очередная итерация игры
        /// </summary>
        /// <param name="obj"></param>
        private void Tick(object obj)
        {
            for (int number = 0; number < graveyard.Length; number++)
                if (graveyard[number] != Stage.Sleeps)
                    DoRiseProgress(number);
            RiseNew();
        }

        /// <summary>
        /// Подъем мертвецов в случайно выбранных клетках (где их раньше не было)
        /// </summary>
        private void RiseNew()
        {
            int alive_count = 0;
            foreach (Stage s in graveyard)
                if (s != Stage.Sleeps)
                    alive_count++;

            List<int> graves_nums = new List<int>();
            while (alive_count < GameDefaults.Population)
            {
                int r;
                do r = random.Next(graveyard.Length);
                while (graveyard[r] != Stage.Sleeps);

                graves_nums.Add(r);
                graveyard[r]++;
                alive_count++;
            }
            RiseNewEvent?.Invoke(this, new RiseNewEventArgs(graves_nums));
        }

        /// <summary>
        /// Подъем мертвеца на 1 стадию вперед в данной могиле
        /// </summary>
        /// <param name="number">Номер могилы</param>
        private void DoRiseProgress(int number)
        {
            if (number >= graveyard.Length || number < 0)
                throw new ArgumentException();

            if (graveyard[number] < GameDefaults.MaxStage)
                graveyard[number]++;
            else
            {
                graveyard[number] = 0;
                ChangeHp(hp - 1);
            }
            RiseProgressEvent?.Invoke(this, new RiseProgressEventArgs(number, graveyard[number]));
        }

        /// <summary>
        /// Уменьшение количества очков здоровья и проверка на прогирыш
        /// </summary>
        /// <param name="hp_new">Обновленное количество очков здоровья</param>
        private void ChangeHp(int hp_new)
        {
            hp = hp_new;
            ChangeHpEvent?.Invoke(this, new ChangeHpEventArgs(hp));
            if (hp == 0)
            {
                Stop();
            }
        }

    }
}
