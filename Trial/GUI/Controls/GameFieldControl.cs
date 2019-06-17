using System;
using System.ComponentModel;
using System.Windows.Forms;
using MSD.Client.Core;
using System.Linq;

namespace MSD.Client.Controls
{
    internal partial class GameFieldControl : UserControl
    {
        internal IMustStayDead Game { get; }
        public GameFieldControl()
        {
            InitializeComponent();
            Game = new CMustStayDead();

            Game.RiseEvent += OnRise;
            Game.ChangeHpEvent += OnChangeHp;
        }

        private void OnRise(object sender, RiseEventArgs e)
        {
            for (int j = 0; j < e.Places.Count; j++)
            {
                foreach (var control in this.Controls.OfType<Button>())
                {
                    if (control.Name == "button" + (e.Places[j] + 1))
                    {
                        string text_new = "";
                        if (e.Stage_new == Stage.Sleeps)
                            text_new = "Могила " + (e.Places[j] + 1);
                        else
                            text_new = "Стадия " + (int)e.Stage_new;

                        Invoke((MethodInvoker)delegate ()
                        {
                            control.Text = text_new;
                        });

                    }
                }
            }
        }

        private void OnChangeHp(object sender, ChangeHpEventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                hp_label.Text = "❤ " + e.Hp_new;
            });
        }

        private void onPush(object sender, EventArgs e)
        {
            string text = (sender as Button).Name.Substring("button".Length);
            int number = Int32.Parse(text) - 1; // - 1 потому, что в игре индексация начинается с 0 (массив)
            Game.Push(number);
        }

    }
}
