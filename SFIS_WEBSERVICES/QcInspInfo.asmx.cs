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
    /// QcInspInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class QcInspInfo : System.Web.Services.WebService
    {
      //  BLL.QcInspInfo _qcinspinfo = new BLL.QcInspInfo();
        /// <summary>
        /// 获取所有的检验批次信息
        /// </summary>
        /// <returns>facId,facname,address</returns>
        //[WebMethod]
        //public byte[] GetQcInspInfoAll()
        //{
        //    return GetDataSetSurrogateZipBytes( _qcinspinfo.GetQcInspInfoAll());
           
        //}

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="facinfo"></param>
        //[WebMethod]
        //public void InserQcInspInfo(Entity.QcInspInfoTable insp)
        //{
        //    _qcinspinfo.InsertQcInspInfo(insp);
        //}


        // [WebMethod]
        //  public byte[] GetEntityByLot( string lotnumber)
        // {
        //     return GetDataSetSurrogateZipBytes(_qcinspinfo.GetEntityByLot(lotnumber));
           
        // }

        // [WebMethod]
        // public byte[] GetCountByLot(string lotnumber)
        // {
        //     return GetDataSetSurrogateZipBytes(_qcinspinfo.GetCountByLot(lotnumber));

        // }

        //[WebMethod]
        // public byte[] GetErrorCode(string errorcode)
        // {
        //     return GetDataSetSurrogateZipBytes(_qcinspinfo.GetErrorCode(errorcode));
           
        // }
         
       
        ///// <summary>
        ///// 根据状态获取在检批次信息
        ///// </summary>
        ///// <returns></returns>
        ////[WebMethod]
        ////public byte[] GetEntityBystatus()
        ////{
        ////    return GetDataSetSurrogateZipBytes(BLL.QcInspInfo.GetEntityBystatus());
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
