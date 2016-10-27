using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;


namespace 测试用例
{
    /// <summary>
    /// 通过modbus TCP读写PLC数据,
    /// 
    /// 设计:张东启,2011.4.9
    /// 2011.5.13更新,加入Application.DoEvents();以防连续读写时引起前台反应迟顿,同时让PLC有处理等待时间
    /// 
    /// 
    /// 
    /// </summary>

    public class PLCFunction
    {

        public static bool Connected = false;
        public static int trytimes = 0;
        //内部使用变量
        private static byte[] sendBuf = { 0, 0, 0, 0, 0, 06, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };  //共20个字节
        private static byte[] recBuf = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };  //共20个字节
        private static int hi, low, bytes, hi1, low1;
        private static Socket zdqSocket;
        private static string tiaoshi = "N";
        private static string plcAddress = "127.0.0.1";


        public static int ReadWord(int mwAddress)
        {
            int rtValue = -99;
            if (init_plc() == 0)
            {
                try
                {
                    hi = mwAddress / 256;
                    low = mwAddress - hi * 256;

                    sendBuf[7] = 3;
                    sendBuf[8] = (byte)hi;
                    sendBuf[9] = (byte)low;
                    sendBuf[10] = 0;
                    sendBuf[11] = 1;

                    //发送查询
                    zdqSocket.Send(sendBuf, 12, 0);
                    Application.DoEvents();
                    bytes = zdqSocket.Receive(recBuf, 11, 0);  //返回11个字节
                    if (bytes == 11)
                    {
                        hi = recBuf[9];
                        low = recBuf[10];
                        rtValue = hi * 256 + low;
                    }
                    trytimes = 0;
                }
                catch (Exception te)
                {
                    if (tiaoshi == "Y") MessageBox.Show(te.ToString());
                    trytimes++;
                }
            }
            return rtValue;
        }
        public static void WriteWord(int mwAddress, int mwValue)
        {
            if (init_plc() == 0)
            {
                try
                {
                    hi = mwAddress / 256;
                    low = mwAddress - hi * 256;
                    hi1 = mwValue / 256;
                    low1 = mwValue - hi * 256;
                    sendBuf[7] = 6;
                    sendBuf[8] = (byte)hi;
                    sendBuf[9] = (byte)low;
                    sendBuf[10] = (byte)hi1;
                    sendBuf[11] = (byte)low1;
                    zdqSocket.Send(sendBuf, 12, 0);
                    Application.DoEvents();
                    bytes = zdqSocket.Receive(recBuf, recBuf.Length, 0);  //写一个字返回几个?
                    trytimes = 0;
                }
                catch (Exception te)
                {
                    if (tiaoshi == "Y") MessageBox.Show(te.ToString());
                    trytimes++;
                }
            }
        }
        public static int GetMemory(int mwAddress)
        {
            int rtValue = -99;
            if (init_plc() == 0)
            {
                try
                {
                    hi = mwAddress / 256;
                    low = mwAddress - hi * 256;

                    sendBuf[7] = 2;
                    sendBuf[8] = (byte)hi;
                    sendBuf[9] = (byte)low;
                    sendBuf[10] = 0;
                    sendBuf[11] = 1;

                    //发送查询
                    zdqSocket.Send(sendBuf, 12, 0);
                    Application.DoEvents();
                    bytes = zdqSocket.Receive(recBuf, 10, 0);  //返回11个字节
                    if (bytes == 10)
                    {
                        hi = recBuf[9];
                        rtValue = hi;
                    }
                    trytimes = 0;
                }
                catch (Exception te)
                {
                    if (tiaoshi == "Y") MessageBox.Show(te.ToString());
                    trytimes++;
                }
            }
            return rtValue;
        }

        public static void SetMemory(int mwAddress, int mFlag)
        {
            if (init_plc() == 0)
            {
                //线圈置位与复位
                int mBit = 0;
                if (mFlag == 1) mBit = 255; else mBit = 0;
                try
                {
                    hi = mwAddress / 256;
                    low = mwAddress - hi * 256;
                    sendBuf[7] = 5;
                    sendBuf[8] = (byte)hi;
                    sendBuf[9] = (byte)low;
                    sendBuf[10] = (byte)mBit;
                    sendBuf[11] = 0;
                    zdqSocket.Send(sendBuf, 12, 0);
                    Application.DoEvents();
                    bytes = zdqSocket.Receive(recBuf, recBuf.Length, 0);
                    trytimes = 0;

                }
                catch (Exception te)
                {
                    if (tiaoshi == "Y") MessageBox.Show(te.ToString());
                    trytimes++;
                }
            }
        }


        private static int init_plc()
        {
            //初始化
            int zdqrt = 0;
            if (trytimes > 3)
            {
                //超过3次自动恢复连接
                if (zdqSocket.Connected == true) zdqSocket.Disconnect(false);
                zdqSocket = null;
                trytimes = 0;
            }
            try
            {
                if (zdqSocket == null)
                {
                    zdqSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    zdqSocket.ReceiveTimeout = 3000;// int.Parse(zdqpro.WriteOrReadINI("系统设置", "接收超时"));
                    plcAddress = "172.16.173.149";// zdqpro.WriteOrReadINI("系统设置", "PLC地址");

                }
                if (zdqSocket.Connected == false)
                {
                    //重新连接
                    tiaoshi = "Y";// zdqpro.WriteOrReadINI("系统设置", "PLC调试");
                    zdqSocket.Connect(plcAddress, 502);
                }
            }
            catch (Exception te)
            {
                if (tiaoshi == "Y") MessageBox.Show(te.ToString());
                trytimes++;
                zdqrt = -1;
            }
            Connected = zdqSocket.Connected;//是否已经连接
            Application.DoEvents();
            return zdqrt;
        }



        //读写配置文件功能代码
        public static string WriteOrReadINI(string rootNod, string valueNod)
        {

            //读取配置文件
            XmlDocument mydoc = new XmlDocument();
            mydoc.Load("zdqsys.ini");
            string str1 = rootNod + "/" + valueNod;
            string rt = mydoc.SelectSingleNode(str1).InnerText;
            return rt;
        }

        public static void WriteOrReadINI(string rootNod, string valueNod, string valueStr)
        {
            //写入配置文件
            XmlDocument mydoc = new XmlDocument();
            mydoc.Load("zdqsys.ini");
            string str1 = rootNod + "/" + valueNod;
            XmlNode mynod = mydoc.SelectSingleNode(str1);
            mynod.InnerText = valueStr;
            mydoc.Save("zdqsys.ini");
        }
    }

}
