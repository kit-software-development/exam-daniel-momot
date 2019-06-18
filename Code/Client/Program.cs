using System;
using System.Windows.Forms;

using MSD.Library.TCP;
using MSD.Client.Forms;

namespace MSD.Client
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }



    }
}
