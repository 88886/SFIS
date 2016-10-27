using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AutoSaveLog
{
    public partial class SaveLog : Form
    {
        public SaveLog()
        {
            InitializeComponent();
        }
        string Logfilepath = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                MessageBox.Show("请先选择原始LOG存放路径");
                this.button2_Click(null, null);
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
                return;

            this.Logfilepath = this.textBox2.Text.Trim();
            Thread th = new Thread(new ThreadStart(RunCopyLogFile));
            th.Start();
            this.button1.Enabled = false;
            this.label2.Text = "LOG文件处理中请不要退出程序....";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("E:\\TEST_LOG"))
                Directory.CreateDirectory("E:\\TEST_LOG");
            this.textBox2.Text = @"E:\LOG_ATE\report";
            if (!Directory.Exists(textBox2.Text))
            {
                ShowMsg("\"" + this.textBox2.Text + "\" 路径不存在,请重新选择..");
                return;
            }
            this.button1_Click(null, null);
        }
        private void SHOWState(string str)
        {
            this.label3.Invoke(new EventHandler(delegate
            {
                this.label3.Text = str;
            }));
        }
        private void RunCopyLogFile()
        {
            #region 执行
            while (true)
            {
                try
                {
                    TimeSpan ts3 = new TimeSpan();
                    DirectoryInfo Di = new DirectoryInfo(Logfilepath);
                    FileInfo[] arrFi = Di.GetFiles("*.txt");
                    DateTime myDt = DateTime.Now;
                    int x = 0;
                    foreach (FileInfo item in arrFi)
                    {
                        myDt = DateTime.Now;
                        x++;
                        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(item.CreationTime.ToString()).Ticks);
                        TimeSpan ts2 = new TimeSpan(myDt.Ticks);
                        ts3 = ts1.Subtract(ts2).Duration();
                        string filepath = string.Empty;
                        string fileDir = string.Empty;

                        if (ts3.Minutes >= 1)
                        {
                            string[] arrStr = File.ReadAllLines(item.FullName, System.Text.Encoding.Default);
                            foreach (string str in arrStr)
                            {
                                if (str.IndexOf("FilePath") != -1)
                                {
                                    fileDir = Path.GetDirectoryName(filepath = str.Substring(9, str.Length - 9));
                                    fileDir = "e:\\TEST_LOG" + fileDir.Substring(2, fileDir.Length - 2);
                                    filepath = "e:\\TEST_LOG" + filepath.Substring(2, filepath.Length - 2);
                                    break;
                                }
                                if (str.IndexOf("C:") != -1 || str.IndexOf("D:") != -1 || str.IndexOf("E:") != -1 || str.IndexOf("F:") != -1)
                                {
                                    int index = str.IndexOf("html");
                                    fileDir = Path.GetDirectoryName(filepath = str.Substring(0, index + 4));
                                    fileDir = "e:\\TEST_LOG" + fileDir.Substring(2, fileDir.Length - 2);
                                    filepath = "e:\\TEST_LOG" + filepath.Substring(2, filepath.Length - 2);
                                    break;
                                }

                            }
                            if (!string.IsNullOrEmpty(fileDir))
                            {
                                if (!Directory.Exists(fileDir))
                                    Directory.CreateDirectory(fileDir);

                                if (File.Exists(filepath))
                                {
                                    filepath = string.Format(@"{0}\{1}_A{2}", System.IO.Path.GetDirectoryName(filepath), Path.GetFileNameWithoutExtension(filepath), Path.GetExtension(filepath));
                                }
                                File.Move(item.FullName, filepath);
                                //File.Copy(item.FullName, filepath);
                                this.ShowMsg(string.Format("时间：{0}/移动：{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    item.FullName));
                            }

                        }

                        if ((x % 30) == 0)
                            this.richTextBox1.Clear();
                        SHOWState(string.Format("{0}/{1}", x, arrFi.Length));
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message);
                }
                Thread.Sleep(500);
            }
            #endregion
        }


        private void ShowMsg(string msg)
        {
            this.richTextBox1.Invoke(new EventHandler(delegate
            {
                this.richTextBox1.AppendText(msg + "\n");
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = fbd.SelectedPath;
            }
        }
    }
}

