using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SocketClient
{
    public partial class frmClient : Form
    {
        public static Socket ClientSocket;
        public frmClient()
        {
            InitializeComponent();
            assimblePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        System.Windows.Forms.Timer time1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer time2 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerByClient = new System.Windows.Forms.Timer();

        public void ShowPrgMsg(string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                   // rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }
        private void frmClient_Load(object sender, EventArgs e)
        {
            time1.Interval = 50;
            time1.Enabled = true;
            time1.Tick += new EventHandler(time1_Tick);

            time2.Interval = 1000;
            time2.Enabled = true;
            time2.Tick += new EventHandler(time2_Tick);

            timerByClient.Interval = 100;
            timerByClient.Enabled = false;
            timerByClient.Tick += new EventHandler(timerByClient_Tick);

            xx = panel1.Size.Width;
            tbip.Text = ReadIniFile.IniReadValue("SOCKET", "IP", System.Environment.CurrentDirectory + "\\SFIS.INI");
            tb_port.Text = ReadIniFile.IniReadValue("SOCKET", "PORT", System.Environment.CurrentDirectory + "\\SFIS.INI");
        }
        int xx = 0;
        private void time1_Tick(object sender, EventArgs e)
        {

            label5.Location = new Point(xx--, 7);
            if (xx == (0 - label5.Size.Width))
            {
                xx = panel1.Size.Width;
            }
        }
        private void time2_Tick(object sender, EventArgs e)
        {
            if (label5.ForeColor == Color.Maroon)
            {
                label5.ForeColor = Color.Blue;
            }
            else
            {
                label5.ForeColor = Color.Maroon;
            }
        }

        private void tb_duankou_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }


        private void tb_dataInput_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_dataInput.Text)) && (e.KeyCode == Keys.Enter))
            {

                
               btn_send_Click(null,null);
               labInput.Text = tb_dataInput.Text;
               tb_dataInput.Text = "";
            }
        }

        #region variable
        /**
         * 客户端所需变量
         * */
        //客户端连接对象
        private TcpClient client;
        //客户端网络工作流
        private NetworkStream nsClient;
        //接收服务端传来的消息
        private byte[] msgBytesByServer;
        static string assimblePath = "";
        private bool connetState = false;
        bool SendFlag = false;
        #endregion   

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "正在连线......";
                //获取连接服务器Tcp端口号
                Int16 port = Int16.Parse(tb_port.Text);
                //创建客户端连接对象
                client = new TcpClient();
                // 与服务器建立连接
                client.Connect(tbip.Text, port);
             //   frmServer_onDownLoadList("已建立" + client.Client.LocalEndPoint + "与" + client.Client.RemoteEndPoint + "的连接");
                //获得网络工作流
                nsClient = client.GetStream();
             //   frmServer_onDownLoadList("获得网络工作流对象");
                connetState = true;
                btn_connet.Enabled = false;
               // btnClose.Enabled = true;
                this.timerByClient.Enabled = true;
                label1.Text = "连线成功......";
                SendFlag = true;
                tb_dataInput.Focus();
                tbip.ReadOnly = true;
                tb_port.ReadOnly = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("输入服务器IP地址或端口号不是有效数据!", "输入数据格式错误");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("服务器IP地址或端口号不能为空！", "输入数据错误！");
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("指定服务器连接端口未启动！", "连接到服务器错误！");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("操作无效！", "连接到服务器错误！");
            }
            catch (SocketException)
            {
                MessageBox.Show("连接服务器失败！", "失败");
            }
        }     
      
        public void dispose()
        {
            if (nsClient != null)
                nsClient.Close();
            if (client != null)
                client.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dispose();
            this.Close();
        }

        #region 读取来自客户端的数据
        /// <summary>
        /// 读取来自客户端的数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void BeginReadServerMsg(NetworkStream nsClient)
        {
            lock (nsClient)
            {
                try
                {
                    if (nsClient.CanRead && nsClient.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsClient.DataAvailable)
                        {
                            byte[] msgByte = new byte[10240];
                            //每次从流中读取1KB的数据
                            readSize = nsClient.Read(msgByte, 0, 10240);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                        }
                        msgBytesByServer = new byte[totalSize];
                        // **** 这里，从ms中读取数据前，ms指针必须回零，不然会出错。****
                        ms.Position = 0;

                        //将ms临时流中保存的数据全部读出
                        int readAllSize = ms.Read(msgBytesByServer, 0, totalSize);   
                        //将接收到的byte[]转成String
                        String serverMsg = Encoding.Default.GetString(msgBytesByServer, 0, msgBytesByServer.Length);

                        // **** 在数组上调用ToString()得不到数据的                  
                       // labshowmsg.Text = serverMsg.ToString();
                        ShowPrgMsg(serverMsg.ToString()+"\r\n");
                        nsClient.Flush();
                        ms.Close();
                        SendFlag = true;
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 客户端用于监听服务端消息的订时器
        private void timerByClient_Tick(object sender, EventArgs e)
        {
            try
            {
                lock (nsClient)
                {
                    if (nsClient != null && nsClient.CanRead)
                    {
                        if (nsClient.DataAvailable)
                        {
                            BeginReadServerMsg(nsClient);
                        }
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show("数据无法读取，流对象已被销毁或与服务端已断开连接！详细信息：" + ex.ToString());
            }
        }
        #endregion 
 
  
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (!connetState)
            {
              //  labshowmsg.Text = "未连接服务器......";
                ShowPrgMsg("未连接服务器......");
                
                return;
            }

            if (!SendFlag)
            {
                ShowPrgMsg("服务器未响应,请稍等......");
                return;
            }

            if (nsClient != null)
            {
               
                try
                {
                    SendFlag = false;

                    byte[] dataBuffer;     //存储数据的byte[]
                    byte[] headBuffer = new byte[100];        //存储发送数据头
                    byte[] totalBuffer;
                    //获取要发送的数据
                    String msg = this.tb_dataInput.Text;
                    String hMsg = "DATA:";

                    //将string转成byte[]
                    byte[] hBuffer = Encoding.Default.GetBytes(hMsg);//存储头部信息

                    hBuffer.CopyTo(headBuffer, 0);
                    dataBuffer = Encoding.Default.GetBytes(msg);//存储头部字节数据

                    totalBuffer = new byte[headBuffer.Length + dataBuffer.Length];
                    headBuffer.CopyTo(totalBuffer, 0);
                    dataBuffer.CopyTo(totalBuffer, headBuffer.Length);

                 
                    //向流中写数据发送到客户端
                    nsClient.Write(totalBuffer, 0, totalBuffer.Length);
                    //发送数据
                    nsClient.Flush();
          
                }
                catch (ObjectDisposedException)
                {
                    MessageBox.Show("与服务器的连接已断开！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            connetState = false;
            dispose();
            Application.Exit();
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {

            ReadIniFile.IniWriteValue("SOCKET", "IP", tbip.Text, System.Environment.CurrentDirectory+"\\SFIS.INI");
            ReadIniFile.IniWriteValue("SOCKET", "PORT", tb_port.Text, System.Environment.CurrentDirectory+"\\SFIS.INI");


            connetState = false;
            dispose();
        }


    }

}

