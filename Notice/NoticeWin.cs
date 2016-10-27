using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Notice
{
    public partial class NoticeWin :Office2007Form// Form
    {
        public delegate void CallBack(int count);
        private string txt = string.Empty;
        private int wait = 5000;
        private string detail = string.Empty;
        private string creatTime = string.Empty;
        private int count = 0;
        private bool show = false;
        private bool detailClosed = true;
        private CallBack cb = null;
        private static int number = 0;
        
        private static readonly object lck = new object();

        private NoticeWin()
        {
            InitializeComponent();
        }

        public NoticeWin(string noti)
        {
            this.txt = noti;
            InitializeComponent();
        }
        public NoticeWin(string noti, int wait_time)
        {
            this.txt = noti;
            wait = wait_time;
            InitializeComponent();
        }

        public NoticeWin(string noti, int wait_time, int count, CallBack cb)
        {
            this.txt = noti;
            wait = wait_time;
            this.count = count;
            this.cb = cb;
            InitializeComponent();
        }

        public NoticeWin(Notice notice, int count, CallBack cb)
        {
            detail = notice.detail;
            this.txt = notice.msg;
            wait = notice.wait==0?wait:notice.wait;
            creatTime = notice.CreatTime.ToString("yyyy/MM/dd hh:mm:ss");
            this.count = count;
            this.cb = cb;
            InitializeComponent();
        }


        private void Notice_Load(object sender, EventArgs e)
        {
            lock (lck)
            {
                this.Text = "提示" + (++number).ToString();
            }
            this.Width = NoticeCenter.Cfg.Width;
            this.Height = NoticeCenter.Cfg.Height;
            this.Disposed += new EventHandler(NoticeWin_Disposed);
            this.MouseLeave += new EventHandler(Notice_MouseLeave);
            this.MouseMove += new MouseEventHandler(Notice_MouseMove);
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width);
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - (count * this.Height));//-(count*this.Height)
            label1.Width = this.Width - 20;
            label1.Height = this.Height - 70;
            this.label1.Text = "    " + txt;
            linkLabel1.Top = this.Height - 40;
            linkLabel1.Left = this.Width - 145;
            checkBox1.Top = this.Height - 41;
            checkBox1.Left = this.Width - 80;
            label2.Text = creatTime;
            timer1.Interval = 10;
            timer1.Enabled = true;
        }

        void NoticeWin_Disposed(object sender, EventArgs e)
        {
            if (cb != null)
            {
                cb(count);
            }
        }

        void Notice_MouseLeave(object sender, EventArgs e)
        {
            timer1.Interval = wait;
            timer1.Enabled = true;
        }

        void Notice_MouseMove(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            this.Opacity = 1.0;
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (!show)
            {
                int target = ((Screen.PrimaryScreen.WorkingArea.Height - (count * this.Height)) - this.Height);
                if (this.Top - 10 >= target)
                {
                    this.Top -= 10;
                }
                else
                {
                    this.Top = ((Screen.PrimaryScreen.WorkingArea.Height - (count * this.Height)) - this.Height);
                    show = true;
                    timer1.Interval = wait;
                }
            }
            else
            {
                if (this.Opacity > 0)
                {
                    if (detailClosed)
                    {
                        timer1.Interval = 100;
                        this.Opacity = this.Opacity - 0.1;
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //new CfgLoader().SetConfig("show_notice", checkBox1.Checked ? "0" : "1");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (detail == null || detail.Length == 0)
            {
                detail = txt;
            }
            DetailWin dw = new DetailWin(detail, Callback);
            dw.Show();
            detailClosed = false;
        }

        private void Callback()
        {
            detailClosed = true;
        }

       
    }
}
