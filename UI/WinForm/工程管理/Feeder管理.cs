using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Net;
using System.Net.Sockets;//导入命名空间
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace SFIS_V2
{
    public partial class FeederManage : Office2007Form// Form
    {
        public FeederManage(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
            clientQueue = new Queue<string>();
            locker = new object();
        }
        MainParent mFrm;
        private delegate void delegateshowdata();
        delegateshowdata dsd;
        #region socket服务
        #region variable
        /**
         * 服务端所需变量
         * */
        //服务器端监听对象
        private TcpListener listener;
        private NetworkStream nsServer;
        //存储客户端的消息byte[]
        private byte[] msgBytesByClient;
        //用于标识用户客户端执行是何种操作(0:发送数据　1:发送文件 2:发送数据和文件)
     //   private int iFunction = 0;
        //存储文件跟数据的总共byte[]
      //  private byte[] totalBuffer;
     //   private PrintDocument pdoc;
        private String printText;

        private String idcard;


        /**
         * 客户端所需变量
         * */
        //客户端连接对象
        private TcpClient client;
        //客户端网络工作流
        private NetworkStream nsClient;
        //接收服务端传来的消息
        private byte[] msgBytesByServer;
        private Queue<string> clientQueue;
        private object locker;
        private Thread workerThread;
        #endregion

        #region 启动服务器
        /// <summary>
        /// 启动服务器按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = int.Parse(txtServerPort.Text);
                IPAddress localAddr = IPAddress.Parse(this.txtServerIP.Text);
                frmServer_onDownLoadList("正在创建" + txtServerIP.Text + "服务端TcpListener对象");
                //创建TcpListener监听对象
                listener = new TcpListener(localAddr, port);
                frmServer_onDownLoadList("已成功创建TcpListener对象，端口号为：" + txtServerPort.Text);
                //启动对端口的连接请求监听
                listener.Start();
                //this.button1.Enabled = true;
                this.btnStartServer.Enabled = false;
                //启动定时器
                this.timerByServer.Enabled = true;
                Thread th = new Thread(new ThreadStart(ListenerServer));
                th.IsBackground = true;
                th.Start();
                this.mFrm.ShowPrgMsg("成功启动服务!", MainParent.MsgType.Outgoing);
            }
            catch (FormatException fx)
            {
                MessageBox.Show("服务器IP地址或端口号不是有效数据！详细信息：" + fx.ToString(), "输入数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("IP地址或端口号为空！详细信息：" + ex.ToString(), "输入数据错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("TcpListener启动失败！详细信息：" + ex.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 线程启动方法监听客户端的连接请求
        public void ListenerServer()
        {
            try
            {
                while (true)
                {
                    //判断是否客户端连接请求
                    if (listener.Pending())
                    {
                        // 接收客户端的请求，并创建一个客户端连接
                        TcpClient client = listener.AcceptTcpClient();
                        this.frmServer_onDownLoadList("接收来自" + client.Client.RemoteEndPoint + "请求！");
                        //为每一个客户端创建一个线程用于监听客户端消息
                        Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                        thread.IsBackground = true;
                        thread.Start(client);
                    }
                    Thread.Sleep(200);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！,详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 客户端用于监听服务端消息的定时器
        private void timerByClient_Tick(object sender, EventArgs e)//?
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
                            frmServer_onDownLoadList("已接收" + readSize + "字节的数据");
                        }
                        frmServer_onDownLoadList("服务端共发送" + totalSize + "字节的数据");
                        msgBytesByServer = new byte[totalSize];

                        // **** 这里，从ms中读取数据前，ms指针必须回零，不然会出错。****
                        ms.Position = 0;

                        //将ms临时流中保存的数据全部读出
                        int readAllSize = ms.Read(msgBytesByServer, 0, totalSize);
                        frmServer_onDownLoadList("已接收到" + readAllSize + "字节的数据,字节数据的长度：" + msgBytesByServer.Length);
                        //将接收到的byte[]转成String
                        String serverMsg = Encoding.Default.GetString(msgBytesByServer);

                        // **** 在数组上调用ToString()得不到数据的
                        frmServer_onDownLoadList("服务端消息：" + serverMsg.ToString());
                        nsClient.Flush();
                        ms.Dispose();

                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 服务器用于监听客户端连接的定时器
        private void timerByServer_Tick(object sender, EventArgs e)
        {
            try
            {
                //判断是否客户端连接请求
                if (listener.Pending())
                {
                    // 接收客户端的请求，并创建一个客户端连接
                    TcpClient client = listener.AcceptTcpClient();
                    this.lbxServer.Items.Add("接收来自" + client.Client.RemoteEndPoint + "请求！");
                    //为每一个客户端创建一个线程用于监听客户端消息
                    Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                    thread.IsBackground = true;
                    thread.Start(client);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 创建接受客户端请求的网络工作流对象
        /// <summary>
        /// 创建网络工作流对象
        /// </summary>
        /// <param name="o">线程执行方法参数对象</param>
        public void CreateNetworkstream(Object o)
        {
            TcpClient client = o as TcpClient;
            CreateNetworkstream(client);
        }
        #endregion

        #region 创建客户端网络工作流对象
        /// <summary>
        /// 接受客户端的连接请求并创建网络工作流对象
        /// </summary>
        /// <param name="client">连接请求TcpClient对象</param>
        public void CreateNetworkstream(TcpClient server)
        {

            try
            {
                //接受客户端连接请求
                NetworkStream nsServer = server.GetStream();
                this.nsServer = nsServer;
                frmServer_onDownLoadList("创建" + server.Client.RemoteEndPoint + "客户端的网络工作流对象");
                while (true)
                {
                    if (nsServer == null)
                    {
                        Thread.CurrentThread.Abort();
                        this.frmServer_onDownLoadList("销毁" + server.Client.RemoteEndPoint + "客户端的线程");
                        break;
                    }
                    if (nsServer != null && nsServer.CanRead && nsServer.DataAvailable)
                    {
                        ReadByClient(nsServer);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建客户端网络工作流失败！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 开始读取来自客户端的数据
        /// <summary>
        /// 读取客户端数据
        /// </summary>
        /// <param name="nsClient"></param>
        public string ReadDataByClient(byte[] msgBytesByClient)
        {

            //将接收到的byte[]转成String
            String serverMsg = Encoding.Default.GetString(msgBytesByClient, 0, msgBytesByClient.Length);
            frmServer_onDownLoadList("客户端说：" + serverMsg);
            return serverMsg.ToString();
        }
        #endregion

        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {

                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        long totalSize = 0;
                        frmServer_onDownLoadList("正在接收客户端数据，请稍候……");
                        byte[] hBuffer = new byte[100];
                        nsServer.Read(hBuffer, 0, 100);
                        string hMsg = Encoding.Default.GetString(hBuffer, 0, 100);
                        totalSize += 100;


                        if (hMsg.Trim().Replace("\0", "").IndexOf("DATA:") != -1)
                        {
                            MemoryStream ms = new MemoryStream();
                            while (nsServer.DataAvailable)
                            {
                                try
                                {
                                    byte[] msgByte = new byte[40960];
                                    //每次从流中读取1KB的数据
                                    readSize = nsServer.Read(msgByte, 0, 40960);
                                    //累计总共流中保存的字节数
                                    totalSize += readSize;
                                    //写入临时流中用于一次性全部读取数据
                                    ms.Write(msgByte, 0, readSize);
                                    Thread.Sleep(50);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                }
                            }
                            byte[] msgByClient = new byte[ms.Length];
                            ms.Position = 0;

                            //将ms临时流中保存的文件以及数据全部读出
                            int readAllSize = ms.Read(msgByClient, 0, (int)msgByClient.Length);
                            frmServer_onDownLoadList("收到客户端发送" + totalSize + "字节的数据");
                            this.cbx_fixtureid.Invoke(new EventHandler(delegate
                            {
                                this.cbx_fixtureid.Text = ReadDataByClient(msgByClient);
                            }));
                            // this.tb_fixtureid.Text = ReadDataByClient(msgByClient);

                        }
                        else
                        {
                            try
                            {
                                if (hMsg.Split(':').Length >= 2)
                                {
                                    string filename = hMsg.Split(':')[1].Trim().Replace("\0", "");

                                    if (!Directory.Exists(Application.StartupPath + @"\Download"))
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + @"\Download\");
                                    }
                                    String filePath = Application.StartupPath + @"\Download\" + filename;
                                    //将接收到的byte[]写成文件
                                    try
                                    {
                                        FileStream fs = new FileStream(filePath, FileMode.Create);
                                        while (nsServer.DataAvailable)
                                        {
                                            try
                                            {
                                                Thread.Sleep(50);
                                                byte[] filebyte = new byte[40960];
                                                //每次从流中读取1KB的数据
                                                readSize = nsServer.Read(filebyte, 0, 40960);
                                                //累计总共流中保存的字节数
                                                totalSize += readSize;
                                                //写入临时流中用于一次性全部读取数据
                                                fs.Write(filebyte, 0, readSize);
                                                fs.Flush();
                                                frmServer_onDownLoadList1("已接收：" + totalSize + " 字节");
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                            }
                                        }

                                        frmServer_onDownLoadList("收到客户端发送" + totalSize + "字节的【" + filename + "】文件");
                                        if (nsServer != null)
                                        {
                                            //获取要发送的数据
                                            String msg = "服务器端已接收【" + filename + "】文件";
                                            //将string转成byte[]
                                            Byte[] data = Encoding.Default.GetBytes(msg);
                                            //向流中写数据发送到客户端
                                            nsServer.Write(data, 0, data.Length);
                                        }
                                        fs.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("写文件发生异常！" + ex.ToString());
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
                finally
                {
                    nsServer.Flush();
                }

            }
        }
        #endregion


        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadFileByClient(byte[] msgBytesByClient, string fileName)
        {
            if (!Directory.Exists(Application.StartupPath + @"\Download"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Download\");
            }
            String filePath = Application.StartupPath + @"\Download\" + fileName;
            //将接收到的byte[]写成文件
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                fs.Write(msgBytesByClient, 0, msgBytesByClient.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写文件发生异常！" + ex.ToString());
            }
        }
        #endregion

        #region 读取客户端封装的文件和数据byte[]
        /// <summary>
        /// 读取客户端传送过来的存储着文件和数据byte[]
        /// </summary>
        /// <param name="nsServer"></param>
        public void ReadFileAndDataByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {
                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsServer.DataAvailable)
                        {
                            byte[] msgByte = new byte[1024];
                            //每次从流中读取1KB的数据
                            readSize = nsServer.Read(msgByte, 0, 1024);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            lbxServer.Items.Add("已接收" + readSize + "字节的数据");
                        }

                        lbxServer.Items.Add("客户端共发送" + totalSize + "字节的数据");
                        msgBytesByClient = new byte[totalSize];
                        ms.Position = 0;

                        //将ms临时流中保存的文件以及数据全部读出
                        int readAllSize = ms.Read(msgBytesByClient, 0, totalSize);

                        //取出byte[]中的文件数据并生成文件
                        String filePath = Application.StartupPath + @"\zp.wlt";
                        //将接收到的byte[]写成文件
                        try
                        {
                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            fs.Write(msgBytesByClient, 0, 1024);
                            fs.Flush();
                            fs.Close();
                            lbxServer.Items.Add("已接收到1024个字节的文件");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("写文件发生异常！" + ex.ToString());
                        }

                        //取出byte[]中的数据并生成string
                        String msg = Encoding.Default.GetString(msgBytesByClient, 1024, msgBytesByClient.Length - 1024);
                        lbxServer.Items.Add("客户端：" + msg);
                        nsServer.Flush();
                        ms.Dispose();

                        printText = msg;

                        //解压zp.wlt相片文件
                        DesImageFile();
                        //打印客户端数据
                        printMethod();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 打印客户端数据
        /// <summary>
        /// 打印客户端数据
        /// </summary>
        public void printMethod()
        {
            string msg = WriteImageFile();

            lock (locker)
            {
                lbxServer.Items.Add("入队列:" + msg);
                clientQueue.Enqueue(msg);		// 入队列
            }

            if (workerThread != null &&
                (workerThread.ThreadState & ThreadState.Suspended)
                != ThreadState.Running)
            {
                workerThread.Resume();		// 唤醒线程
            }
        }

        public String WriteImageFile()
        {
            string filepath = "";
            try
            {
                /**
                 *绘制背景图片
                 * 
                 */
                Image imgBack = Image.FromFile(Application.StartupPath + @"\IDCard.bmp");
                //create a Bitmap the Size of the original photograph
                Bitmap bmBack = new Bitmap(imgBack.Width, imgBack.Height);
                Graphics e = Graphics.FromImage(bmBack);
                //既然使用位图，就需要先画出图片，在画字
                e.DrawImage(imgBack, new Rectangle(0, 0, imgBack.Width, imgBack.Height));

                /**
                 * 
                 * 绘制头像图片
                 * */
                Image imgPhoto = Image.FromFile(Application.StartupPath + @"\zp.bmp");
                Bitmap bmPhoto = new Bitmap(imgPhoto);
                bmPhoto.MakeTransparent(bmPhoto.GetPixel(20, 20));

                //叠加
                e.DrawImage(bmPhoto, new Rectangle(200, 32, imgPhoto.Width, imgPhoto.Height));  //Set the detination Position
                String[] idCardInfo = printText.Split('|');
                idcard = idCardInfo[5];
                if (idCardInfo != null && idCardInfo.Length > 0)
                {
                    //姓名
                    e.DrawString(idCardInfo[0], new Font("宋体", 12), new System.Drawing.SolidBrush(Color.Black), 60, 32);
                    //性别
                    e.DrawString(idCardInfo[1], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 55);
                    //民族
                    e.DrawString(idCardInfo[2], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 55);
                    try
                    {
                        //出生日期 
                        String year = idCardInfo[3].Substring(0, 4);
                        String month = idCardInfo[3].Substring(5, 2);
                        String date = idCardInfo[3].Substring(8, 2);
                        e.DrawString(year, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 80);
                        e.DrawString(month, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 110, 80);
                        e.DrawString(date, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 140, 80);
                    }
                    catch
                    {
                    }
                    //住址
                    if (idCardInfo[4].Length > 11)
                    {
                        int rows = 0;
                        if (idCardInfo[4].Length % 11 == 0)
                        {
                            rows = idCardInfo[4].Length / 11;
                        }
                        else
                        {
                            rows = idCardInfo[4].Length / 11 + 1;
                        }
                        if (rows == 2)
                        {
                            String startStr = idCardInfo[4].Substring(0, 11);
                            String endStr = idCardInfo[4].Substring(11, idCardInfo[4].Length - 11);
                            e.DrawString(startStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                            e.DrawString(endStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 125);
                        }
                        if (rows == 3)
                        {
                            String startStr = idCardInfo[4].Substring(0, 11);
                            String middleStr = idCardInfo[4].Substring(11, 11);
                            String endStr = idCardInfo[4].Substring(22, idCardInfo[4].Length - 22);
                            e.DrawString(startStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                            e.DrawString(middleStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 125);
                            e.DrawString(endStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 142);
                        }
                    }
                    else
                    {
                        e.DrawString(idCardInfo[4], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                    }
                    //身份号码
                    e.DrawString(idCardInfo[5], new Font("宋体", 12, FontStyle.Bold), new System.Drawing.SolidBrush(Color.Black), 106, 170);
                    //签发机关
                    e.DrawString(idCardInfo[6], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 364);
                    //有效期限
                    e.DrawString(idCardInfo[7], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 389);
                }
                filepath = Application.StartupPath + @"\" + idcard + ".bmp";
                bmBack.Save(filepath, ImageFormat.Bmp);
                e.Dispose();
                bmBack.Dispose();
                imgBack.Dispose();
                return filepath;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return filepath;
        }

        void pdoc_BeginPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 开始打印...{1}", DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_EndPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 打印结束...{1}{1}", DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 这里的值不能是txtMessage.Text，而应该是由队列中取出的
            // 因为要打印时，txtMessgae的Text已经改变了，txtMessage和printer不同步
            string msg;
            lock (locker)
            {
                msg = clientQueue.Dequeue();	// 出队列
            }
            e.Graphics.DrawImage(Image.FromFile(msg), new Point(100, 100));
            lbxServer.Items.Add(msg);
        }
        #endregion

        #region 将zp.wlt文件解密成照片
        /// <summary>
        /// 解密zp.wlt相片文件
        /// </summary>
        /// <returns>解密结果</returns>
        public void DesImageFile()
        {
            try
            {
                String filePath = Application.StartupPath + @"\zp.wlt";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("zp.wlt文件不存在！");
                    return;
                }
                int returnValue = GetBmp(filePath, 1);
                String showMsg = "";
                switch (returnValue)
                {
                    case 0: showMsg = "调用sdtapi.dll错误！"; break;
                    case -1: showMsg = "照片解密错误！"; break;
                    case -2: showMsg = "wlt文件后缀错误！"; break;
                    case -3: showMsg = "wlt文件打开错误！"; break;
                    case -4: showMsg = "wlt文件格式错误！"; break;
                    case -5: showMsg = "软件未授权！"; break;
                    case -6: showMsg = "设备连接错误！"; break;
                    default: showMsg = ""; break;
                }
                if (showMsg != "")
                {
                    MessageBox.Show(showMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 向客户端发送数据
        private void ServerSendToClient(string msg)
        {
            try
            {
                if (nsServer != null)
                {
                    //获取要发送的数据
                    //String msg =this.txtServerData.Text;
                    //将string转成byte[]
                    Byte[] data = Encoding.Default.GetBytes(msg);
                    //向流中写数据发送到客户端
                    nsServer.Write(data, 0, data.Length);
                    //发送数据
                    nsServer.Flush();
                    frmServer_onDownLoadList(data.Length + "字节的数据已成功发送！");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("向客户端发送数据失败，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 解密相片文件
        /// <summary>
        /// 解密文件的SDK方法
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="intf"></param>
        /// <returns></returns>
        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);
        #endregion

        #region 程序加载
        private void frmServer_Load(object sender, EventArgs e)
        {
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            txtServerIP.Text = ipAddr.ToString();
            this.onDownLoadList += new dDownloadList(frmServer_onDownLoadList);
            this.onDownLoadList1 += new dDownloadList1(frmServer_onDownLoadList1);
        }
        #endregion


        //委托
        public delegate void dDownloadList(string msg);
        //事件
        public event dDownloadList onDownLoadList;

        public void frmServer_onDownLoadList(string msg)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new FeederManage.dDownloadList(frmServer_onDownLoadList), new object[] { msg });
            }
            else
            {
                lbxServer.Items.Add(msg);
                Application.DoEvents();
            }
        }

        //委托
        public delegate void dDownloadList1(string msg);
        //事件
        public event dDownloadList1 onDownLoadList1;

        public void frmServer_onDownLoadList1(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new FeederManage.dDownloadList1(frmServer_onDownLoadList1), new object[] { msg });
            }
            else
            {
                //lblFileSend.Text = msg;
                Application.DoEvents();
            }
        }
        public void dispose()
        {
            if (nsClient != null)
                nsClient.Close();
            if (client != null)
                client.Close();
        }
        /// <summary>
        /// 将listview的内容写入txt文本。
        /// </summary>
        private void ListViewToTxt()
        {
            if (lbxServer.Items.Count < 1)
                return;
            try
            {
                string str = string.Empty;
                long cols = lbxServer.Items.Count;
                foreach (ListViewItem lvi in lbxServer.Items)
                {
                    for (long i = 0; i < cols; i++)
                    {
                        str += lvi.SubItems[(Int32)i].Text + "\r\n";
                    }
                }
                FileStream fs = new FileStream(string.Format("{0}Socket.txt",
                System.AppDomain.CurrentDomain.BaseDirectory), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(str);
                sw.Close();
                fs.Close();
                lbxServer.Items.Clear();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        #endregion
        private void LoadData()
        {
            try
            {
                show_dgv_feederinfo(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfo()));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void show_dgv_feederinfo(DataTable dt)
        {
            this.dgv_feederinfo.Invoke(new EventHandler(delegate
            {
                dgv_feederinfo.DataSource = dt;
            }));
        }
        private void show_feederinfo(DataTable dt)
        {
            this.dgv_feederinfo.Invoke(new EventHandler(delegate
            {
                dt.DefaultView.Sort = "fixturesize desc";
                dgv_feederinfo.DataSource = dt.DefaultView.ToTable();
                foreach (DataRow dr in dt.Rows)
                {
                    this.cbx_fixtureid.Items.Add(dr["fixtureId"].ToString());
                }
            }));
        }

        private void FrmLoadData()
        {
            try
            {
                show_feederinfo(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfo()));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                SendPrgMsg(mLogMsgType.Error, ex.Message);
            }
        }
        private void FeederManage_Load(object sender, EventArgs e)
        {
            #region 服务端加载
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            txtServerIP.Text = ipAddr.ToString();
            this.onDownLoadList += new dDownloadList(frmServer_onDownLoadList);
            this.onDownLoadList1 += new dDownloadList1(frmServer_onDownLoadList1);
            #endregion


            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion
            this.cbx_fixtureid.Text = "";
            //    dsd = new delegateshowdata(FrmLoadData);
            //    dsd.BeginInvoke(null, null);
        }

        private void cb_fixtureid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cbx_fixtureid.Text.Trim()))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(
                    refWebtFixtureInfo.Instance.GetFixtureInfoByFixtureId(this.cbx_fixtureid.Text.Trim()));
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["fixtureId"].ToString() == this.cbx_fixtureid.Text.Trim())
                    {
                        this.mFrm.ShowPrgMsg("该Feeder编号已存在,请确认...", MainParent.MsgType.Normal);
                        SendPrgMsg(mLogMsgType.Normal, "该Feeder编号已存在,请确认...");
                        this.cbx_fixtureid.SelectAll();
                        this.cbx_fixtureid.Focus();
                        return;
                    }
                }
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                this.bt_update.Enabled = false;
                this.bt_delete.Enabled = false;
                if (string.IsNullOrEmpty(this.cbx_fixtureid.Text.Trim()))
                    throw new Exception("Feeder编号不能为空!");
                if (string.IsNullOrEmpty(this.txt_MANUFACTURER.Text.Trim()))
                    throw new Exception("请选择Feeder厂商!");
                if (string.IsNullOrEmpty(this.cbx_FIXTURESIZE.Text.Trim()))
                    throw new Exception("请选择Feeder型号!");
                if (string.IsNullOrEmpty(this.cb_fixturestate.SelectedItem.ToString()))
                    throw new Exception("请选择Feeder状态!");
                if (string.IsNullOrEmpty(this.txt_FIXTUREMAINTAINDATE.Text.Trim()))
                    throw new Exception("请设定保养的时间!");
                if (string.IsNullOrEmpty(this.cb_fixturetype.Text.Trim()))
                    throw new Exception("请选择设备类型!");

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureTypeByTypename(this.cb_fixturetype.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    fixturetype = dt.Rows[0]["typeId"].ToString();
                }
                else
                {
                    this.mFrm.ShowPrgMsg(string.Format("该类型名称[{0}]不存在!", this.cb_fixturetype.Text.Trim()), MainParent.MsgType.Warning);
                    SendPrgMsg(mLogMsgType.Warning, string.Format("该类型名称[{0}]不存在!", this.cb_fixturetype.Text.Trim()));
                    this.cb_fixturetype.SelectAll();
                    this.cb_fixturetype.Focus();
                    return;
                }
                //this.tb_fixtureid.Text = ReadDataByClient();                                       
                //string sRES = refWebtFixtureInfo.Instance.InsertFixtureInfo(new WebServices.tFixtureInfo.tFixtureInfo1()
                //   {
                //       FixtureId = this.cbx_fixtureid.Text.Trim(),
                //       FixtureName = "Feeder",
                //       FixtureType = fixturetype,
                //       FixtureState = state,
                //       FixtureBegingdate = txt_FIXTUREBEGINGDATE.Text,//Replace("/", ""),
                //       FixtureMaintaindate = txt_FIXTUREMAINTAINDATE.Text,//.Replace("/", ""),
                //       FixtureNote = this.tbt_FIXTURENOTE.Text.Trim(),
                //       Manufacturer = this.txt_MANUFACTURER.Text,
                //       FixtureSize = this.cbx_FIXTURESIZE.Text,
                //       Assetscode = "",
                //       Nameplate = ""
                //   });
                Dictionary<string, object> dic = new Dictionary<string, object>();           
                dic.Add("FIXTUREID", cbx_fixtureid.Text.Trim());
                dic.Add("FIXTURENAME", "Feeder");
                dic.Add("FIXTURETYPE", fixturetype);
                dic.Add("FIXTURESTATE", state);
                dic.Add("FIXTUREBEGINGDATE",txt_FIXTUREBEGINGDATE.Text );
                dic.Add("FIXTUREMAINTAINDATE", txt_FIXTUREMAINTAINDATE.Text);
                dic.Add("FIXTURENOTE", tbt_FIXTURENOTE.Text.Trim());
                dic.Add("MANUFACTURER",txt_MANUFACTURER.Text );
                dic.Add("FIXTURESIZE", cbx_FIXTURESIZE.Text);
               string sRES= refWebtFixtureInfo.Instance.InsertFixtureInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));


                //LoadData();
                this.mFrm.ShowPrgMsg(string.Format("添加[{0}]成功! [{1}]", this.cbx_fixtureid.Text.Trim(),sRES), MainParent.MsgType.Outgoing);
                SendPrgMsg(mLogMsgType.Incoming, string.Format("添加[{0}]成功! [{1}]", this.cbx_fixtureid.Text.Trim(), sRES));
                dsd = new delegateshowdata(LoadData);
                dsd.BeginInvoke(null, null);
                //ds = new delegateshowdata(ListViewToTxt);
                //ds.BeginInvoke(null, null);
                FrmBLL.publicfuntion.SaveTxtLog(System.IO.Directory.GetCurrentDirectory(), lbxServer);
                ClearInfo();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                SendPrgMsg(mLogMsgType.Error, ex.Message);
            }
        }
        private int state;
        private void cb_fixturestate_SelectedIndexChanged(object sender, EventArgs e)
        {
            state = this.cb_fixturestate.SelectedIndex;
        }
        private string[] arrFixtureSizeP = new string[] { "DOUBLE 8", "12/16", "24/32", "44/56", "Stick", "Tray" };
        private string[] arrFixtureSizeJ = new string[] { "8", "12", "16", "24", "32", "44", "Stick", "Tray" };
        private void cb_manufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stext = this.txt_MANUFACTURER.SelectedItem.ToString();///.SelectedText;
            switch (stext)
            {
                case "Panasonic":
                    this.cbx_FIXTURESIZE.Text = "";
                    this.cbx_FIXTURESIZE.Items.Clear();
                    this.cbx_FIXTURESIZE.Items.AddRange(arrFixtureSizeP);
                    break;
                case "Juki":
                    this.cbx_FIXTURESIZE.Text = "";
                    this.cbx_FIXTURESIZE.Items.Clear();
                    this.cbx_FIXTURESIZE.Items.AddRange(arrFixtureSizeJ);
                    break;
                default:
                    break;
            }
        }
        private void ClearInfo()
        {
            this.cbx_fixtureid.Text = "";
            this.txt_MANUFACTURER.Text = "";
            this.cbx_FIXTURESIZE.Text = "";
            this.cb_fixturestate.Text = "";
            this.cb_fixturetype.Text = "";
            this.tbt_FIXTURENOTE.Text = "";
            //this.tb_begingdate.Text = "";//DateTime.Now.ToString();
            this.txt_FIXTUREMAINTAINDATE.Text = "";// DateTime.Now.ToString();
            this.bt_save.Enabled = true;
            this.bt_delete.Enabled = true;
            this.bt_update.Enabled = true;
            this.bt_updateinfo.Enabled = true;
            this.bt_add.Enabled = true;
            this.bt_query.Enabled = true;
            this.txt_FIXTUREID.Enabled = true;
            this.cbx_fixtureid.Enabled = true;
            this.dtp_fixturemaintaindate.Enabled = true;

            this.txt_FIXTUREID.Text = "";
            this.cbx_FIXTUREITEM.Text = "";
            this.txt_FIXTURECONTEXT.Text = "";
            this.txt_FIXTURENOTE.Text = "";
            this.txt_nextmaintaindate.Text = "";
            this.txt_FIXTUREENDDATE.Text = "";
            this.txt_FIXTURESTARTDATE.Text = "";
            this.cbx_fixtureid.SelectedIndex = -1;
            this.cb_fixturestate.SelectedIndex = -1;
        }

        private void bti_query_Click(object sender, EventArgs e)
        {
            try
            {
                this.tabControl1.SelectedTabIndex = 0;
                if (!string.IsNullOrEmpty(this.tbi_feederid.Text.Trim()))
                {
                    show_dgv_feederinfo(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfoByFixtureId(this.tbi_feederid.Text.Trim())));
                }
                else
                {
                    dsd = new delegateshowdata(ShowData);
                    dsd.BeginInvoke(null, null);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void ShowData()
        {
            if (!string.IsNullOrEmpty(this.tbi_feederid.Text.Trim()))
                show_dgv_feederinfo(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfoByFixtureId(this.cbx_fixtureid.Text.Trim())));
            else
                show_dgv_feederinfo(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfo()));
        }

        private void bt_clearinfo_Click(object sender, EventArgs e)
        {
            ClearInfo();
        }


        private string fixturetype = "";
        private void cb_fixturetype_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cb_fixturetype.Text.Trim()))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureType());
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.cb_fixturetype.Text.Trim() == dr["typename"].ToString())
                    {
                        return;
                    }
                }
                if (MessageBox.Show(string.Format("该设备类型名称[{0}]不存在,确认添加?", this.cb_fixturetype.Text.Trim()), "提示",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    fixturetype = refWebExecuteSqlCmd.Instance.GetSeqBasics();
                    refWebtFixtureInfo.Instance.InsertFixtureType(fixturetype, this.cb_fixturetype.Text.Trim());
                }
                else
                {
                    this.cb_fixturetype.Focus();
                    this.cb_fixturetype.SelectAll();
                }
            }
        }

        private void dgv_feederinfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                string dtmaintain = dgv_feederinfo["fixturemaintaindate", e.RowIndex].Value.ToString();
                string feederfixturesize = dgv_feederinfo["fixturesize", e.RowIndex].Value.ToString();
                string dtnow = DateTime.Now.ToString("yyyyMMdd");
                if (string.Compare(dtnow, dtmaintain) > 0)
                {
                    this.dgv_feederinfo.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;//.Red;
                }
                if (feederfixturesize == "NA")
                {
                    this.dgv_feederinfo.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dtp_fixturemaintaindate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dtp_fixturemaintaindate.Text))
            {
                if (string.Compare(this.txt_FIXTUREMAINTAINDATE.Text, this.txt_FIXTUREBEGINGDATE.Text) < 0)
                {
                    this.mFrm.ShowPrgMsg("保养时间早于Feeder启用时间!", MainParent.MsgType.Warning);
                    SendPrgMsg(mLogMsgType.Warning, "保养时间早于Feeder启用时间!");
                    this.dtp_fixturemaintaindate.Focus();
                    return;
                }
            }
        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            try
            {
                this.bt_delete.Enabled = false;
                if (string.IsNullOrEmpty(this.cbx_fixtureid.Text.Trim()))
                    throw new Exception("Feeder编号不能为空!");
                if (string.IsNullOrEmpty(this.txt_MANUFACTURER.Text.Trim()))
                    throw new Exception("请选择Feeder厂商!");
                if (string.IsNullOrEmpty(this.cbx_FIXTURESIZE.Text.Trim()))
                    throw new Exception("请选择Feeder型号!");
                if (string.IsNullOrEmpty(this.cb_fixturestate.SelectedItem.ToString()))
                    throw new Exception("请选择Feeder状态!");
                if (string.IsNullOrEmpty(this.txt_FIXTUREMAINTAINDATE.Text.Trim()))
                    throw new Exception("请设定保养的时间!");
                if (string.IsNullOrEmpty(this.cb_fixturetype.Text.Trim()))
                    throw new Exception("请选择设备类型!");

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureTypeByTypename(this.cb_fixturetype.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    fixturetype = dt.Rows[0]["typeId"].ToString();
                }
                else
                {
                    this.mFrm.ShowPrgMsg(string.Format("该类型名称[{0}]不存在!", this.cb_fixturetype.Text.Trim()), MainParent.MsgType.Warning);
                    SendPrgMsg(mLogMsgType.Warning, string.Format("该类型名称[{0}]不存在!", this.cb_fixturetype.Text.Trim()));
                    this.cb_fixturetype.SelectAll();
                    this.cb_fixturetype.Focus();
                    return;
                }
                //refWebtFixtureInfo.Instance.UpdateFixtureInfo(new WebServices.tFixtureInfo.tFixtureInfo1()
                //{
                //    FixtureId = this.cbx_fixtureid.Text,
                //    FixtureBegingdate = this.txt_FIXTUREBEGINGDATE.Text,
                //    FixtureMaintaindate = this.txt_FIXTUREMAINTAINDATE.Text,
                //    FixtureNote = this.tbt_FIXTURENOTE.Text.Trim(),
                //    FixtureSize = this.cbx_FIXTURESIZE.Text.Trim(),
                //    FixtureState = this.cb_fixturestate.SelectedIndex,
                //    FixtureType = fixturetype,
                //    Manufacturer = this.txt_MANUFACTURER.Text.Trim(),
                //});

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("FIXTUREID", txt_FIXTUREID.Text.Trim());
                dic.Add("FIXTUREBEGINGDATE", txt_FIXTUREBEGINGDATE.Text);
                dic.Add("FIXTUREMAINTAINDATE", this.txt_FIXTUREMAINTAINDATE.Text);
                dic.Add("FIXTURENOTE", tbt_FIXTURENOTE.Text.Trim());
                dic.Add("FIXTURESIZE", cbx_FIXTURESIZE.Text.Trim());
                dic.Add("fixturestate", cb_fixturestate.SelectedIndex);
                dic.Add("fixturetype", fixturetype);
                dic.Add("Manufacturer", txt_MANUFACTURER.Text.Trim());
                refWebtFixtureInfo.Instance.UpdateFixtureInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));

                this.mFrm.ShowPrgMsg(string.Format("修改[{0}]成功!", this.cbx_fixtureid.Text.Trim()), MainParent.MsgType.Outgoing);
                SendPrgMsg(mLogMsgType.Outgoing, string.Format("修改[{0}]成功!", this.cbx_fixtureid.Text.Trim()));
                dsd = new delegateshowdata(LoadData);
                dsd.BeginInvoke(null, null);
                ClearInfo();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                SendPrgMsg(mLogMsgType.Error, ex.Message);
            }
        }

        private void dgv_feederinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    this.cbx_fixtureid.Enabled = false;
                    this.dtp_fixturemaintaindate.Enabled = false;
                    this.bt_save.Enabled = false;
                    this.cbx_fixtureid.Text = dgv_feederinfo["fixtureId", e.RowIndex].Value.ToString();
                    this.txt_MANUFACTURER.Text = dgv_feederinfo["Manufacturer", e.RowIndex].Value.ToString();
                    this.cbx_FIXTURESIZE.Text = dgv_feederinfo["fixturesize", e.RowIndex].Value.ToString();
                    this.cb_fixturestate.Text = dgv_feederinfo["fixturestate", e.RowIndex].Value.ToString();
                    this.txt_FIXTUREBEGINGDATE.Text = dgv_feederinfo["fixturebegingdate", e.RowIndex].Value.ToString();
                    this.txt_FIXTUREMAINTAINDATE.Text = dgv_feederinfo["fixturemaintaindate", e.RowIndex].Value.ToString();
                    this.cb_fixturetype.Text = "Feeder";
                    this.tbt_FIXTURENOTE.Text = dgv_feederinfo["fixturenote", e.RowIndex].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                SendPrgMsg(mLogMsgType.Error, ex.Message);
            }
        }

        private void bt_clearinfo_Click_1(object sender, EventArgs e)
        {
            ClearInfo();
        }

        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            switch (this.tabControl1.SelectedTabIndex)
            {
                case 0:
                    bti_query_Click(null, null);
                    this.cbx_fixtureid.Focus();
                    this.cbx_fixtureid.Text = "";
                    //FeederManage_Load(null, null);
                    break;
                case 1:
                    FeederRegist_Load(null, null);
                    break;
                case 2:
                    this.cb_feedersize.Focus();
                    break;

            }
        }
        bool DeleteFeederData = false;
        private void bt_delete_Click(object sender, EventArgs e)
        {
            try
            {
                this.bt_update.Enabled = false;
                string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);

                if (string.IsNullOrEmpty(EmpData[0]))
                {
                    MessageBox.Show("用户名不能为空!!");
                    return;
                }
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo(EmpData[0], null, EmpData[1]));
                if (_dt == null || _dt.Rows.Count < 1 )
                {
                    MessageBox.Show("用户名或密码输入错误\n请重新输入!!");
                }
                else
                {
                    DeleteFeederData = true;
                    mFrm.ShowPrgMsg("输入权限正确！", MainParent.MsgType.Incoming);

                }
                if (DeleteFeederData == true)
                {
                    if (MessageBox.Show("确定要删除\r\n Feeder: " + this.cbx_fixtureid.Text.Trim().ToString() + "\r\n" + "制造商: " + this.txt_MANUFACTURER.Text.Trim().ToString() + "\r\n" + "Feeder型号: " + this.cbx_FIXTURESIZE.Text.Trim().ToString(),
                        "删除提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        refWebtFixtureInfo.Instance.DeleteFixtureInfoByFixtureId(this.cbx_fixtureid.Text.Trim().ToString());
                        this.mFrm.ShowPrgMsg("删除Feeder完成", MainParent.MsgType.Incoming);
                        DeleteFeederData = false;
                        dsd = new delegateshowdata(LoadData);
                        dsd.BeginInvoke(null, null);
                        ClearInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Incoming);
                SendPrgMsg(mLogMsgType.Error, ex.Message);
            }
        }
        #region feeder维护

        private void GetData()
        {
            showdgv_feederregist(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureRegist()));
        }
        private void showdgv_feederregist(DataTable dt)
        {
            this.dgv_feederregist.Invoke(new EventHandler(delegate
            {
                dgv_feederregist.DataSource = dt;
            }));
        }
        private void bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_FIXTUREID.Text.Trim()))
                    throw new Exception("Feeder编号不能为空!");
                if (string.IsNullOrEmpty(this.cbx_FIXTUREITEM.Text.Trim()))
                    throw new Exception("Feeder维护项目不能为空!");
                if (string.IsNullOrEmpty(this.txt_FIXTURECONTEXT.Text.Trim()))
                    throw new Exception("Feeder维护内容不能为空!");
                if (string.IsNullOrEmpty(this.txt_FIXTUREENDDATE.Text.Trim()))
                    throw new Exception("Feeder维护时间不能为空!");
                if (string.IsNullOrEmpty(this.txt_nextmaintaindate.Text))
                    throw new Exception("Feeder下次维护时间不能为空!");

                //refWebtFixtureInfo.Instance.InsertFixtureRegist(new WebServices.tFixtureInfo.tFixtureRegist()
                //{
                //    FixtureRegistId = refWebExecuteSqlCmd.Instance.GetSeqBasics(),
                //    FixtureId = this.txt_FIXTUREID.Text.Trim(),
                //    UserId = this.mFrm.gUserInfo.userId,
                //    FixtureItem = this.cbx_FIXTUREITEM.Text.Trim(),
                //    FixtureContext = this.txt_FIXTURECONTEXT.Text.Trim(),
                //    FixtureStartdate = this.txt_FIXTURESTARTDATE.Text,//.ToString().Replace("/",""),
                //    FixtureEnddate = this.txt_FIXTUREENDDATE.Text,//.Replace("/", ""),
                //    FixtureNote = this.txt_FIXTURENOTE.Text.Trim()
                //});
                Dictionary<string, object> dic = new Dictionary<string, object>();
                FrmBLL.publicfuntion.SerializeControl(dic,panelEx2);
                dic.Add("fixtureregistid", refWebExecuteSqlCmd.Instance.GetSeqBasics());
                dic.Add("USERID", this.mFrm.gUserInfo.userId);
                dic.Remove("nextmaintaindate".ToUpper());
               string _StrErr= refWebtFixtureInfo.Instance.InsertFixtureRegist(FrmBLL.ReleaseData.DictionaryToJson(dic));
               if (_StrErr != "OK")
               {
                   MessageBox.Show("InsertFixtureRegist发生异常:\r\n" + _StrErr);
                   return;
               }
                refWebtFixtureInfo.Instance.UpdateMaintaindate(this.txt_FIXTUREID.Text, this.txt_nextmaintaindate.Text);
                this.mFrm.ShowPrgMsg(string.Format("添加[{0}]维护信息成功!", this.txt_FIXTUREID.Text.Trim()), MainParent.MsgType.Outgoing);
                SendPrgMsg(mLogMsgType.Outgoing, string.Format("添加[{0}]维护信息成功!", this.txt_FIXTUREID.Text.Trim()));
                ClearInfo();
                dsd = new delegateshowdata(GetData);
                dsd.BeginInvoke(null, null);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                SendPrgMsg(mLogMsgType.Error, ex.Message);
            }
        }

        private void FeederRegist_Load(object sender, EventArgs e)
        {
            dsd = new delegateshowdata(GetData);
            dsd.BeginInvoke(null, null);
        }


        private void dtp_fixtureenddate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dtp_fixtureenddate.Text))
            {
                if (string.Compare(this.txt_FIXTUREENDDATE.Text, this.txt_FIXTURESTARTDATE.Text) < 0)
                {
                    this.mFrm.ShowPrgMsg("保养结束时间早于保养开始时间!", MainParent.MsgType.Warning);
                    SendPrgMsg(mLogMsgType.Warning, "保养结束时间早于保养开始时间!");
                    this.dtp_fixtureenddate.Focus();
                    this.dtp_fixtureenddate.Select();
                    return;
                }
                this.txt_nextmaintaindate.Text = Convert.ToDateTime(this.dtp_fixtureenddate.Value).AddMonths(3).ToString("yyyyMMdd");
            }
        }

        private void bt_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txt_FIXTUREID.Text.Trim()))
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureRegistByFixtureId(this.txt_FIXTUREID.Text.Trim()));
                    dgv_feederregist.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void tb_feederid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt_FIXTUREID.Text.Trim()))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfoByFixtureId(this.txt_FIXTUREID.Text.Trim()));
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.txt_FIXTUREID.Text.Trim() == dr["fixtureId"].ToString())
                    {
                        return;
                    }
                }
                this.mFrm.ShowPrgMsg("该Feeder编号不存在!请确认...", MainParent.MsgType.Warning);
                SendPrgMsg(mLogMsgType.Warning, "该Feeder编号不存在!请确认...");
                this.txt_FIXTUREID.SelectAll();
                this.txt_FIXTUREID.Focus();
            }
        }
        /// <summary>
        /// 添加行序列号
        /// </summary>
        private void dgv_feederregist_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgv_feederregist.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }
        private void bt_updateinfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.bt_query.Enabled = false;
                this.bt_updateinfo.Enabled = false;
                //refWebtFixtureInfo.Instance.UpdateFixtureRegist(new WebServices.tFixtureInfo.tFixtureRegist()
                //{
                //    FixtureId = this.txt_FIXTUREID.Text.Trim(),
                //    UserId = this.mFrm.gUserInfo.userId,
                //    FixtureItem = this.cbx_FIXTUREITEM.Text.Trim(),
                //    FixtureContext = this.txt_FIXTURECONTEXT.Text.Trim(),
                //    FixtureStartdate = this.txt_FIXTURESTARTDATE.Text,//.ToString().Replace("/",""),
                //    FixtureEnddate = this.txt_FIXTUREENDDATE.Text,//.Replace("/", ""),
                //    FixtureNote = this.txt_FIXTURENOTE.Text.Trim()
                //});
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("FIXTUREID",txt_FIXTUREID.Text.Trim());
                dic.Add("USERID",mFrm.gUserInfo.userId);
                dic.Add("FIXTUREITEM",cbx_FIXTUREITEM.Text.Trim());
                dic.Add("FIXTURECONTEXT",txt_FIXTURECONTEXT.Text.Trim());
                dic.Add("FIXTURESTARTDATE",txt_FIXTUREENDDATE.Text);
                dic.Add("FIXTUREENDDATE",txt_FIXTUREENDDATE.Text);
                dic.Add("FIXTURENOTE", txt_FIXTURENOTE.Text.Trim());
                refWebtFixtureInfo.Instance.UpdateFixtureRegist(FrmBLL.ReleaseData.DictionaryToJson(dic));
                refWebtFixtureInfo.Instance.UpdateMaintaindate(this.txt_FIXTUREID.Text, this.txt_nextmaintaindate.Text);
                this.mFrm.ShowPrgMsg(string.Format("修改[{0}]信息成功!", this.txt_FIXTUREID.Text.Trim()), MainParent.MsgType.Outgoing);
                SendPrgMsg(mLogMsgType.Outgoing, string.Format("修改[{0}]信息成功!", this.txt_FIXTUREID.Text.Trim()));
                ClearInfo();
                dsd = new delegateshowdata(GetData);
                dsd.BeginInvoke(null, null);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Incoming);
                SendPrgMsg(mLogMsgType.Incoming, ex.Message);
            }
        }
        private void dgv_feederregist_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    this.bt_add.Enabled = false;
                    this.bt_query.Enabled = false;
                    this.txt_FIXTUREID.Enabled = false;
                    this.txt_FIXTUREID.Text = dgv_feederregist["feedid", e.RowIndex].Value.ToString();
                    this.cbx_FIXTUREITEM.Text = dgv_feederregist["fixtureitem", e.RowIndex].Value.ToString();
                    this.txt_FIXTURECONTEXT.Text = dgv_feederregist["fixturecontext", e.RowIndex].Value.ToString();
                    this.txt_FIXTURENOTE.Text = dgv_feederregist["remark", e.RowIndex].Value.ToString();
                    this.txt_FIXTURESTARTDATE.Text = dgv_feederregist["fixturestartdate", e.RowIndex].Value.ToString();
                    this.txt_FIXTUREENDDATE.Text = dgv_feederregist["fixtureenddate", e.RowIndex].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Incoming);
                SendPrgMsg(mLogMsgType.Incoming, ex.Message);
            }
        }
        private void bt_clear_Click(object sender, EventArgs e)
        {
            ClearInfo();
        }
        #endregion
        #region 盘点
        private delegate void delegateshowfeederdate(string name, string value);
        delegateshowfeederdate dsfd;
        private void Showdgv_feedertotal(DataTable dt)
        {
            this.dgv_feedertotal.Invoke(new EventHandler(delegate
            {
                dgv_feedertotal.DataSource = dt;
            }));
        }
        private void ShowFeedertotalInfo(string feedersize, string manufacture)
        {
            try
            {
                Showdgv_feedertotal(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfoBySizeAndManu(feedersize, manufacture)));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void bt_select_Click(object sender, EventArgs e)
        {
            try
            {
                this.lb_count.Text = "";
                if (!string.IsNullOrEmpty(this.cb_manufacture.Text.Trim()))
                {
                    //dsfd = new delegateshowfeederdate(ShowFeedertotalInfo);
                    //dsfd.BeginInvoke(this.cb_feedersize.Text.Trim(), null, null);
                    Showdgv_feedertotal(FrmBLL.ReleaseData.arrByteToDataTable(refWebtFixtureInfo.Instance.GetFixtureInfoBySizeAndManu(this.cb_feedersize.Text.Trim(), this.cb_manufacture.Text.Trim())));
                    this.lb_count.Text = "Feeder总数:" + this.dgv_feedertotal.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                throw;
            }
        }
        private void bt_toexcel_Click(object sender, EventArgs e)
        {
            if (dgv_feedertotal.RowCount != 0)
            {
                FrmBLL.DataGridViewToExcel.DataToExcel(dgv_feedertotal);
            }
        }

        private void dgv_feedertotal_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                string dtmaintain = dgv_feedertotal["feedermaintaindate", e.RowIndex].Value.ToString();
                string dtnow = DateTime.Now.ToString("yyyyMMdd");
                if (string.Compare(dtnow, dtmaintain) > 0)
                {
                    this.dgv_feedertotal.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;//.Red;
                }

                Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                dgv_feedertotal.RowHeadersWidth - 4, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                           dgv_feedertotal.RowHeadersDefaultCellStyle.Font, rectangle,
                           dgv_feedertotal.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }
        private void cb_manufacture_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stext = this.cb_manufacture.SelectedItem.ToString();///.SelectedText;
            switch (stext)
            {
                case "Panasonic":
                    this.cb_feedersize.Text = "";
                    this.cb_feedersize.Items.Clear();
                    this.cb_feedersize.Items.AddRange(arrFixtureSizeP);
                    break;
                case "Juki":
                    this.cb_feedersize.Text = "";
                    this.cb_feedersize.Items.Clear();
                    this.cb_feedersize.Items.AddRange(arrFixtureSizeJ);
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void FeederManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            dispose();
        }

        private void dtp_begingdate_ValueChanged(object sender, EventArgs e)
        {
            txt_FIXTUREBEGINGDATE.Text = dtp_begingdate.Text;
        }

        private void dtp_fixturemaintaindate_ValueChanged(object sender, EventArgs e)
        {
            txt_FIXTUREMAINTAINDATE.Text = dtp_fixturemaintaindate.Text;
        }

        private void dtp_fixturestartdate_ValueChanged(object sender, EventArgs e)
        {
            txt_FIXTURESTARTDATE.Text = dtp_fixturestartdate.Text;
        }

        private void dtp_fixtureenddate_ValueChanged(object sender, EventArgs e)
        {
            txt_FIXTUREENDDATE.Text = dtp_fixtureenddate.Text;
        }



        private void dgv_feederinfo_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                dgv_feederinfo[e.ColumnIndex, e.RowIndex].ToolTipText = string.Format("当前累计有{0}行数据!", dgv_feederinfo.RowCount);
            }
        }

        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void SendPrgMsg(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }

        }
    }
}
