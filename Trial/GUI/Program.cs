using System;
using System.Windows.Forms;

using MSD.Client.TCP;
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
            /*
            using (MyTCPClient module = new MyTCPClient())
            {
                module.TCPExceptionOccured += ShowException;
                

                module.Connect();
                module.SendMessage("It's alive!");
                module.SendMessage("It's alive! x2");

            }
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }


        public static void ShowException(object sender, ExceptionArgs e)
        {
            MessageBox.Show(e.Text, e.Title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
