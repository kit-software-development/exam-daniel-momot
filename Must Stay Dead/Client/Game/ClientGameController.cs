using System;
using System.Collections.Generic;

using MSD.Library.Core;
using MSD.Library.Core.Commands;

namespace MSD.Client.Game
{
   internal class ClientGameController : IMustStayDead
    {
        /// <summary>
        /// Событие одновременного восстания нескольких новых мертвецов
        /// </summary>
        public event EventHandler<RiseNewEventArgs> RiseNewEvent;
        /// <summary>
        /// Событие прогресса уже поднявшегося мертвеца
        /// </summary>
        public event EventHandler<RiseProgressEventArgs> RiseProgressEvent;
        /// <summary>
        /// Событие конца игры по причине проигрыша (закончились очки здоровья)
        /// </summary>
        public event EventHandler<LoseEventArgs> LoseEvent;
        /// <summary>
        /// Событие изменения количества очков здоровья
        /// </summary>
        public event EventHandler<ChangeHpEventArgs> ChangeHpEvent;

        /// <summary>
        /// Событие вызывается для отправки собщения игре на сервер
        /// </summary>
        public event EventHandler<GameEventArgs> GameEvent;

        /// <summary>
        /// Результат игры. Когда приходит сообщение от сервера о конце игры, принимает неотрицательное значение
        /// </summary>
        public int Result { get; } = -1;

        /// <summary>
        /// Пауза игры
        /// </summary>
        public void Pause()
        {
            GameEvent?.Invoke(this, new GameEventArgs(new PauseCommand()));
        }

        /// <summary>
        /// Уменьшение прогресса восставшего мертвеца на 1 единицу
        /// </summary>
        /// <param name="number">Номер могилы</param>
        public void Push(int number)
        {
            GameEvent?.Invoke(this, new GameEventArgs(new PushCommand(number)));
        }

        /// <summary>
        /// Начало игры или возобновление после паузы
        /// </summary>
        public void Start()
        {
            GameEvent?.Invoke(this, new GameEventArgs(new StartCommand()));
        }

        /// <summary>
        /// Завершение игры нажатием кнопки "стоп"
        /// </summary>
        public void Stop()
        {
            GameEvent?.Invoke(this, new GameEventArgs(new StopCommand()));
        }
        /// <summary>
        /// Вызывается при получении с сервера сообщения о появления новых мертвецов
        /// </summary>
        /// <param name="places">Список мест, где появляются мертвецы</param>
        public void RiseNew(List<int> places)
        {
            RiseNewEvent?.Invoke(this, new RiseNewEventArgs(places));
        }
        /// <summary>
        /// Вызывается при получении с сервера сообщения о прогрессе мертвеца до определенной стадии
        /// </summary>
        /// <param name="place">Номер могилы мертвеца</param>
        /// <param name="stage">Новая стадия</param>
        public void RiseProgress(int place, Stage stage)
        {
            RiseProgressEvent?.Invoke(this, new RiseProgressEventArgs(place, stage));
        }
        /// <summary>
        /// Вызывается при получении с сервера сообщения о проигрыше (закончились очки здоровья)
        /// </summary>
        public void Lose(int result)
        {
            LoseEvent?.Invoke(this, new LoseEventArgs(result));
        }
        /// <summary>
        /// Вызывается при получении с сервера сообщения об изменении количества очков здоровья
        /// </summary>
        /// <param name="hp_new">Новое количество очков здоровья</param>
        public void ChangeHp(int hp_new)
        {
            ChangeHpEvent?.Invoke(this, new ChangeHpEventArgs(hp_new));
        }

    }
}
