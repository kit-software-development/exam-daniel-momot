
namespace MSD.Library.Core.Commands
{
    /// <summary>
    /// Абстрактная реализация любого события игры, информация о котором передается между клиентом и сервером
    /// </summary>
    public abstract class AbstractCommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Type { protected set; get; }
        /// <summary>
        /// Аргументы команды
        /// </summary>
        public string Arguments {protected set; get;}
    }
}
