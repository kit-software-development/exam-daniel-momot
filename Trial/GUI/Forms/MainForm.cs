using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSD.Client.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            gameFieldControl.Game.LoseEvent += onStop;
        }

        private void onExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void onNew(object sender, EventArgs e)
        {
            gameFieldControl.Game.Start();
        }

        private void onSave(object sender, EventArgs e)
        {

        }

        private void onLoad(object sender, EventArgs e)
        {

        }

        private void onAbout(object sender, EventArgs e)
        {
            new AboutBoxForm().ShowDialog();
        }

        private void onSettings(object sender, EventArgs e)
        {

        }

        private void onStatistics(object sender, EventArgs e)
        {

        }

        private void onStop(object sender, EventArgs e)
        {
            int result = gameFieldControl.Game.Stop();

            MessageBox.Show("Ваш результат: " + result, "Игра завершена",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void onPause(object sender, EventArgs e)
        {
            gameFieldControl.Game.Pause();
        }

        private void onStart(object sender, EventArgs e)
        {
            gameFieldControl.Game.Start();
        }
    }
}
