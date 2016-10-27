using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;

namespace TestWeserver
{
    /// <summary>
    /// QcLotInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class QcLotInfo : System.Web.Services.WebService
    {
       // BLL.QcLotInfo _qclotinfo = new BLL.QcLotInfo();
        /// <summary>
        /// 获取所有的检验批次信息
        /// </summary>
        ///// <returns>facId,facname,address</returns>
        //[WebMethod]
        //public byte[] GetQcLotInfoAll(string lotno, int lotstatus)
        //{
        //    return GetDataSetSurrogateZipBytes(_qclotinfo.GetQcLotInfoAll(lotno, lotstatus));
           
        //}

        ////获取当日批次记录数
        //[WebMethod]
        //public byte[] GetQcLotCount()
        //{
        //    return GetDataSetSurrogateZipBytes(_qclotinfo.GetQcLotCount());

        //}

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="facinfo"></param>
        //[WebMethod]
        //public void InserQcLotInfo(Entity.QcLotInfoTable lot)
        //{
        //    _qclotinfo.InserQcLotInfo(lot);
        //}

        ///// <summary>
        ///// 更新批次信息
        ///// </summary>
        ///// <param name="facId"></param>
        ///// <param name="facinfo"></param>
        //[WebMethod]
        //public void updateQcLotInfo(string lotno, Entity.QcLotInfoTable lotinfo)
        //{
        //    _qclotinfo.UpdateQcLotInfo(lotno, lotinfo);
        //}

       
        ///// <summary>
        ///// 取得报表
        ///// </summary>
        ///// <param name="lotno"></param>

        //[WebMethod]
       
        //  public byte[] GetQcLotReport(string lotno)
        //{
        //    return GetDataSetSurrogateZipBytes(_qclotinfo.GetQcLotReport(lotno));
        //}

        //[WebMethod]
        // public byte[] GetQcLotDefect(string lotno)
        //{
        //    return GetDataSetSurrogateZipBytes(_qclotinfo.GetQcLotDefect(lotno));
        //}

        // [WebMethod]
        // public  byte[] QeDefect(string lotno,string wipstation ,int type)
        // {
        //    return GetDataSetSurrogateZipBytes(_qclotinfo.QeDefect(lotno,wipstation ,type));
        //}

        // [WebMethod]
        // public byte[] FQCLotReport(int qtype, string dt)
        // {
        //     return GetDataSetSurrogateZipBytes(_qclotinfo.FQCLotReport(qtype,dt));
        // }

        // [WebMethod]
        // public byte[] FQCTop3Report(int qtype, string sdt,string edt)
        // {
        //     return GetDataSetSurrogateZipBytes(_qclotinfo.FQCTop3Report(qtype, sdt,edt));
        // }


        // [WebMethod]
        // public byte[] FQCQSReport(int qtype, string dt)
        // {
        //     return GetDataSetSurrogateZipBytes(_qclotinfo.FQCQSReport(qtype, dt));
        // }

        // [WebMethod]
        // public byte[] FQCLotList(string partnumber, string woid, string sdt, string edt, string errcode)
        // {
        //     return GetDataSetSurrogateZipBytes(_qclotinfo.FQCLotList(partnumber,woid,sdt,edt,errcode));
        // }

        ///// <summary>
        ///// 根据状态获取在检批次信息
        ///// </summary>
        ///// <returns></returns>
        ////[WebMethod]
        ////public byte[] GetEntityBystatus()
        ////{
        ////    return GetDataSetSurrogateZipBytes(BLL.QcLotInfo.GetEntityBystatus());
        ////}

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
