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
        int count = 1; 

        Button [,] btn;

        private void createButtons(int m)
        {
            int left = 0;
            int top = 0;
            btn = new Button[n, n];
            int step = this.panel1.Width / n;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
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
            createButtons(3);            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
            
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
            Image Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o3.png");

            if (button.Image == null)
            {
                //MessageBox.Show("Прощай " + button.Name);
                if (n == 3)
                {
                    if (turn)
                    { Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/x3.png"); }
                    else
                    { Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o3.png"); }
                }

                if (n == 4)
                {
                    if (turn)
                    { Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/x4.png"); }
                    else
                    { Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o4.png"); }
                }

                if (n == 5)
                {
                    if (turn)
                    { Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/x5.png"); }
                    else
                    { Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o5.png"); }
                }

                button.Image = Img;
                button.Tag = 1;
                check();
                turn = !turn;

                if (n == 3)
                {
                    if (count <= 4)
                    {
                        bot_click();
                    }
                }

                if (n == 4)
                {
                    if (count <=8)
                    {
                        bot_click();
                    }
                }

                if (n == 5)
                {
                    if (count <= 12)
                    {
                        bot_click();
                    }
                }
                
                //button.Dispose();
            }

            
            

        }
        private void ClearTable(object sender, EventArgs eventArgs)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            Brush brr = Brushes.Red;
            Font fnt = new Font("Arial", 20);
            string s = n.ToString();
            g.FillRectangle(Brushes.SkyBlue, 450, 350, 30,30);
            g.DrawString(s, fnt, brr, 450, 350);
        }

        private void Play_Click(object sender, EventArgs e)
        {
            int left = 0;
            int top = 0;
            count = 1;
            turn = true;

            if ((n >= 3) && (n <= 5))
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        panel1.Controls.Remove(btn[i, j]);
                    }
                }


                n = Convert.ToInt32(textBox1.Text);

                btn = new Button[n, n];
                int step = (int)420 / n;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        btn[i, j] = new Button();
                        btn[i, j].Parent = panel1;
                        btn[i, j].Left = left + step * j;
                        btn[i, j].Top = top;
                        btn[i, j].Width = step;
                        btn[i, j].Height = step;
                        //btn[i, j].Name = "btn" + (int)(i * n + j);
                        btn[i, j].Click += ButtonOnClick;

                        //panel1.Controls.Add(btn[i, j]);
                        //top += button.Height + 2;
                    }
                    left = 0;
                    top += step;
                }
            }
            else { MessageBox.Show("Введите нормальное число!"); }
        }
            

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //n = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода чисел!");
            }
        }

        private void Play_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void bot_click()
        {
            Random Rand = new Random();
            count++;
            Image Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o3.png");
            while (true)
            {
                if (!turn)
                {
                    //MessageBox.Show();
                    int x = Rand.Next(0, n);
                    int y = Rand.Next(0, n);
                    Button but = btn[x,y];

                    if (but.Image == null)
                    {
                        if (n == 3)
                        {
                            Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o3.png");
                        }

                        if (n == 4)
                        {
                           Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o4.png"); 
                        }

                        if (n == 5)
                        {
                           Img = Image.FromFile("C://Developing/C Sharp/KresticsAndNulls/src/o5.png");
                        }

                        but.Image = Img;
                        but.Tag = 2;
                        check();
                        turn = !turn;
                        break;
                    }
                }
            }
        }

        private void check()
        {

            if (((btn[0, 0].Tag == btn[0, 1].Tag) && (btn[0, 1].Tag == btn[0, 2].Tag) && ((int)btn[0, 0].Tag != 0)) ||
                ((btn[1, 0].Tag == btn[1, 1].Tag) && (btn[1, 1].Tag == btn[1, 2].Tag) && ((int)btn[1, 0].Tag != 0)) ||
                ((btn[2, 0].Tag == btn[2, 1].Tag) && (btn[2, 1].Tag == btn[2, 2].Tag) && ((int)btn[2, 0].Tag != 0)))
                if (turn)
                {
                    MessageBox.Show("Победа крестиков");
                }
                else
                {
                    MessageBox.Show("Победа ноликов");
                }

        }

    }
}
