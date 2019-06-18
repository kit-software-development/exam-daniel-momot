using System;

namespace MSD.Library.TCP
{
    /// <summary>
    /// Аргументы события исключения
    /// </summary>
    public class ExceptionArgs : EventArgs
    {
        /// <summary>
        /// Полный текст исключения
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Тип исключения
        /// </summary>
        public string Title { get; }

        public ExceptionArgs(string text, string title)
        {
            Text = text;
            Title = title;
        }
    }

    /// <summary>
    /// Событие исключения
    /// </summary>
    /// <typeparam name="ExceptionArgs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void EventHandler<ExceptionArgs>(object sender, ExceptionArgs args);

}
