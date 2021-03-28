using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KresticsAndNulls {
    public partial class Form1 : Form {
        bool turn = true;   // Чей ход, 0 - нолик, 1 - крестик
        int n = 3;          // Размерность
        int count = 0;

        Button[,] btn;

        private void createButtons(int m) {
            int left = 0;
            int top = 0;
            btn = new Button[n, n];
            int step = this.panel1.Width / n;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    btn[i, j] = new Button();
                    btn[i, j].Left = left + step * j;
                    btn[i, j].Top = top;
                    btn[i, j].Width = step;
                    btn[i, j].Height = step;
                    btn[i, j].Click += ButtonOnClick;
                    btn[i, j].Tag = 0;
                    panel1.Controls.Add(btn[i, j]);
                }
                left = 0;
                top += step;
            }
        }

        public Form1() {
            InitializeComponent();
            regame();
        }

        private void clearButtons() {
            if (btn == null)
                return;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    panel1.Controls.Remove(btn[i, j]);
                }
            }
        }

        private void regame() {
            turn = true;
            count = 0;
            clearButtons();
            createButtons(3);
        }

        // Поцедура для всех динамически создаваемых кнопок
        private void ButtonOnClick(object sender, EventArgs eventArgs) {
            var button = (Button)sender;
            Image Img = null;

            if (button.Image == null) {
                if (n == 3)
                    Img = Image.FromFile("../../src/x3.png");

                if (n == 4)
                    Img = Image.FromFile("../../src/x4.png");

                if (n == 5)
                    Img = Image.FromFile("../../src/x5.png");

                button.Image = Img;
                button.Tag = 1;
                button.Refresh();
                count++;
                if (!check()) {
                    turn = !turn;
                    System.Threading.Thread.Sleep(300);
                    bot_click();
                }
            }
        }

        private void Play_Click(object sender, EventArgs e) {
            turn = true;
            int new_n = Convert.ToInt32(textBox1.Text);

            if ((new_n >= 3) && (new_n <= 5)) {
                clearButtons();
                n = new_n;
                createButtons(n);
            }
            else { MessageBox.Show("Введите нормальное число!"); }
        }

        // Логика бота
        private void bot_click() {
            Random Rand = new Random();
            Image Img = null;
            while (true) {
                //MessageBox.Show();
                int x = Rand.Next(0, n);
                int y = Rand.Next(0, n);
                Button but = btn[x, y];

                if (but.Image == null) {
                    if (n == 3)
                        Img = Image.FromFile("../../src/o3.png");

                    if (n == 4)
                        Img = Image.FromFile("../../src/o4.png");

                    if (n == 5)
                        Img = Image.FromFile("../../src/o5.png");

                    but.Image = Img;
                    but.Tag = 2;
                    but.Refresh();
                    count++;
                    break;
                }
            }
            if (!check())
                turn = !turn;
        }

        // Проверка на победу, но не работает
        private bool check() {
            // 1 - крестик(Человек), 2 - нолик(бот) 
            bool horiz_win = check_horiz_win();
            bool vertic_win = check_vertic_win();
            bool diag_win = check_diag_win();
            bool draw = (count == n * n) && !horiz_win && !vertic_win && !diag_win;

            if (horiz_win || vertic_win || diag_win || draw) {
                if (draw) {
                    MessageBox.Show("Ничья!");
                }
                else {
                    if (turn)
                        MessageBox.Show("Победа крестиков!");
                    else
                        MessageBox.Show("Победа ноликов!");
                }
                regame();
                return true;
            }
            return false;
        }

        private bool check_horiz_win() {
            bool horiz_win = false;
            int cur_tag = -1;
            for (int i = 0; i < n; i++) {
                bool is_horiz_win = true;
                cur_tag = (int)btn[i, 0].Tag;
                if (cur_tag == 0)
                    continue;
                for (int j = 1; j < n; j++)
                    if ((int)btn[i, j].Tag != cur_tag) {
                        is_horiz_win = false;
                        break;
                    }
                horiz_win = horiz_win || is_horiz_win;
            }
            return horiz_win;
        }

        private bool check_vertic_win() {
            bool vertic_win = false;
            int cur_tag = -1;
            for (int i = 0; i < n; i++) {
                bool is_vertic_win = true;
                cur_tag = (int)btn[0, i].Tag;
                if (cur_tag == 0)
                    continue;
                for (int j = 1; j < n; j++)
                    if ((int)btn[j, i].Tag != cur_tag) {
                        is_vertic_win = false;
                        break;
                    }
                vertic_win = vertic_win || is_vertic_win;
            }
            return vertic_win;
        }


        private bool check_diag_win() {
            bool diag_win = false;
            int cur_tag = (int)btn[0, 0].Tag;
            if (cur_tag != 0) {
                diag_win = true;
                for (int i = 1; i < n; i++) {
                    if ((int)btn[i, i].Tag != cur_tag) {
                        diag_win = false;
                        break;
                    }
                }
            }
            if (diag_win)
                return true;

            cur_tag = (int)btn[0, n - 1].Tag;
            if (cur_tag != 0) {
                diag_win = true;
                for (int i = 1; i < n; i++) {
                    if ((int)btn[i, n - i - 1].Tag != cur_tag) {
                        diag_win = false;
                        break;
                    }
                }
            }
            return diag_win;
        }
    }
}
