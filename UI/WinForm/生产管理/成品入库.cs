using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Drawing.Printing;
using System.Threading;
using System.Net;
using System.IO;
using RefWebService_BLL;
using FrmBLL;
using System.Xml;

namespace SFIS_V2
{
    public partial class FrmStockIn : Office2007Form //Form
    {
        public FrmStockIn(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }

        MainParent sMain;
        System.Diagnostics.Stopwatch ExeTime = new System.Diagnostics.Stopwatch();

        #region   Socket服务
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
      //  private int iFunction = 0;
        //存储文件跟数据的总共byte[]
        private byte[] totalBuffer;
        private PrintDocument pdoc;
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
            labListen.Text = "正在启动监听......";
            labListen.Update();
            try
            {
                Int32 port = int.Parse(txtServerPort.Text);
                IPAddress localAddr = IPAddress.Parse(this.txtServerIP.Text);
                FrmStockIn_onDownLoadList("正在创建" + txtServerIP.Text + "服务端TcpListener对象");
                //创建TcpListener监听对象
                listener = new TcpListener(localAddr, port);
                FrmStockIn_onDownLoadList("已成功创建TcpListener对象，端口号为：" + txtServerPort.Text);
                //启动对端口的连接请求监听
                listener.Start();
                //  this.button1.Enabled = true;
                this.btnStartServer.Enabled = false;
                txtServerIP.Enabled = false;
                txtServerPort.Enabled = false;
                //启动定时器
                //this.timerByServer.Enabled = true;
                Thread th = new Thread(new ThreadStart(ListenerServer));
                th.IsBackground = true;
                th.Start();
                labListen.Text = "正在监听......";
            }
            catch (FormatException fx)
            {
                MessageBox.Show("服务器IP地址或端口号不是有效数据！详细信息：" + fx.ToString(), "输入数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labListen.Text = "启动监听失败......";
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("IP地址或端口号为空！详细信息：" + ex.ToString(), "输入数据错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labListen.Text = "启动监听失败......";
            }
            catch (SocketException ex)
            {
                MessageBox.Show("TcpListener启动失败！详细信息：" + ex.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labListen.Text = "启动监听失败......";
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
                        this.FrmStockIn_onDownLoadList("接收来自" + client.Client.RemoteEndPoint + "请求！");
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
                            FrmStockIn_onDownLoadList("已接收" + readSize + "字节的数据");
                        }
                        FrmStockIn_onDownLoadList("服务端共发送" + totalSize + "字节的数据");
                        msgBytesByServer = new byte[totalSize];

                        // **** 这里，从ms中读取数据前，ms指针必须回零，不然会出错。****
                        ms.Position = 0;

                        //将ms临时流中保存的数据全部读出
                        int readAllSize = ms.Read(msgBytesByServer, 0, totalSize);
                        FrmStockIn_onDownLoadList("已接收到" + readAllSize + "字节的数据,字节数据的长度：" + msgBytesByServer.Length);
                        //将接收到的byte[]转成String
                        String serverMsg = Encoding.Default.GetString(msgBytesByServer);

                        // **** 在数组上调用ToString()得不到数据的
                        FrmStockIn_onDownLoadList("服务端消息：" + serverMsg.ToString());
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
                MessageBox.Show("操作无效！,详细信息：" + ex.ToString());
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
                FrmStockIn_onDownLoadList("创建" + server.Client.RemoteEndPoint + "客户端的网络工作流对象");
                while (true)
                {
                    if (nsServer == null)
                    {
                        Thread.CurrentThread.Abort();
                        this.FrmStockIn_onDownLoadList("销毁" + server.Client.RemoteEndPoint + "客户端的线程");
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
        public void ReadDataByClient(byte[] msgBytesByClient)
        {

            //将接收到的byte[]转成String
            String serverMsg = Encoding.Default.GetString(msgBytesByClient, 0, msgBytesByClient.Length);
            FrmStockIn_onDownLoadList("客户端说：" + serverMsg);
            tbInput.Invoke(new EventHandler(delegate
            {               
               tbInput.Text = serverMsg; 
            }));

            tbInput.Invoke(new EventHandler(delegate
            {
               // tbInput.Text = serverMsg;
                tbInput_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }));
           
          ///  tbInput_KeyDown(null, new KeyEventArgs(Keys.Enter));
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
                        FrmStockIn_onDownLoadList("正在接收客户端数据，请稍候……");
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
                            FrmStockIn_onDownLoadList("收到客户端发送" + totalSize + "字节的数据");
                            ReadDataByClient(msgByClient);
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
                                                FrmStockIn_onDownLoadList1("已接收：" + totalSize + " 字节");
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                            }
                                        }

                                        FrmStockIn_onDownLoadList("收到客户端发送" + totalSize + "字节的【" + filename + "】文件");
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
                        //   printMethod();
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
        private void SendMsgToClient(string SendMsg)
        {
            labfrmMsg.Text = SendMsg;
            try
            {
                if (nsServer != null)
                {
                    labfrmMsg.Text = SendMsg;
                    //获取要发送的数据
                    String msg = SendMsg;
                    //将string转成byte[]
                    Byte[] data = Encoding.Default.GetBytes(msg);
                    //向流中写数据发送到客户端
                    nsServer.Write(data, 0, data.Length);
                    //发送数据
                    nsServer.Flush();
                    FrmStockIn_onDownLoadList(data.Length + "字节的数据已成功发送！");

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



        //委托
        public delegate void dDownloadList(string msg);
        //事件
        public event dDownloadList onDownLoadList;

        public void FrmStockIn_onDownLoadList(string msg)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new FrmPallet.dDownloadList(FrmStockIn_onDownLoadList), new object[] { msg });
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

        public void FrmStockIn_onDownLoadList1(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new FrmPallet.dDownloadList1(FrmStockIn_onDownLoadList1), new object[] { msg });
            }
            else
            {
                lblFileSend.Text = msg;
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

        #endregion

        System.Windows.Forms.Timer timerByServer = new System.Windows.Forms.Timer();

        /// <summary>
        /// 进度条时间
        /// </summary>
        System.Windows.Forms.Timer Timeprogress = new System.Windows.Forms.Timer();

        public string LineName = "";
        public string CraftId = "";
        public string CraftName = "";
        public bool Line = false;
        public bool Craft = false;
        DataTable dtStock = null;
        /// <summary>
        /// 入库单据号
        /// </summary>
        string StockIn = "";
        private void FrmStockIn_Load(object sender, EventArgs e)
        {

            try
            {
                #region 添加应用程序
                if (this.sMain.gUserInfo.rolecaption == "系统开发员")
                {
                    List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                    FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                    FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                    {
                        progid = this.Name,
                        progname = this.Text,
                        progdesc = this.Text

                    }, lsfunls);
                }
                #endregion

            }
            catch (Exception ex)
            {
                this.sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

            this.Timeprogress.Interval = 100;
            this.Timeprogress.Enabled = false;
            this.Timeprogress.Tick += new EventHandler(Timeprogress_Tick);
            
            this.timerByServer.Interval = 100;
            this.timerByServer.Enabled = false;
            this.timerByServer.Tick += new EventHandler(timerByServer_Tick);

            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress[] ipAddr = ipHost.AddressList;
            foreach (IPAddress ip in ipAddr)
            {
                txtServerIP.Items.Add(ip.ToString());
            }
            if (txtServerIP.Items.Count > 1)
            {
                for (int i = 0; i < txtServerIP.Items.Count; i++)
                {
                    if (txtServerIP.Items[i].ToString().IndexOf("170.1") != -1)
                    {
                        txtServerIP.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                txtServerIP.SelectedIndex = 0;
            }
            this.onDownLoadList += new dDownloadList(FrmStockIn_onDownLoadList);
            this.onDownLoadList1 += new dDownloadList1(FrmStockIn_onDownLoadList1);
           // labemployee.Text = sMain.gUserInfo.username;

          //  MessageBox.Show(panelEx1);
            grouptray.Size = new Size(panelEx1.Width / 3, 110);
            groupCarton.Size = new Size(panelEx1.Width / 3, 110);
            groupPallet.Size = new Size(panelEx1.Width / 3, 110);

            XmlDocument doc = new XmlDocument();
            string XmlName = "DllConfig.xml";
            doc.Load(XmlName);
            LineName = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("STOCKIN").SelectSingleNode("LINE")).GetAttribute("Name").ToString();
            CraftName = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("STOCKIN").SelectSingleNode("CRFTNAME")).GetAttribute("Name").ToString();
            CraftId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("STOCKIN").SelectSingleNode("CRFTID")).GetAttribute("Name").ToString();

            LabConfig.Text = "线体: " + LineName + "  --  途程: " + CraftName;

            dgvtray.Rows.Add("Total", "0");
            dgvcarton.Rows.Add("Total", "0");
            dgvpallet.Rows.Add("Total", "0");
          //  dgvcarton.Rows.Add("CA12121001", "2");
         //   dgvcarton.Rows.Add("CA12121002", "2");

            LabConfig.Left = panelEx1.Width- btnselectcraft.Right;

        }
        string sCMD = "";
        string Conum = "";
        int PackFlag = 0;
        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            string InputData = tbInput.Text.Trim().ToUpper();
            if ((!string.IsNullOrEmpty(InputData)) && (e.KeyCode == Keys.Enter))
            {
               
                if (InputData == "NA")
                {
                    return;
                }

                if (StockIn=="")
                  GetStockInNo();

                if (string.IsNullOrEmpty(sCMD))
                {
                    if ((InputData == "TRAY") || (InputData == "CARTON") || (InputData == "PALLET"))
                    {
                        sCMD = InputData;
                        SendMsgToClient("CMD OK");
                    }
                    else
                    {
                        SendMsgToClient("ERROR: CMD ERROR");
                        return;
                    }
                }
                else
                    if (InputData == "UNDO")
                    {
                        sCMD = "";
                        SendMsgToClient("UNDO OK,Please Input CMD?");
                        ListEsn.Items.Clear();
                        dgvcarton.Rows.Clear();
                        dgvpallet.Rows.Clear();
                        dgvtray.Rows.Clear();
                        dgvtray.Rows.Add("Total", "0");
                        dgvcarton.Rows.Add("Total", "0");
                        dgvpallet.Rows.Add("Total", "0");
                        return;
                    }
                    else
                    if (InputData == "END")
                    {
                        #region 上传系统,批量过站
                        SendMsgToClient("System to upload, Please wait");
                        if (ListEsn.Items.Count==0)
                        {
                            SendMsgToClient("ERROR: No Data Upload Database" );
                            return;
                        }

                        if (sCMD == "TRAY")
                        {                   
                            UpdateStockInToWip(dgvtray, Conum);
                           // GetListString(dgvtray,out str);                          
                          
                        }
                        else
                            if (sCMD == "CARTON")
                            {                        
                                 UpdateStockInToWip(dgvcarton,Conum);
                           
                            }
                            else
                                if (sCMD == "PALLET")
                                {                                
                                     UpdateStockInToWip(dgvpallet,Conum);

                                }

                        #region 每片产品提交webservices处理
                        /*     progressBarX1.Maximum = ListEsn.Items.Count;
                        progressBarX1.Value = ListEsn.Items.Count;

                        for (int i = 0; i < ListEsn.Items.Count; i++)
                        {
                          
                            string RES = refWebtPalletInfo.Instance.SP_TEST_STOCKIN(ListEsn.Items[i].ToString(), 
                                CraftId, sMain.gUserInfo.userId + "-" + sMain.gUserInfo.pwd, "NA", LineName);
                            if (RES != "OK")
                            {
                                SendMsgToClient("ERROR: Call TEST_STOCKIN ERROR, " + RES);
                                return;
                            }
                            progressBarX1.Value = progressBarX1.Value - 1;                        
                         
                        }
                    */
                      
                        #endregion
                        Timeprogress.Enabled = true;
                        ExeTime.Reset();
                        ExeTime.Start();
                        List<string> AllEsn = new List<string>();
                        for (int i = 0; i < ListEsn.Items.Count; i++)
                        {
                            AllEsn.Add(ListEsn.Items[i].ToString());
                        }
                        string RES = refWebtPalletInfo.Instance.SP_TEST_STOCKIN(AllEsn.ToArray(),CraftId, sMain.gUserInfo.userId + "-" + sMain.gUserInfo.pwd, "NA", LineName, ListEsn.Items.Count);
                        Timeprogress.Enabled = false;
                        if (RES != "OK")
                        {
                            ExeTime.Stop();
                            SendMsgToClient("Error: Failed To Upload->"+RES);

                            StockIn = "";
                            ListEsn.Items.Clear();
                            dgvcarton.Rows.Clear();
                            dgvpallet.Rows.Clear();
                            dgvtray.Rows.Clear();
                            dgvtray.Rows.Add("Total", "0");
                            dgvcarton.Rows.Add("Total", "0");
                            dgvpallet.Rows.Add("Total", "0");
                        }
                        else
                        {
                            ExeTime.Stop();
                            PrintInventoryDocuments(StockIn);
                            StockIn = "";
                            ListEsn.Items.Clear();
                            SendMsgToClient(string.Format("UpLoad OK, A total of {0} sec", (ExeTime.ElapsedMilliseconds / 1000).ToString()));
                            dgvcarton.Rows.Clear();
                            dgvpallet.Rows.Clear();
                            dgvtray.Rows.Clear();
                            dgvtray.Rows.Add("Total", "0");
                            dgvcarton.Rows.Add("Total", "0");
                            dgvpallet.Rows.Add("Total", "0");
                        }
                        #endregion
                    }
                    else
                    {
                       
                        #region 刷入产品信息
                        if (sCMD == "TRAY")
                        {
                            PackFlag = 0;
                            Conum = "TrayNO";
                            if (!CheckDupData(dgvtray, InputData))
                            {
                                SendMsgToClient("ERROR: " + InputData + " Duplicate !");
                                return;
                            }
                        }
                        else
                        if (sCMD == "CARTON")
                        {
                            PackFlag = 1;
                            Conum = "cartonnumber";
                            if (!CheckDupData(dgvcarton, InputData))
                            {
                                SendMsgToClient("ERROR: " + InputData + " Duplicate !");
                                return;
                            }
                        }
                        else
                            if (sCMD == "PALLET")
                            {
                                PackFlag = 2;
                                Conum = "palletnumber";
                                if (!CheckDupData(dgvpallet, InputData))
                                {
                                    SendMsgToClient("ERROR: " + InputData + " Duplicate !");
                                    return;
                                }
                            }

                        #region 判定Carton是否关闭
                        int flag = 0;
                        if (!refWebtPalletInfo.Instance.CheckCartonOrPalletClosed(InputData, PackFlag, out flag))
                        {

                            if (flag == 1)
                                SendMsgToClient(string.Format("ERROR: NO {0} Data", sCMD));
                            else
                                if (flag == 2)
                                    SendMsgToClient(string.Format("ERROR: {0} No  Multiple Data",sCMD));
                                else
                                    if (flag == 3)
                                        SendMsgToClient(string.Format("ERROR: {0} Not Closed",sCMD));
                            return;
                        }
                        #endregion

                        DataTable dtwip= FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWip(Conum, InputData));
                        int CtnTotal = dtwip.Rows.Count;
                        if (dtwip.Rows.Count != 0)
                        {


                            #region 判定工单状态
                            string WoStatus = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByWo(dtwip.Rows[0]["工单"].ToString())).Rows[0]["wostate"].ToString();

                            if (WoStatus == "1")
                            {
                                SendMsgToClient("ERROR: WO Close"); //工单结束
                                return;
                            }
                            else
                                if (WoStatus == "2")
                                {
                                    SendMsgToClient("ERROR: WO Suspend "); //工单暂停
                                    return;
                                }
                                else
                                    if (WoStatus == "3")
                                    {
                                        SendMsgToClient("ERROR: WO Initialization"); //工单初始化
                                        return;
                                    }

                            #endregion

                            #region 判定途程
                            DataTable dt = dtwip.DefaultView.ToTable(true, "当前站位");
                            foreach (DataRow dr in dt.Rows)
                            {

                                DataTable dtesn = FrmBLL.publicfuntion.getNewTable(dtwip, string.Format("当前站位='{0}'", dr["当前站位"].ToString()));
                                string chkesn = dtesn.Rows[0][0].ToString();
                                string SnResult = CheckRoute(chkesn);
                                if (SnResult != "OK")
                                {
                                    SendMsgToClient("ERROR: " + SnResult);
                                    return;
                                }

                            }
                            #endregion

                            #region 判定仓库是否存在有资料,有则报异常(暂时不启用)
                            //foreach (DataRow dr in dtwip.Rows)
                            //{
                            //    if (!refWebtWarehouseWipTracking.Instance.CheckDataInWH(dr["唯一条码"].ToString()))
                            //    {
                            //        SendMsgToClient("ERROR: 仓库资料重复-->" + dr["唯一条码"].ToString());
                            //        return;
                            //    }

                            //}
                            #endregion


                            foreach (DataRow dr in dtwip.Rows)
                            {
                                ListEsn.Items.Add(dr["唯一条码"].ToString());
                            }
                            if (sCMD == "TRAY")
                            {
                                dgvtray.Rows.Add(InputData, dtwip.Rows.Count.ToString());
                                dgvtray.Rows[0].Cells[1].Value = ListEsn.Items.Count.ToString();
                            }
                            else
                                if (sCMD == "CARTON")
                                {
                                    dgvcarton.Rows.Add(InputData, dtwip.Rows.Count.ToString());
                                    dgvcarton.Rows[0].Cells[1].Value = ListEsn.Items.Count.ToString();
                                }
                                else
                                    if (sCMD == "PALLET")
                                    {
                                        dgvpallet.Rows.Add(InputData, dtwip.Rows.Count.ToString());
                                        dgvpallet.Rows[0].Cells[1].Value = ListEsn.Items.Count.ToString();
                                    }

                            
                            SendMsgToClient(InputData+"  OK!");
                        }
                        else
                        {
                            SendMsgToClient("ERROR: " + InputData + " No Data");
                            return;
                        }
                    
                        #endregion
                    }
            }
        }

        private void FrmStockIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            dispose();
        }

        private void FrmStockIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = System.Environment.CurrentDirectory;
            if (lbxServer.Items.Count != 0)
            {
                FrmBLL.publicfuntion.SaveTxtLog(path + "\\StockInLog", lbxServer);
            }

            XmlDocument doc = new XmlDocument();
            string XmlName = "DllConfig.xml";
            doc.Load(XmlName);

            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("STOCKIN").SelectSingleNode("LINE")).SetAttribute("Name", LineName);
            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("STOCKIN").SelectSingleNode("CRFTNAME")).SetAttribute("Name", CraftName);
            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("STOCKIN").SelectSingleNode("CRFTID")).SetAttribute("Name", CraftId);
            doc.Save(XmlName);
        }

        private string CheckRoute(string esn)
        {
            DataTable dtchk = new DataTable("mydt");
            dtchk.Columns.Add("Code");
            dtchk.Columns.Add("Param");
            dtchk.Rows.Add(":DATA", esn);
            dtchk.Rows.Add(":MYGROUP",CraftId);
            return refWebProPublicStoredproc.Instance.SP_PublicStoredproc("pro_CheckRoute", dtchk);
        }

        private void btnselectLine_Click(object sender, EventArgs e)
        {
                 
            Line = true;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtLineInfo.Instance.GetAllLineInfo());
            SelectData sd = new SelectData(this, dt);
            sd.ShowDialog();
            Line = false;
        }

        private void btnselectcraft_Click(object sender, EventArgs e)
        {
            Craft = true;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCraftInfo.Instance.GetAllCraftInfo());
            SelectData sd = new SelectData(this, dt);
            sd.ShowDialog();
            Craft = true;
        }


        private void UpdateStockInToWip(DataGridView DGV, string ColumnName)
        {           
            for (int i = 1; i < DGV.Rows.Count; i++)
            {
                refWebtWipTracking.Instance.UpdateWipStockInNumber(ColumnName, DGV.Rows[i].Cells[0].Value.ToString(), StockIn);               
           
            }
          
        }

        private void GetStockInNo()
        {
           StockIn = refWebProPublicStoredproc.Instance.GetStockInNumber();
        }

     

        private bool CheckDupData(DataGridView dgv, string data)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Cells[0].Value.ToString() == data)
                {
                    return false;
                }
            }
            return true;
        }

        #region 列印入库单据
        private void PrintStockInNo_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
           
            try
            {
                int ofLeft = 15;
                int ofTop = 50; //距顶
                int height = 20; //行距
                int ofWidth = 110;//水平间距
                int x = 3; //距顶,设置列高
                int xx = 0;
                int yy = ofTop * x - 10; //表头上下距离
                int Total = 0;
                //定义表头及格式


                e.Graphics.DrawString("万得凯实业有限公司", new Font("宋体", 18, FontStyle.Bold), Brushes.Black, ofLeft + 240, ofTop - 15);
                e.Graphics.DrawString("入库单", new Font("宋体", 18, FontStyle.Bold), Brushes.Black, ofLeft + 315, 70);
                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                    new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, 96);

                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                  new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, 125);

                e.Graphics.DrawString("入库单号:"+StockIn, new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 8, ofTop * 2 + 10);
                e.Graphics.DrawString("入库日期:" + DateTime.Now.ToString("yyyy.MM.dd HH:mm"), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 6 * ofWidth - 150, ofTop * 2 + 10);

                e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, yy);

                e.Graphics.DrawString("序号", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 10, yy);

                e.Graphics.DrawString("工单", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 1 * ofWidth - 70, yy);

                e.Graphics.DrawString("料号", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 2 * ofWidth - 90, yy);

                e.Graphics.DrawString("品名/规格", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 125, yy);

                e.Graphics.DrawString("单位", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 40, yy);

                e.Graphics.DrawString("数量", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 5 * ofWidth - 15, yy);

                e.Graphics.DrawString("实/收发数量", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 6 * ofWidth + 30 - 100, yy);

                e.Graphics.DrawString("仓库", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 7 * ofWidth - 80, yy);

                e.Graphics.DrawString("备注", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 8 * ofWidth - 140, yy);

                int zz = dtStock.Rows.Count / 8 + 1;
                for (int i = 0; i <= zz*7; i++)
                {
                    try
                    {
                    xx = (ofTop + (i + x + 3) * height) - 10;
                    string woId = dtStock.Rows[i][0].ToString();
                    e.Graphics.DrawString((i+1).ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 5, xx);
                    //工单
                    e.Graphics.DrawString(woId, new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, xx);
                    //料号
                    e.Graphics.DrawString(dtStock.Rows[i][1].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 110, xx);
                    //品名
                    e.Graphics.DrawString(dtStock.Rows[i][3].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 205, xx);

                    e.Graphics.DrawString("PC", new Font("宋体", 10, FontStyle.Regular), Brushes.Black, ofLeft + 485, xx);
                    //数量
                    e.Graphics.DrawString(dtStock.Rows[i][4].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 538, xx);

                    e.Graphics.DrawString("________", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx - 1);

                   // e.Graphics.DrawString(dtStock.Rows[i][2].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);
                    e.Graphics.DrawString("_____", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);
                   
                    e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 730, xx);

                    Total = Total + Convert.ToInt32(dtStock.Rows[i][4].ToString());
                    }
                  catch (Exception )
                    {

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 5, xx);
                        //工单
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, xx);
                        //料号
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 110, xx);
                        //品名
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 205, xx);

                        e.Graphics.DrawString("", new Font("宋体", 10, FontStyle.Regular), Brushes.Black, ofLeft + 485, xx);
                        //数量
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 538, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx - 1);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 730, xx);
                    }
                }

                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                           new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, xx + 30);

                e.Graphics.DrawString("合计:", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 40, xx + 50);
                e.Graphics.DrawString(Total.ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 100, xx + 50);
                e.Graphics.DrawString("生产部", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft, xx + 100);
                e.Graphics.DrawString("质量部", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 300, xx + 100);
                e.Graphics.DrawString("仓库", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx + 100);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 列印入库单据
        /// </summary>
        /// <param name="StockIn"></param>
        private void PrintInventoryDocuments(string StockIn)
        {
            dtStock = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetStockInPrint(StockIn));
            PrintStockInNo.Print();
            dtStock = null;
        }
      
        #endregion
 
        private void RePrintStockInNo_Click(object sender, EventArgs e)
        {
             StockIn = Input.InputQuery.ShowInputBox("输入入库单据号码", string.Empty).ToUpper();
            if (!string.IsNullOrEmpty(StockIn))
            {
                if (StockIn != "NA")
                {
                    PrintInventoryDocuments(StockIn);
                }
            }

        }

        private void ReUpLoadStock_Click(object sender, EventArgs e)
        {
        
            string StockNo = Input.InputQuery.ShowInputBox("请输入入库单据",string.Empty).ToUpper();
            if (!string.IsNullOrEmpty(StockNo))
            {
                if (StockNo != "NA")
                {
                    DataTable dtWipstock =FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWip("storenumber", StockNo));
                    if (dtWipstock !=null && dtWipstock.Rows.Count != 0)
                    {
                        ListEsn.Items.Clear();
                        foreach (DataRow dr in dtWipstock.Rows)
                        {
                            if (dr["下一站"].ToString() != "1008")
                            {
                                ListEsn.Items.Add(dr["唯一条码"].ToString());
                            }
                        }
                        if (ListEsn.Items.Count != 0)
                        {
                            #region
                            /*
                            progressBarX1.Maximum = ListEsn.Items.Count;
                            progressBarX1.Value = ListEsn.Items.Count;

                            for (int i = 0; i < ListEsn.Items.Count; i++)
                            {
         
                                string RES = refWebtPalletInfo.Instance.SP_TEST_STOCKIN(ListEsn.Items[i].ToString(), CraftId, sMain.gUserInfo.userId + "-" + sMain.gUserInfo.pwd, "NA", LineName);
                                if (RES != "OK")
                                {
                                    MessageBox.Show("ERROR: 执行TEST_STOCKIN错误, " + RES);
                                    return;
                                }
                                progressBarX1.Value = progressBarX1.Value - 1;
                            }*/
                            #endregion

                            List<string> AllEsn = new List<string>();
                            for (int i = 0; i < ListEsn.Items.Count; i++)
                            {
                                AllEsn.Add(ListEsn.Items[i].ToString());
                            }
                            string RES = refWebtPalletInfo.Instance.SP_TEST_STOCKIN(AllEsn.ToArray(), CraftId, sMain.gUserInfo.userId + "-" + sMain.gUserInfo.pwd, "NA", LineName, ListEsn.Items.Count);
                            if (RES != "OK")
                            {
                                SendMsgToClient("Error: Failed To Upload->" + RES);
                            }
                            else
                            {
                                PrintInventoryDocuments(StockNo);
                            }
                        }
                        else
                        {
                            MessageBox.Show("没有可以上传的资料,请确认!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有资料可以上传系统");
                    }
                }


            }
        }

        /// <summary>
        /// 进度条时间
        /// </summary>
        int bar = 0;
        private void Timeprogress_Tick(object sender, EventArgs e)
        {
            bar += 1;
            progressBarX1.Value = bar;
            progressBarX1.Update();
            if (progressBarX1.Value == progressBarX1.Maximum)
            {
                bar = 0;
            }
                
        }
      


      

    }
}
