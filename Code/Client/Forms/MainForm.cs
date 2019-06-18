using System;
using System.Windows.Forms;

using MSD.Library.TCP;
using MSD.Library.Core;

namespace MSD.Client.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Если что-то случится с соединением при отправке или получении данных, показать диалоговое окно с оповещением и закрыть окно с игрой
            gameFieldControl.GameAdapter.Sender.TCPExceptionOccured += ShowException;
            gameFieldControl.GameAdapter.Receiver.TCPExceptionOccured += ShowException;

            // При окончании игры демонстрируем окошко с результатом
            gameFieldControl.GameAdapter.Game.LoseEvent += onStop;
        }

        public void ShowException(object sender, ExceptionArgs e)
        {
            MessageBox.Show(e.Text, e.Title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            onClose(this, new FormClosedEventArgs(CloseReason.None));
        }

        private void onNew(object sender, EventArgs e)
        {
            gameFieldControl.GameAdapter.Game.Start();
        }

        private void onSave(object sender, EventArgs e)
        {

        }

        private void onLoad(object sender, EventArgs e)
        {

        }
        private void onStatistics(object sender, EventArgs e)
        {

        }
        private void onSettings(object sender, EventArgs e)
        {

        }

        private void onAbout(object sender, EventArgs e)
        {
            new AboutBoxForm().ShowDialog();
        }



        private void onPause(object sender, EventArgs e)
        {
            gameFieldControl.GameAdapter.Game.Pause();
        }

        private void onStart(object sender, EventArgs e)
        {
            gameFieldControl.GameAdapter.Game.Start();
        }

        private void onStop(object sender, EventArgs e)
        {
            gameFieldControl.GameAdapter.Game.Stop();

            // Если игра закончилась по запросу сервера
            if (e is LoseEventArgs)
            {
                if (e != null)
                    MessageBox.Show("Your result: " + (e as LoseEventArgs).Result, "Game is over",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Accepting result is impossible", "Game is over",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void onClose(object sender, FormClosedEventArgs e)
        {
            gameFieldControl.GameAdapter.Stop();
            Application.Exit();
        }
    }
}
