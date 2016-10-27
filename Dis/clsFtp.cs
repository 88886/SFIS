using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Xml;

namespace Dis
{
    internal class Ftp_Socket
    {
        #region 内部变量

        private string m_strRemoteHost;
        private int m_strRemotePort;
        private string m_strRemotePath;
        private string m_strRemoteUser;
        private string m_strRemotePass;
        private Boolean m_bConnected;

        //private Form1 m_frm = null;
        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string m_strMsg;
        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string m_strReply;
        /// <summary>
        /// 服务器返回的应答码
        /// </summary>
        private int m_iReplyCode;
        /// <summary>
        /// 进行控制连接的socket
        /// </summary>
        private Socket m_socketControl;
        /// <summary>
        /// 传输模式
        /// </summary>
        private TransferType m_trType;
        /// <summary>
        /// 传输模式:二进制类型、ASCII类型
        /// </summary>
        public enum TransferType
        {
            /// <summary>
            /// Binary
            /// </summary>
            Binary,
            /// <summary>
            /// ASCII
            /// </summary>
            ASCII
        };
        /// <summary>
        /// 接收和发送数据的缓冲区
        /// </summary>
        private static int m_BLOCK_SIZE = 3072;
        Byte[] m_buffer = new Byte[m_BLOCK_SIZE];
        /// <summary>
        /// 编码方式
        /// </summary>
        //Encoding ASCII = Encoding.Default;
        Encoding m_ASCII = Encoding.GetEncoding("gb2312");

        #endregion
        #region 内部函数
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="remotePath"></param>
        /// <param name="remoteUser"></param>
        /// <param name="remotePass"></param>
        /// <param name="remotePort"></param>
        public Ftp_Socket(string remoteHost, string remotePath, string remoteUser, string remotePass, int remotePort)
        {
            m_strRemoteHost = remoteHost;
            m_strRemotePath = remotePath;
            m_strRemoteUser = remoteUser;
            m_strRemotePass = remotePass;
            m_strRemotePort = remotePort;
            Connect();
        }

        #endregion

