using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Entity;
using System.IO.Compression;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

namespace TestWeserver
{
    /// <summary>
    /// OperateRam 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class OperateRam : System.Web.Services.WebService
    {
        //BLL.OperateRam ram = new BLL.OperateRam();

        [WebMethod]
        public string Inser_RamInfo(RamBaseinfo info)
        {
            return ram.Inser_RamInfo(info);
        }
        [WebMethod]
        public string Backup_Test(RamTestInfo info)
        {
            return ram.Backup_Test(info);
        }
        [WebMethod]
        public string Inser_service(RamServiceInfo info)
        {
            return ram.Inser_service(info);
        }
        [WebMethod]
        public byte[] Get_BadCase(string mac)
        {
            return GetDataSetSurrogateZipBytes(ram.Get_BadCase(mac));
        }
        [WebMethod]
        public string Inser_Pack(RamPackInfo info)
        {
            return ram.Inser_Pack(info);
        }
        [WebMethod]
        public byte[] checkUserID(string userId)
        {
            return GetDataSetSurrogateZipBytes(ram.checkUserID(userId));
        }
        //[WebMethod]
        //public byte[] GetMac_Oldsn(string sn)
        //{
        //    return GetDataSetSurrogateZipBytes(ram.GetMac_Oldsn(sn));
        //}
        [WebMethod]
        public byte[] Get_RamInfo()
        {
            return GetDataSetSurrogateZipBytes(ram.Get_RamInfo());
        }
        [WebMethod]
        public byte[] Get_TestInfo()
        {
            return GetDataSetSurrogateZipBytes(ram.Get_TestInfo());
        }
        [WebMethod]
        public byte[] Get_ServiceInfo()
        {
            return GetDataSetSurrogateZipBytes(ram.Get_ServiceInfo());
        }
        [WebMethod]
        public byte[] Get_PackInfo()
        {
            return GetDataSetSurrogateZipBytes(ram.Get_PackInfo());
        }
        [WebMethod]
        public byte[] Get_WorkIDInfo(string worid)
        {
            return GetDataSetSurrogateZipBytes(ram.Get_WorkIDInfo(worid));
        }
        [WebMethod]
        public string GetConnectStr()
        {
            return ram.GetConnectStr();
        }
        #region 添加压缩功能

        private byte[] GetDataSetSurrogateZipBytes(DataSet dataSet)
        {
            //DataSet dataSet = GetNorthwindDataSet();
            using (MemoryStream ms = new MemoryStream())
            {
                DataSetSurrogate dss = new DataSetSurrogate(dataSet);
                BinaryFormatter ser = new BinaryFormatter();
                ser.Serialize(ms, dss);
                byte[] buffer = ms.ToArray();
                byte[] zipBuffer = Compress(buffer);

                ms.Close();
                ms.Dispose();
                return zipBuffer;
            }
        }

        private byte[] Compress(byte[] data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream zipStream = null;
                    zipStream = new GZipStream(ms, CompressionMode.Compress, true);
                    zipStream.Write(data, 0, data.Length);
                    zipStream.Close();
                    ms.Position = 0;
                    byte[] compressed_data = new byte[ms.Length];
                    ms.Read(compressed_data, 0, int.Parse(ms.Length.ToString()));

                    ms.Close();
                    ms.Dispose();
                    return compressed_data;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
