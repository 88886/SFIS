using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace 测试用例
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }


        Point p1;
        Point p2;
        ArrayList mya = new ArrayList();
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (p1.X == 0 && p1.Y == 0)
            {
                p1 = p;
                mya.Add(p1);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            p2 = p;
            mya.Add(p2);
            Graphics g = this.CreateGraphics();
            g.DrawLine(Pens.Red, p1, p2);
            p1 = p2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = string.Format("{0:HHmm}", this.textBox3.Text);
            this.dateTimePicker1.Text = s;
            //int number = mya.Count;
            //Point[] myp = new Point[number];
            //Object[] ob = new Object[number];

            //for (int i = 0; i < number; i++)
            //{
            //    ob[i] = mya[i];
            //    myp[i] = (Point)ob[i];
            //}
            //Graphics g = this.CreateGraphics();
            //g.DrawPolygon(Pens.Red, myp);
            //g.FillPolygon(Brushes.Red, myp);
        }

        int x;
        int y;
        int z;
        int h;
        private void button2_Click(object sender, EventArgs e)
        {
            
            x = Math.Abs( 2 * -1);
            y = -2 * -1;

            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Blue, 5);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(p, 10, 30, 200, 30);
            g.Dispose();
            p.Dispose();
        }
    }
}

