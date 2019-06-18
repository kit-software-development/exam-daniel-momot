using System.Collections.Generic;

namespace MSD.Library.Core.Commands
{

    /// <summary>
    /// Команда подъема новых мертвецов
    /// </summary>
    public class RiseNewCommand : AbstractCommand
    {
        /// <summary>
        /// Номера могил
        /// </summary>
        public List<int> Places { get; }

        /// <summary>
        /// Команда подъема новых мертвецов
        /// </summary>
        public RiseNewCommand(List<int> places)
        {
            Type = "RiseNew";

            Arguments = "";
            foreach(int cur in places)
                Arguments += ":" + cur;
            if (Arguments.Length > 1)
                Arguments = Arguments.Substring(1);

            Places = places;
        }
    }
    /// <summary>
    /// Команда прогресса подъема поднявшегося мертвеца
    /// </summary>
    public class RiseProgressCommand : AbstractCommand
    {
        /// <summary>
        /// Номер могилы
        /// </summary>
        public int Place { get; }
        /// <summary>
        /// (Новая) стадия подъема
        /// </summary>
        public Stage Stage { get; }

        /// <summary>
        /// Команда прогресса подъема поднявшегося мертвеца
        /// </summary>
        public RiseProgressCommand(int place, Stage stage)
        {
            Type = "RiseProgress";
            Arguments = place + ":" + ((int)stage);

            Stage = stage;
            Place = place;
        }
    }
    /// <summary>
    /// Команда уменьшения прогресса подъема мертвеца на 1 единицу
    /// </summary>
    public class PushCommand : AbstractCommand
    {
        /// <summary>
        /// Номер могилы
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Команда уменьшения прогресса подъема мертвеца на 1 единицу
        /// </summary>
        public PushCommand(int number)
        {
            Type = "Push";
            Arguments = number.ToString();

            Number = number;
        }
    }
    /// <summary>
    /// Команда проигрыша
    /// </summary>
    public class LoseCommand : AbstractCommand
    {
        /// <summary>
        /// Результат игры
        /// </summary>
        public int Result { get; }

        /// <summary>
        /// Команда проигрыша
        /// </summary>
        public LoseCommand(int result)
        {
            Type = "Lose";
            Arguments = result.ToString();

            Result = result;
        }
    }
    /// <summary>
    /// Команда изменения количества очков здоровья
    /// </summary>
    public class ChangeHpCommand : AbstractCommand
    {
        /// <summary>
        /// Количество очков (новое)
        /// </summary>
        public int Hp { get; }

        /// <summary>
        /// Команда изменения количества очков здоровья
        /// </summary>
        public ChangeHpCommand(int hp)
        {
            Type = "ChangeHp";
            Arguments = hp.ToString();

            Hp = hp;
        }
    }

}
