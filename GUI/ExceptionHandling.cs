using System.Windows.Forms;
using System;

namespace MSD.Client
{
    internal class ExceptionArgs : EventArgs
    {
        public string Text { get; }
        public string Title { get; }

        public ExceptionArgs(string text, string title)
        {
            Text = text;
            Title = title;
        }
    }
    internal delegate void EventHandler<ExceptionArgs>(object sender, ExceptionArgs args);


    internal static class ExceptionHandling
    {

    }
}
