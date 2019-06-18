using System;

namespace MSD.Library.Core
{
    /// <summary>
    /// Абстрактное описание игрового движка. Реализуется на сервере собственно движком, а на клиенте контроллером, который выполняет роль буфера между интерфейсом и серверной частью
    /// </summary>
    public interface IMustStayDead
    {
        /// <summary>
        /// Событие одновременного восстания нескольких новых мертвецов
        /// </summary>
        event EventHandler<RiseNewEventArgs> RiseNewEvent;
        /// <summary>
        /// Событие прогресса уже поднявшегося мертвеца
        /// </summary>
        event EventHandler<RiseProgressEventArgs> RiseProgressEvent;
        /// <summary>
        /// Событие конца игры по причине проигрыша (закончились очки здоровья)
        /// </summary>
        event EventHandler<LoseEventArgs> LoseEvent;
        /// <summary>
        /// Событие изменения количества очков здоровья
        /// </summary>
        event EventHandler<ChangeHpEventArgs> ChangeHpEvent;

        /// <summary>
        /// Начало игры или возобновление после паузы
        /// </summary>
        void Start();

        /// <summary>
        /// Завершение игры нажатием кнопки "стоп"
        /// </summary>
        void Stop();

        /// <summary>
        /// Пауза игры
        /// </summary>
        void Pause();

        /// <summary>
        /// Уменьшение прогресса восставшего мертвеца на 1 единицу
        /// </summary>
        /// <param name="number">Номер могилы</param>
        void Push(int number);
    }
}
