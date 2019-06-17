using System;
using System.Windows.Forms;
using System.Linq;

using MSD.Client.Game;
using MSD.Library.Core;

namespace MSD.Client.Controls
{
    internal partial class GameFieldControl : UserControl
    {
        /// <summary>
        /// Отвечает за обмен сообщениями клиентского контроллера игры (который генерит события для интерфейса) с сервером
        /// </summary>
        public ClientGameAdapter GameAdapter;
        public GameFieldControl()
        {
            InitializeComponent();
            GameAdapter = new ClientGameAdapter();

            // Отражаем в интерфейсе события, инициируемые сервером
            GameAdapter.Game.RiseNewEvent += OnRiseNew;
            GameAdapter.Game.RiseProgressEvent += OnRiseProgress;
            GameAdapter.Game.ChangeHpEvent += OnChangeHp;
        }

        /// <summary>
        /// Вызывается при подъеме новых мертвецов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRiseNew(object sender, RiseNewEventArgs e)
        {
            for (int j = 0; j < e.Places.Count; j++)
            {
                foreach (var control in this.Controls.OfType<Button>())
                {
                    if (control.Name == "button" + (e.Places[j] + 1))
                    {
                        string text_new = "";
                        text_new = "Стадия " + (int)Stage.Wakes;

                        // потокобезопасное изменение текста кнопки
                        Invoke((MethodInvoker)delegate ()
                        {
                            control.Text = text_new;
                        });
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Вызывается при прогрессе уже поднявшегося мертвеца
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRiseProgress(object sender, RiseProgressEventArgs e)
        {
                foreach (var control in this.Controls.OfType<Button>())
                {
                    if (control.Name == "button" + (e.Place + 1))
                    {
                        string text_new = "";
                        if (e.Stage_new == Stage.Sleeps)
                            text_new = "Могила " + (e.Place + 1);
                        else
                            text_new = "Стадия " + (int)e.Stage_new;

                    // потокобезопасное изменение текста кнопки
                    Invoke((MethodInvoker)delegate ()
                        {
                            control.Text = text_new;
                        });

                    }
            }
        }

        /// <summary>
        /// Вызывается при изменении здоровья
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChangeHp(object sender, ChangeHpEventArgs e)
        {
            // потокобезопасное изменение текста кнопки
            Invoke((MethodInvoker)delegate ()
            {
                hp_label.Text = "❤ " + e.Hp_new;
            });
        }

        /// <summary>
        /// Вызывается при клике на могилу и уменьшает прогресс восстания мертвеца на 1 единицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPush(object sender, EventArgs e)
        {
            string text = (sender as Button).Name.Substring("button".Length);
            int number = Int32.Parse(text) - 1; // - 1 потому, что в игре индексация начинается с 0 (массив)
            GameAdapter.Game.Push(number);
        }

    }
}
