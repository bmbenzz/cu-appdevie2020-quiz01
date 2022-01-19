using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
ควยยยยยยยยย ดราก้อน 3 นิ้ว สั้นจังวะ อิอิ
namespace quiz2020_1
{
    public partial class Form1 : Form
    {
        Form2 frm2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            frm2.Show();
            button4.Enabled = false;
        }
        
        double gameTime = 0;
        Random rd = new Random();
        bool isEnd = true;
        public int[,] square = new int[3, 3];
        private void button1_Click(object sender, EventArgs e) // start
        {
            if (isEnd)
            {
                timer1.Start();
                isEnd = false;
            }
            for (int sqx = 0; sqx < 3; sqx++)
                for (int sqy = 0; sqy < 3; sqy++)
                {
                    square[sqx, sqy] = 0;
                }
            for (int i = 1; i <=9; i++)
            {
                bool filled = false;
                do
                {
                    int sqx = rd.Next(0, 3);
                    int sqy = rd.Next(0, 3);
                    if (square[sqx,sqy] == 0)
                    {
                        square[sqx, sqy] = i;
                        filled = true;
                    }
                } while (filled == false);
            }
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gameTime += 1;
            label1.Text = "time : " + (gameTime / 10).ToString() + " s";
            if (textBox1.Text == textBox2.Text && textBox2.Text != "")
            {
                timer1.Stop();
                isEnd = true;
            }
            if (gameTime >= 200)
            {
                timer1.Stop();
                isEnd = true;
                MessageBox.Show("หมดเวลา");
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int dx = e.X / (pictureBox1.Width / 3);
            int dy = e.Y / (pictureBox1.Height / 3);
            textBox1.Text += square[dx, dy];
            frm2.setLabel1(textBox1.Text);
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i<=3; i++)
            {
                Point v1 = new Point(pictureBox1.Width * i / 3, 0);
                Point v2 = new Point(pictureBox1.Width * i / 3, pictureBox1.Height);
                Point h1 = new Point(0, pictureBox1.Height * i / 3);
                Point h2 = new Point(pictureBox1.Width, pictureBox1.Height *i / 3);
                g.DrawLine(Pens.Black, v1, v2);
                g.DrawLine(Pens.Black, h1, h2);
            }
            for (int sqx = 0; sqx < 3; sqx++)
                for (int sqy = 0; sqy < 3; sqy++)
                {
                    Point sq = new Point(sqx * 75, sqy * 82);
                    Font ft = new Font("Arial", 40.0f);
                    g.DrawString(square[sqx, sqy].ToString(), ft, Brushes.Black, sq);
                }
        }

        private void button2_Click(object sender, EventArgs e) // clear
        {
            textBox1.Text = "";
            frm2.setLabel1(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frm2.myparent = this;
        }

        private void button4_Click(object sender, EventArgs e) //show frm2
        {
            frm2.Show();
            button4.Enabled = false;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e) //hide frm2
        {
            frm2.Hide();
            button4.Enabled = true;
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e) // save pw
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
            sw.WriteLine(textBox2.Text);
            sw.Close();
        }

        private void button7_Click(object sender, EventArgs e) // read pw
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            textBox2.Text = sr.ReadToEnd();
            sr.Close();
        }

        private void button3_Click(object sender, EventArgs e) // core.exe pw
        {
            textBox2.Text = core.toolbox.get_data();
        }
    }
}
