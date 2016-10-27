using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_ReceiveMoveStock : Office2007Form  //Form
    {
        public Frm_ReceiveMoveStock(MainParent frm)
        {
            InitializeComponent();
            this.mfrm = frm;
        }
        MainParent mfrm;
        System.Windows.Forms.Timer timerByServer = new System.Windows.Forms.Timer();
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Green, Blue, Black, Orange, Red }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        bool ChkEmp = false;
        bool ChkSTOREHOUSEID = false;
        bool ChkLOCID = false;
        string STOREHOUSEID = "";
        string LocID = "";
        string sstatus = "";
        int loctotal = 0;
        int qty_loctotal = 0;
        string EmpNo = "";
        string EmpPwd = "";
        string sCMD = "";
        public string MYGROUP = "";
        public string CraftName = "";
        private void Frm_ReceiveMoveStock_Load(object sender, EventArgs e)
        {
            try
            {

                #region 添加应用程序

                if (this.mfrm.gUserInfo.rolecaption == "系统开发员")
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
                this.mfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            this.tb_moveid.SelectAll();
            this.tb_moveid.Focus();

            this.Enabled = false;

            #region 获取本机IP地址
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

            #endregion
        }

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
        private int iFunction = 0;
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
                this.Invoke(new Frm_StockTOMove.dDownloadList(FrmStockIn_onDownLoadList), new object[] { msg });
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
                this.Invoke(new Frm_StockTOMove.dDownloadList1(FrmStockIn_onDownLoadList1), new object[] { msg });
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

        private static void RunFile(string dir, string localFileName, string thisappname)
        {
            try
            {
                if (File.Exists(Path.Combine(dir, localFileName)))
                {
                    Process myProcess = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = dir + localFileName;
                    psi.WorkingDirectory = dir;
                    psi.UseShellExecute = false;
                    psi.Arguments = thisappname;
                    psi.RedirectStandardError = true;
                    psi.CreateNoWindow = true;
                    // psi.RedirectStandardOutput = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    myProcess.StartInfo = psi;
                    myProcess.Start();
                    myProcess.WaitForExit(20);
                    myProcess.Close();
                    //ssssss
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowPrgMsg(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.Text = string.Empty;
                    rtbmsg.Font = new Font(rtbmsg.Font, FontStyle.Bold);

                }));
            }
            catch
            {
            }
        }

        private void bt_confirm_Click(object sender, EventArgs e)
        {
            this.rtbmsg.Text = "";
            if (string.IsNullOrEmpty(this.tb_moveid.Text.Trim()))
            {
                this.rtbmsg.Text = "请输入移库单号";
                return;
            }
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(
                         refWebtz_whs_move_store.Instance.Getstore_info(this.tb_moveid.Text.Trim()));
            if (dt.Rows.Count > 0)
            {
                listMoveID.Items.Clear();
                this.list_info.Items.Clear();
                list_QTY.Items.Clear();

                this.listMoveID.View = System.Windows.Forms.View.Details;
                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = dr[0].ToString();

                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(dr[i].ToString());
                    }
                    listMoveID.Items.Add(lvi);
                }

                DataTable dt_qty = FrmBLL.ReleaseData.arrByteToDataTable(refWebtz_whs_move_store.Instance.Getmove_store_qty(this.tb_moveid.Text.Trim()));
                foreach (DataRow dr_qty in dt_qty.Rows)
                {
                    ListViewItem lvi_qty = new ListViewItem();
                    this.list_QTY.View = System.Windows.Forms.View.Details;
                    lvi_qty.SubItems[0].Text = dr_qty[0].ToString();

                    for (int index = 1; index < dt_qty.Columns.Count; index++)
                    {
                        lvi_qty.SubItems.Add(dr_qty[index].ToString());
                    }
                    list_QTY.Items.Add(lvi_qty);
                }
            }
            else
            {
                listMoveID.Items.Clear();
                this.list_info.Items.Clear();
                MessageBox.Show("没有数据,请确认移库单号是否正确");
                this.rtbmsg.Text = "没有数据,请确认移库单号是否正确";
                return;
            }
            this.tb_moveid.Enabled = false;
            this.bt_confirm.Enabled = false;
        }

        private void bt_modify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_moveid.Text.Trim()))
            {
                this.bt_confirm.Enabled = true;
                this.tb_moveid.Enabled = true;
                this.tb_moveid.Focus();
                this.tb_moveid.SelectAll();
                this.rtbmsg.Text = "";
            }
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (!string.IsNullOrEmpty(this.tbInput.Text.Trim()) && (e.KeyCode == Keys.Enter))
            {
                string InputData = this.tbInput.Text.Trim();
                this.rtbmsg.Text = "";
                if (string.IsNullOrEmpty(this.tb_moveid.Text.Trim()))
                {
                    SendMsgToClient("ERROR: Please input move number ");
                    this.rtbmsg.Text = "请输入移库单号";
                    return;
                }

                #region 开始
                if (InputData == "UNDO")
                {
                    ChkEmp = false;
                    ChkSTOREHOUSEID = false;
                    ChkLOCID = false;
                    sCMD = "";
                    SendMsgToClient("UNDO OK,Please Input CMD ");
                    this.rtbmsg.Text = "UNDO OK,Please Input CMD?";
                    list_info.Items.Clear();
                    return;
                }
                #endregion

                #region  确认权限
                if (!ChkEmp)
                {
                    try
                    {
                        EmpNo = InputData.Split('-')[0];
                        EmpPwd = InputData.Split('-')[1];

                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetJurUserInfoByIdandpwd(EmpNo, EmpPwd));
                        if (dt.Rows.Count > 0)
                        {
                            Labempno.Text = EmpNo;
                            Labname.Text = dt.Rows[0][0].ToString();
                            ChkEmp = true;
                            SendMsgToClient("EMP OK!");
                            this.rtbmsg.Text = "EMP OK";
                        }
                        else
                        {
                            SendMsgToClient("ERROR: Emp Error");
                            this.rtbmsg.Text = "ERROR: Emp Error";
                        }
                    }
                    catch (Exception ex)
                    {
                        SendMsgToClient("ERROR: Emp Format Error->" + ex.Message);
                        this.rtbmsg.Text = "ERROR: Emp Format Error->" + ex.Message;
                        return;
                    }
                }
                #endregion
                else
                    #region  扫描结束指令
                    if (InputData == "END")
                    {
                        for (int j = 0; j < list_QTY.Items.Count; j++)
                        {
                            if ((list_QTY.Items[j].SubItems[2].Text != list_QTY.Items[j].SubItems[3].Text) && (list_QTY.Items[j].SubItems[3].Text.ToString() != "0"))
                            {
                                SendMsgToClient(" Move store unfinished ,please continue!");
                                this.rtbmsg.Text = "移库未完成，请继续！";
                                return;
                            }
                        }
                        this.bt_confirm.Enabled = true;
                        this.tb_moveid.Enabled = true;
                        this.tb_moveid.Text = "";
                        this.tb_moveid.Focus();
                        this.tb_moveid.SelectAll();
                        this.listMoveID.Items.Clear();
                        this.list_info.Items.Clear();
                        SendMsgToClient(" Move store complete!");
                        this.rtbmsg.Text = "移库完成！";
                    }
                    #endregion
                    else
                        #region 扫描仓库并检查是否在该移库范围内
                        if (!ChkSTOREHOUSEID )
                                {
                                    try
                                    {
                                        DataTable dt=FrmBLL.ReleaseData .arrByteToDataTable (refWebtz_whs_move_store .Instance .Getstorebymoveid(this.tb_moveid .Text.Trim (),InputData  ));
                                        if (dt.Rows.Count > 0)
                                        {
                                            if (STOREHOUSEID != InputData)
                                            {
                                                STOREHOUSEID = InputData;
                                                LocID = "";
                                            }

                                            if (STOREHOUSEID == "1015")
                                            {
                                                LocID = "NA";
                                                ChkLOCID = true;
                                            }
                                            ChkSTOREHOUSEID = true;
                                            SendMsgToClient("Store OK!");
                                            this.rtbmsg.Text = "Store ID OK";
                                        }
                                        else
                                        {
                                            SendMsgToClient("ERROR: Warehouse ID is beyond the scope of the move");
                                            this.rtbmsg.Text = "ERROR: 所刷仓库ID不在该移库范围内";
                                            return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SendMsgToClient("ERROR: Emp Format Error->" + ex.Message);
                                        this.rtbmsg.Text = "ERROR: Emp Format Error->" + ex.Message;
                                        return;
                                    }
                                }
                                #endregion
                            else
                                #region 扫描仓库的库位并检查库位是否存在且为空
                                if (!ChkLOCID)
                                {
                                    try
                                    {
                                        if (string.IsNullOrEmpty(STOREHOUSEID))
                                        {
                                            SendMsgToClient("Please input Warehouse ID!");
                                            this.rtbmsg.Text = "请先输入仓库ID";
                                            return;
                                        }
                                        #region 检查库位是否存在
                                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.checkLocIDbySHID(STOREHOUSEID, InputData));
                                        if (dt.Rows.Count > 0)
                                        {                                          
                                            loctotal=Convert .ToInt32 (dt.Rows [0][5].ToString() );
                                        }
                                        else
                                        {
                                            SendMsgToClient("ERROR: LocID does not exist in the warehouse ");
                                            this.rtbmsg.Text = "ERROR: 库位在该仓库中不存在";
                                            return;
                                        }
                                        #endregion

                                        #region 检查库位上是否有货物
                                        if (LocID != InputData)
                                        {
                                            DataTable dt_store = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.checkz_whs_trackingbyLocIDSHID(STOREHOUSEID, InputData));
                                            if (dt_store.Rows.Count > 0)
                                            {
                                                SendMsgToClient("ERROR: LocID  existing goods  ");
                                                this.rtbmsg.Text = "ERROR: 库位已经存在货物";
                                                return;
                                            }
                                            else
                                            {
                                                qty_loctotal = 0;
                                                LocID = InputData;
                                                ChkLOCID = true;
                                                SendMsgToClient("LocID OK!");
                                                this.rtbmsg.Text = "LocID OK";
                                            }
                                        }
                                        else
                                        {
                                            LocID = InputData;
                                            ChkLOCID = true;
                                            SendMsgToClient("LocID OK!");
                                            this.rtbmsg.Text = "LocID OK";
                                        }
                                        #endregion

                                    }
                                    catch (Exception ex)
                                    {
                                        SendMsgToClient("ERROR: Emp Format Error->" + ex.Message);
                                        this.rtbmsg.Text = "ERROR: Emp Format Error->" + ex.Message;
                                        return;
                                    }
                                }
                                #endregion
                                else
                                    {
                                        #region 检查是否重复

                                        if (!CheckDupData_Pallet(list_info, InputData))
                                        {
                                            SendMsgToClient("ERROR: " + InputData + " Duplicate !");
                                            this.rtbmsg.Text = "ERROR: " + InputData + " 重复 !";
                                            return;
                                        }

                                        #endregion

                                        #region 根据InputData获取数据库资料

                                        DataTable dt_info = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.Getstoreinfobypallet(InputData));
                                        if (dt_info == null || dt_info.Rows.Count != 1)
                                        {
                                            SendMsgToClient("ERROR: No Data ");
                                            this.rtbmsg.Text = "无资料，请确认刷入的栈板号码是否有效";
                                            return;
                                        }

                                        bool partnumber_scope = false;
                                        for (int i = 0; i < listMoveID.Items.Count; i++)
                                        {

                                            if ((listMoveID.Items[i].SubItems[1].Text == dt_info.Rows[0][1].ToString()) && (listMoveID.Items[i].SubItems[2].Text == dt_info.Rows[0][2].ToString()))
                                            {
                                                #region 检查所刷人Pallet的信息并获取数量
                                                if (listMoveID.Items[i].SubItems[4].Text != STOREHOUSEID)
                                                {
                                                    SendMsgToClient("ERROR:The warehouse is not consistent  ");
                                                    this.rtbmsg.Text = "该栈板所移的仓库和刷人的仓库不一致";
                                                    return;
                                                }
                                                #endregion

                                                #region 检查存储数量是否超出库位上限
                                                if ((qty_loctotal + Convert.ToInt32(dt_info.Rows[0][0].ToString())) < loctotal)
                                                {
                                                    partnumber_scope = true;
                                                    listMoveID.Items[i].SubItems[6].Text = dt_info.Rows[0][0].ToString();
                                                    qty_loctotal +=Convert .ToInt32 ( dt_info.Rows[0][0].ToString());
                                                    #region 如果移库单中的料号已经移库完，更新该移库的状态
                                                    for (int j = 0; j < list_QTY .Items .Count ; j++)
                                                    {
                                                        if (list_QTY.Items[j].SubItems[1].Text == dt_info.Rows[0][1].ToString())
                                                        {
                                                            list_QTY.Items[j].SubItems[3].Text =(Convert.ToInt32(list_QTY.Items[j].SubItems[3].Text) +Convert.ToInt32(dt_info.Rows[0][0].ToString())).ToString () ;
                                                            if (list_QTY.Items[j].SubItems[2].Text == list_QTY.Items[j].SubItems[3].Text)
                                                            {
                                                                refWebtz_whs_move_store.Instance.move_store_id_status(this.tb_moveid.Text.Trim(), list_QTY.Items[j].SubItems[1].Text, "C");
                                                                refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                                                                    {
                                                                        WOID = "NA",
                                                                        PARTNUMBER = "",
                                                                        PRODUCTNAME = "",
                                                                        LOTIN = "NA",
                                                                        LOTIN_QTY = 0,
                                                                        LOTOUT = tb_moveid.Text.Trim(),
                                                                        LOTOUT_QTY = 0,
                                                                        PLANT = "2100",
                                                                        MOVE_TYPE = "311",
                                                                        UPLOAD_FLAG = "Y"
                                                                    });
                                                                UpLoadMove(this.tb_moveid.Text.Trim());
                                                            }
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                else 
                                                {
                                                    SendMsgToClient("ERROR:Beyond the storage location limit   ");
                                                    this.rtbmsg.Text = "超出库位存储上限";
                                                    return;
                                                }
                                                #endregion

                                                #region 将所刷的数据信息展示到界面上

                                                DataSet ds = new DataSet();
                                                DataTable dt = new DataTable();

                                                dt.Columns.Add("PALLET_ID");
                                                dt.Columns.Add("PN_ID");
                                                dt.Columns.Add("OUT_ID");
                                                dt.Columns.Add("IN_ID");
                                                dt.Columns.Add("LOC_ID");

                                                DataRow NEWROW = dt.NewRow();
                                                NEWROW["PALLET_ID"] = dt_info.Rows[0][2].ToString();
                                                NEWROW["PN_ID"] = dt_info.Rows[0][1].ToString();
                                                NEWROW["OUT_ID"] = dt_info.Rows[0][3].ToString();
                                                NEWROW["IN_ID"] = STOREHOUSEID;
                                                NEWROW["LOC_ID"] = LocID;

                                                dt.Rows.Add(NEWROW);
                                                ds.Tables.Add(dt);
                                                ds.AcceptChanges();
                                                ListViewItem ivi = new ListViewItem();
                                                this.list_info.View = System.Windows.Forms.View.Details;
                                                ivi.SubItems[0].Text = ds.Tables[0].Rows[0]["PALLET_ID"].ToString();
                                                ivi.SubItems.Add(ds.Tables[0].Rows[0]["PN_ID"].ToString());
                                                ivi.SubItems.Add(ds.Tables[0].Rows[0]["OUT_ID"].ToString());
                                                ivi.SubItems.Add(ds.Tables[0].Rows[0]["IN_ID"].ToString());
                                                ivi.SubItems.Add(ds.Tables[0].Rows[0]["LOC_ID"].ToString());
                                                this.list_info.Items.Add(ivi);

                                                #endregion
                                            }
                                        }
                                        if (partnumber_scope == false)
                                        {
                                            SendMsgToClient("ERROR:Input data is beyond the scope of setting ");
                                            this.rtbmsg.Text = "刷入的序号所属料号不在设定移库料号范围内";
                                            return;
                                        }
                                        #endregion

                                        #region 上传系统

                                        SendMsgToClient("正在上传系统....");
                                        this.rtbmsg.Text = "正在上传系统....";
                                        string sMsg = "";
                                        if (STOREHOUSEID == "1015")
                                        {
                                            sstatus = "6";
                                        }
                                        else
                                        {
                                            sstatus = "1";
                                        }
                                        sMsg = refWebtWarehouseWipTracking.Instance.Updatez_whs_tracking_move_store(dt_info.Rows[0][2].ToString(), this.tb_moveid.Text.Trim(), STOREHOUSEID, LocID,sstatus );
                                        if (sMsg != "OK")
                                        {
                                            SendMsgToClient("ERROR: Execute Update" + sCMD + " To STOCK_MOVE Failed");
                                            this.rtbmsg.Text = "更新移库资料失败" + sMsg;
                                            return;
                                        }
                                        else
                                        {
                                            SendMsgToClient(" Receive Move store OK! Please input StorehouseID");
                                            this.rtbmsg.Text = "接受移库OK,请刷人仓库ID";
                                            ChkSTOREHOUSEID = false;
                                            ChkLOCID = false;
                                        }
                                        #endregion
                                    }


            }
        }

        private bool CheckDupData_Pallet(ListView lv, string data)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[1].Text == data)
                {
                    return false;
                }
            }
            return true;
        }

        #region 移库上抛
        /// <summary>
        /// 移库上抛
        /// </summary>
        private void UpLoadMove(string MOVE_STORE_ID)
        {          

          
            SendMsgToClient("UpLoad SAP..... ");
            this.rtbmsg.Text = "开始上传SAP.....";
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtz_whs_move_store.Instance.Get_Z_WHS_MOVE_STORE());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MOVE_STORE_ID"].ToString() == MOVE_STORE_ID)
                    {
                        UpLoadSAP_Move(dr["MOVE_STORE_ID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(),
                                            dr["MOVE_STORE"].ToString(), dr["IMMIGRATION_STORE"].ToString(), Convert.ToInt32(dr["QTY"].ToString()),
                                            dr["ROWID"].ToString(),null);
                    }
                }
            }
          //  ShowPrgMsg("上传SAP结束.....", mLogMsgType.Normal, rtb);
            SendMsgToClient("UpLoad END");
            this.rtbmsg.Text = "上传SAP结束.....";
           
        }
        private void UpLoadSAP_Move(string SFCLOT, string PartNo, string Product, string sSTGE_LOC, string sMOVE_STLOC, int QTY, string sRowid, RichTextBox rtb)
        {
           
            string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.WHS_MOVE_Z_RFC_AUFNR_MIGO(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
            {
                EMP_NO = mfrm.gUserInfo.userId,
                EMP_NAME = mfrm.gUserInfo.username,
                PartNumber = PartNo,
                STGE_LOC = sSTGE_LOC,
                QTY = QTY,
                MOVE_STLOC = sMOVE_STLOC
            }, "");
            if (LsMsg.Length < 4)
            {
               // ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error, rtb);
                SendMsgToClient(LsMsg[0].ToString());
                this.rtbmsg.Text = LsMsg[0].ToString();
            }
            else
            {
                string SAP_LOT = LsMsg[0].ToString();
                string SAP_TYPE = LsMsg[1].ToString();
                string SAP_E_ID = LsMsg[2].ToString();
                string SAP_E_NUM = LsMsg[3].ToString();
                string SAP_MSG = LsMsg[4].ToString();
                if (SAP_TYPE.ToUpper() == "S")
                {
                    string sRes = refWebtz_whs_move_store.Instance.Update_Z_WHS_MOVE_STORES(sRowid);
                  //  ShowPrgMsg(string.Format("上传SAP成功,SAP移库单号[{0}],料号[{1}],产品型号[{2}],数量[{3}],时间[{4}]", SAP_LOT, PartNo, Product, QTY.ToString(), DateTime.Now.ToString()), mLogMsgType.Normal, rtb);
                    SendMsgToClient(string.Format("UpLoad SAP OK,SAP_DN [{0}],PartNo[{1}],ProductName[{2}],Quantity[{3}],Time[{4}]", SAP_LOT, PartNo, Product, QTY.ToString(), DateTime.Now.ToString()));
                    this.rtbmsg.Text = string.Format("上传SAP成功,SAP移库单号[{0}],料号[{1}],产品型号[{2}],数量[{3}],时间[{4}]", SAP_LOT, PartNo, Product, QTY.ToString(), DateTime.Now.ToString());
                }
                else
                {

                    string sSAP_MSG = string.Format("SFIS_NO:{0},PartNo:{1},ProductName:{2},Quantity:{3},MSG:{4},Time[{5}]", SFCLOT, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
                    string sSAP_MSG1 = string.Format("SFIS移库单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", SFCLOT, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
                   // ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtb);
                    SendMsgToClient("SAP:" + sSAP_MSG);
                    this.rtbmsg.Text = sSAP_MSG1;                    

                    string SFCMSG = FrmBLL.publicfuntion.InserSystemLog("UPLOAD", "SAP", SAP_LOT, sSAP_MSG);
                  //  ShowPrgMsg("SFC Log:" + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtb);
                    SendMsgToClient("SFC Log:" + SFCMSG);
                    this.rtbmsg.Text = "SFC Log:" + SFCMSG;
                }

            }
        }
        #endregion

        private void Frm_ReceiveMoveStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region 如果移库单中的料号未移库完，提示不可关闭窗口
            for (int j = 0; j < list_QTY.Items.Count; j++)
            {
                if ((list_QTY.Items[j].SubItems[2].Text != list_QTY.Items[j].SubItems[3].Text) && (list_QTY.Items[j].SubItems[3].Text.ToString() != "0"))
                {
                    SendMsgToClient(" Move store unfinished ,please continue!");
                    this.rtbmsg.Text = "移库未完成，请继续！";
                    return;
                }
            }
            #endregion

        }

    }
}
