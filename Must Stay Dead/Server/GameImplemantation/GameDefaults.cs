
using MSD.Library.Core;

namespace MSD.Server.GameImplemantation
{
    /// <summary>
    /// Хранит значения, балансирующие игровой процесс
    /// </summary>
    internal static class GameDefaults
    {
        /// <summary>
        /// Максимальная стадия, до которой доходит мертвец, прежде чем он оживет
        /// </summary>
        public const Stage MaxStage = Stage.GoesOut;
        /// <summary>
        /// Максимальное (исходное) количество очков здоровья
        /// </summary>
        public const int MaxHp = 5;
        /// <summary>
        /// Количество одновременно поднимающихся зомби
        /// </summary>
        public const int Population = 1;
        /// <summary>
        /// Период тика (восстание новых зомбей и прогресс вылазящих)
        /// </summary>
        public const int Сooldown = 500;
        /// <summary>
        /// Количество мест на кладбище. Жестко связан с интерфейссом, не изменять!!!
        /// </summary>
        public const int GraveyardSize = 8;
    }
}