        #region 登陆
        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        public string RemoteHost
        {
            get
            {
                return m_strRemoteHost;
            }
            set
            {
                m_strRemoteHost = value;
            }
        }
        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public int RemotePort
        {
            get
            {
                return m_strRemotePort;
            }
            set
            {
                m_strRemotePort = value;
            }
        }
        /// <summary>
        /// 当前服务器目录
        /// </summary>
        public string RemotePath
        {
            get
            {
                return m_strRemotePath;
            }
            set
            {
                m_strRemotePath = value;
            }
        }
        /// <summary>
        /// 登录用户账号
        /// </summary>
        public string RemoteUser
        {
            set
            {
                m_strRemoteUser = value;
            }
        }
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string RemotePass
        {
            set
            {
                m_strRemotePass = value;
            }
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool Connected
        {
            get
            {
                return m_bConnected;
            }
        }
        #endregion

        #region 链接
        /// <summary>
        /// 建立连接 
        /// </summary>
        public void Connect()
        {
            m_socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(RemoteHost), m_strRemotePort);
            // 链接
            try
            {
                m_socketControl.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Couldn't connect to remote server");
            }

            // 获取应答码
            ReadReply();
            if (m_iReplyCode != 220)
            {
                DisConnect();
                throw new IOException(m_strReply.Substring(4));
            }

            // 登陆
            SendCommand("USER " + m_strRemoteUser);
            if (!(m_iReplyCode == 331 || m_iReplyCode == 230))
            {
                CloseSocketConnect();//关闭连接
                throw new IOException(m_strReply.Substring(4));
            }
            if (m_iReplyCode != 230)
            {
                SendCommand("PASS " + m_strRemotePass);
                if (!(m_iReplyCode == 230 || m_iReplyCode == 202))
                {
                    CloseSocketConnect();//关闭连接
                    throw new IOException(m_strReply.Substring(4));
                }
            }
            m_bConnected = true;

            // 切换到目录
            ChDir(m_strRemotePath);
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void DisConnect()
        {
            if (m_socketControl != null)
            {
                SendCommand("QUIT");
            }
            CloseSocketConnect();
        }
        #endregion

        #region 传输模式

        /// <summary>
        /// 设置传输模式
        /// </summary>
        /// <param name="ttType">传输模式</param>
        public void SetTransferType(TransferType ttType)
        {
            if (ttType == TransferType.Binary)
            {
                SendCommand("TYPE I");//binary类型传输
            }
            else
            {
                SendCommand("TYPE A");//ASCII类型传输
            }
            if (m_iReplyCode != 200)
            {
                throw new IOException(m_strReply.Substring(4));
            }
            else
            {
                m_trType = ttType;
            }
        }

        /// <summary>
        /// 获得传输模式
        /// </summary>
        /// <returns>传输模式</returns>
        public TransferType GetTransferType()
        {
            return m_trType;
        }

        #endregion

        #region 文件操作
        /// <summary>
        /// 获得所有文件及文件夹列表
        /// </summary>
        /// <param name="strMask">文件名的匹配字符串</param>
        /// <returns></returns>
        public string[] Dir(string strMask)
        {
            // 建立链接
            if (!m_bConnected)
            {
                Connect();
            }

            //建立进行数据连接的socket
            Socket socketData = CreateDataSocket();

            //传送命令
            SendCommand("NLST " + strMask);

            //分析应答代码
            if (!(m_iReplyCode == 150 || m_iReplyCode == 125 || m_iReplyCode == 226))
            {
                throw new IOException(m_strReply.Substring(4));
            }

            //获得结果
            m_strMsg = "";
            while (true)
            {
                int iBytes = socketData.Receive(m_buffer, m_buffer.Length, 0);
                m_strMsg += m_ASCII.GetString(m_buffer, 0, iBytes);
                if (iBytes < m_buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] strsFileList = m_strMsg.Split(seperator);
            socketData.Close();//数据socket关闭时也会有返回码
            if (m_iReplyCode != 226)
            {
                ReadReply();
                if (m_iReplyCode != 226)
                {
                    throw new IOException(m_strReply.Substring(4));
                }
            }
            return strsFileList;
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="strMask">文件类型(*.*)</param>
        /// <returns></returns>
        public List<string> FileList(string strMask)
        {
            // 建立链接
            if (!m_bConnected)
            {
                Connect();
            }

            //建立进行数据连接的socket
            Socket socketData = CreateDataSocket();
            //传送命令
            SendCommand("NLST " + strMask);

            Thread.Sleep(6000);
            //分析应答代码
            if (!(m_iReplyCode == 150 || m_iReplyCode == 125 || m_iReplyCode == 226))
            {
                throw new IOException(m_strReply.Substring(4));
            }

            //获得结果
            m_strMsg = "";
            while (true)
            {
                int iBytes = socketData.Receive(m_buffer, m_buffer.Length, 0);
                m_strMsg += m_ASCII.GetString(m_buffer, 0, iBytes);
                if (iBytes < m_buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] strsFileList = m_strMsg.Split(seperator);
            List<string> temp = new List<string>();
            foreach (string item in strsFileList)
            {
                if (!string.IsNullOrEmpty(item))
                    temp.Add(item.Split('\r')[0]);
            }
            socketData.Close();//数据socket关闭时也会有返回码
            if (m_iReplyCode != 226)
            {
                ReadReply();
                if (m_iReplyCode != 226)
                {
                    throw new IOException(m_strReply.Substring(4));
                }
            }
            //this.m_frm.ShowMsg(Form1.LogMsgType.Warning, "累计:" + temp.Count.ToString());
            return temp;// strsFileList;
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="strMask"></param>
        /// <returns></returns>
        public List<string> DirList(string strMask)
        {
            // 建立链接
            if (!m_bConnected)
            {
                Connect();
            }

            //建立进行数据连接的socket
            Socket socketData = CreateDataSocket();

            //传送命令
            // SendCommand("NLST " + strMask);
            SendCommand("List ");

            //分析应答代码
            if (!(m_iReplyCode == 150 || m_iReplyCode == 125 || m_iReplyCode == 226))
            {
                throw new IOException(m_strReply.Substring(4));
            }

            //获得结果
            m_strMsg = "";
            while (true)
            {
                int iBytes = socketData.Receive(m_buffer, m_buffer.Length, 0);
                m_strMsg += m_ASCII.GetString(m_buffer, 0, iBytes);
                if (iBytes < m_buffer.Length)
                {
                    break;
                }
            }
            List<string> temp = new List<string>();
            char[] seperator = { '\n' };
            string[] strsFileList = m_strMsg.Split(seperator);
            foreach (string str in m_strMsg.Split(seperator))
            {
                if (str.IndexOf("<DIR>") != -1 && str != null)
                    temp.Add(str.Split('>')[1]);
            }
            socketData.Close();//数据socket关闭时也会有返回码
            if (m_iReplyCode != 226)
            {
                ReadReply();
                if (m_iReplyCode != 226)
                {
                    throw new IOException(m_strReply.Substring(4));
                }
            }
            return temp;// strsFileList;
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string strFileName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            SendCommand("SIZE " + Path.GetFileName(strFileName));
            long lSize = 0;
            if (m_iReplyCode == 213)
            {
                lSize = Int64.Parse(m_strReply.Substring(4));
            }
            else
            {
                throw new IOException(m_strReply.Substring(4));
            }
            return lSize;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strFileName">待删除文件名</param>
        public void Delete(string strFileName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            SendCommand("DELE " + strFileName);
            if (m_iReplyCode != 250)
            {
                throw new IOException(m_strReply.Substring(4));
            }
        }

        /// <summary>
        /// 重命名(如果新文件名与已有文件重名,将覆盖已有文件)
        /// </summary>
        /// <param name="strOldFileName">旧文件名</param>
        /// <param name="strNewFileName">新文件名</param>
        public void Rename(string strOldFileName, string strNewFileName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            SendCommand("RNFR " + strOldFileName);
            if (m_iReplyCode != 350)
            {
                throw new IOException(m_strReply.Substring(4));
            }
            //  如果新文件名与原有文件重名,将覆盖原有文件
            SendCommand("RNTO " + strNewFileName);
            if (m_iReplyCode != 250)
            {
                throw new IOException(m_strReply.Substring(4));
            }
        }
        #endregion

        #region 上传和下载
        /// <summary>
        /// 下载一批文件
        /// </summary>
        /// <param name="strFileNameMask">文件名的匹配字符串</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        public void Get(string strFileNameMask, string strFolder)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            string[] strFiles = FileList(strFileNameMask).ToArray();
            foreach (string strFile in strFiles)
            {
                if (!string.IsNullOrEmpty(strFile)/*!strFile.Equals("")*/)//一般来说strFiles的最后一个元素可能是空字符串
                {
                    this.Get(strFile, strFolder, strFile);
                }
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            long _size = GetFileSize(strRemoteFileName);
            SetTransferType(TransferType.Binary);
            if (strLocalFileName.Equals(""))
            {
                strLocalFileName = strRemoteFileName;
            }
            if (!File.Exists(strLocalFileName))
            {
                Stream st = File.Create(strFolder + strLocalFileName);
                st.Close();
            }
            if (!Directory.Exists(strFolder))//判断文件夹是否存在否则创建
                Directory.CreateDirectory(strFolder);
            //可以优化:如果本地文件存在则比较本地文件与FTP文件的创建日期,更具创建日期来判断是否需要下载文件
            if (File.Exists(strFolder + "\\" + strLocalFileName))//判断本地文件是否存在否则删除
            {
                File.Delete(strFolder + "\\" + strLocalFileName);
            }
            //=========
            FileStream output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
            Socket socketData = CreateDataSocket();
            SendCommand("RETR " + strRemoteFileName);
            if (!(m_iReplyCode == 150 || m_iReplyCode == 125
             || m_iReplyCode == 226 || m_iReplyCode == 250))
            {
                throw new IOException(m_strReply.Substring(4));
            }
            long ib = 0;
            //this.m_frm.ShowMsg(Form1.LogMsgType.Warning, string.Format("{0}文件下载中..", strLocalFileName));
            while (true)
            {
                int iBytes = socketData.Receive(m_buffer, m_buffer.Length, 0);
                output.Write(m_buffer, 0, iBytes);
                if (iBytes <= 0)
                {
                    break;
                }
                ib += iBytes;
                //this.m_frm.SetButtonText(string.Format("正在更新  累计[{1}]KB：已完成[{0}]KB", ib / 1000, _size / 1000));
            }
            output.Close();
            //this.m_frm.ShowMsg(Form1.LogMsgType.Incoming, string.Format("{0}文件下载完成", strLocalFileName));
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(m_iReplyCode == 226 || m_iReplyCode == 250))
            {
                ReadReply();
                if (!(m_iReplyCode == 226 || m_iReplyCode == 250))
                {
                    throw new IOException(m_strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 从FTP服务器上获取指定的文件(文本格式)
        /// </summary>
        /// <param name="strRemoteFileName"></param>
        /// <returns>返回文件内容</returns>
        public string GetOneFile(string strRemoteFileName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            long _size = GetFileSize(strRemoteFileName);
            SetTransferType(TransferType.Binary);
            System.IO.MemoryStream memorystream = new System.IO.MemoryStream();
            Socket socketData = CreateDataSocket();
            SendCommand("RETR " + strRemoteFileName);
            if (!(m_iReplyCode == 150 || m_iReplyCode == 125
             || m_iReplyCode == 226 || m_iReplyCode == 250))
            {
                throw new IOException(m_strReply.Substring(4));
            }
            while (true)
            {
                int iBytes = socketData.Receive(m_buffer, m_buffer.Length, 0);
                memorystream.Write(m_buffer, 0, iBytes);
                if (iBytes <= 0)
                {
                    break;
                }
            }
            string StrFileStream = System.Text.Encoding.Default.GetString(memorystream.ToArray());
            memorystream.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(m_iReplyCode == 226 || m_iReplyCode == 250))
            {
                ReadReply();
                if (!(m_iReplyCode == 226 || m_iReplyCode == 250))
                {
                    throw new IOException(m_strReply.Substring(4));
                }
            }

            return StrFileStream;
        }

        /// <summary>
        /// 上传一批文件
        /// </summary>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strFileNameMask">文件名匹配字符(可以包含*和?)</param>
        public void Put(string strFolder, string strFileNameMask)
        {
            string[] strFiles = Directory.GetFiles(strFolder, strFileNameMask);
            foreach (string strFile in strFiles)
            {
                //strFile是完整的文件名(包含路径)
                Put(strFile);
            }
        }

        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="strFileName">本地文件名</param>
        public void Put(string strFileName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            Socket socketData = CreateDataSocket();
            SendCommand("STOR " + Path.GetFileName(strFileName));
            if (!(m_iReplyCode == 125 || m_iReplyCode == 150))
            {
                throw new IOException(m_strReply.Substring(4));
            }
            FileStream input = new
             FileStream(strFileName, FileMode.Open);
            int iBytes = 0;
            while ((iBytes = input.Read(m_buffer, 0, m_buffer.Length)) > 0)
            {
                socketData.Send(m_buffer, iBytes, 0);
            }
            input.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(m_iReplyCode == 226 || m_iReplyCode == 250))
            {
                ReadReply();
                if (!(m_iReplyCode == 226 || m_iReplyCode == 250))
                {
                    throw new IOException(m_strReply.Substring(4));
                }
            }
        }

        #endregion

        #region 目录操作
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void MkDir(string strDirName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            SendCommand("MKD " + strDirName);
            if (m_iReplyCode != 257)
            {
                throw new IOException(m_strReply.Substring(4));
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void RmDir(string strDirName)
        {
            if (!m_bConnected)
            {
                Connect();
            }
            SendCommand("RMD " + strDirName);
            if (m_iReplyCode != 250)
            {
                throw new IOException(m_strReply.Substring(4));
            }
        }

        /// <summary>
        /// 改变目录
        /// </summary>
        /// <param name="strDirName">新的工作目录名</param>
        public void ChDir(string strDirName)
        {
            if (strDirName.Equals(".") || strDirName.Equals(""))
            {
                return;
            }
            if (!m_bConnected)
            {
                Connect();
            }
            SendCommand("CWD " + strDirName);
            if (m_iReplyCode != 250)
            {
                throw new IOException(m_strReply.Substring(4));
            }
            this.m_strRemotePath = strDirName;
        }

        #endregion

        /// <summary>
        /// 将一行应答字符串记录在strReply和strMsg
        /// 应答码记录在iReplyCode
        /// </summary>
        private void ReadReply()
        {
            m_strMsg = "";
            m_strReply = ReadLine();
            m_iReplyCode = Int32.Parse(m_strReply.Substring(0, 3));
        }

        /// <summary>
        /// 建立进行数据连接的socket
        /// </summary>
        /// <returns>数据连接socket</returns>
        private Socket CreateDataSocket()
        {
            SendCommand("PASV");
            if (m_iReplyCode != 227)
            {
                throw new IOException(m_strReply.Substring(4));
            }
            int index1 = m_strReply.IndexOf('(');
            int index2 = m_strReply.IndexOf(')');
            string ipData =
             m_strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " +
                     m_strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " +
                         m_strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." +
             parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new
             Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new
             IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server");
            }
            return s;
        }

        /// <summary>
        /// 关闭socket连接(用于登录以前)
        /// </summary>
        private void CloseSocketConnect()
        {
            if (m_socketControl != null)
            {
                m_socketControl.Close();
                m_socketControl = null;
            }
            m_bConnected = false;
        }

        /// <summary>
        /// 读取Socket返回的所有字符串
        /// </summary>
        /// <returns>包含应答码的字符串行</returns>
        private string ReadLine()
        {
            while (true)
            {
                int iBytes = m_socketControl.Receive(m_buffer, m_buffer.Length, 0);
                m_strMsg += m_ASCII.GetString(m_buffer, 0, iBytes);
                if (iBytes < m_buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] mess = m_strMsg.Split(seperator);
            if (m_strMsg.Length > 2)
            {
                m_strMsg = mess[mess.Length - 2];
                //seperator[0]是10,换行符是由13和0组成的,分隔后10后面虽没有字符串,
                //但也会分配为空字符串给后面(也是最后一个)字符串数组,
                //所以最后一个mess是没用的空字符串
                //但为什么不直接取mess[0],因为只有最后一行字符串应答码与信息之间有空格
            }
            else
            {
                m_strMsg = mess[0];
            }
            if (!m_strMsg.Substring(3, 1).Equals(" "))//返回字符串正确的是以应答码(如220开头,后面接一空格,再接问候字符串)
            {
                return ReadLine();
            }
            return m_strMsg;
        }

        /// <summary>
        /// 发送命令并获取应答码和最后一行应答字符串
        /// </summary>
        /// <param name="strCommand">命令</param>
        private void SendCommand(string strCommand)
        {
            Byte[] cmdBytes = m_ASCII.GetBytes((strCommand + "\r\n").ToCharArray());
            m_socketControl.Send(cmdBytes, cmdBytes.Length, 0);
            ReadReply();
        }
        #endregion
    }

    public class Ftp_MyFtp
    {
        private string username = null;
        private string password = null;
        private string port = null;
        private string ftppath = null;
        private string ftphost = null;
        private string imagedir = null;
        //private string m_UPdateConfigIniFilePath = System.Windows.Forms.Application.StartupPath + :"\Updatecfg.ini";

        public string Imagedir
        {
            get { return imagedir; }
        }
        public string Username
        {
            get { return username; }
        }
        public string Password
        {
            get { return password; }
        }
        public string Port
        {
            get { return port; }
        }
        public string Ftppath
        {
            get { return ftppath; }
        }
        public string Ftphost
        {
            get { return ftphost; }
        }

        private Ftp_Socket userftpsocket;
        private bool PingTest(string ipaddress)
        {
            Ping p = new Ping();
            PingReply pr = p.Send(ipaddress, 1200);
            if ((pr.Status.ToString().IndexOf("Success") == -1) || (Convert.ToInt32(pr.RoundtripTime) > 250))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Ftp_MyFtp(string filepath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(string.Format(@"{0}\DllConfig.xml", filepath));

            this.username = ((XmlElement)xml.SelectSingleNode("AutoCreate").SelectSingleNode("FtpServer").SelectSingleNode("USER")).GetAttribute("Name").ToString();
            this.password = ((XmlElement)xml.SelectSingleNode("AutoCreate").SelectSingleNode("FtpServer").SelectSingleNode("PWD")).GetAttribute("Name").ToString();
            this.port = ((XmlElement)xml.SelectSingleNode("AutoCreate").SelectSingleNode("FtpServer").SelectSingleNode("PORT")).GetAttribute("Name").ToString();
            this.ftphost = ((XmlElement)xml.SelectSingleNode("AutoCreate").SelectSingleNode("FtpServer").SelectSingleNode("IPADD")).GetAttribute("Name").ToString();
            this.imagedir = ((XmlElement)xml.SelectSingleNode("AutoCreate").SelectSingleNode("FtpServer").SelectSingleNode("FILEDIR")).GetAttribute("Name").ToString();
            if (PingTest(ftphost))
            {
                userftpsocket = new Ftp_Socket(ftphost, imagedir, username, password, int.Parse(port));
            }
            else
            {
                throw new Exception("FTP服务器连接失败");
            }
        }
        /// <summary>
        /// 下载一个文件，返回字符串
        /// </summary>
        /// <param name="strRemoteFileName"></param>
        /// <returns></returns>
        public string GetOneFile(string strRemoteFileName)
        {
            return userftpsocket.GetOneFile(strRemoteFileName);
        }
        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="imagename"></param>
        public void PutImage(string imagename)
        {
            userftpsocket.Put(imagename);
        }
        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void DownloadFile(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            userftpsocket.Get(strRemoteFileName, strFolder, strLocalFileName);
        }
    }
}
