using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Data;

namespace TestWeserver
{
    /// <summary>
    /// QD_RD_NEW 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class QD_RD_NEW : System.Web.Services.WebService
    {

       //// BLL.QC_RD mdal = new BLL.QC_RD();

       // [WebMethod]
       // public byte[] getSerNum(string TableName, string Stime, string Etime, string Userd, string serNum, string dateB, string id)
       // {
       //    // return GetDataSetSurrogateZipBytes(mdal.getSerNum(TableName, Stime, Etime, Userd, serNum, dateB, id));
       // }
       // [WebMethod]
       // public string LinkPerNum(string SNum, string PhoneModle)
       // {
       //    // return mdal.LinkPerNum(SNum, PhoneModle);
       // }
       // [WebMethod]
       // public byte[] SelSerByPhone(string tableName, string PhoneModle)
       // {
       //     //return GetDataSetSurrogateZipBytes(mdal.SelSerByPhone(tableName, PhoneModle));
       // }
       // [WebMethod]
       // public byte[] GetSerName()
       // {
       //    // return GetDataSetSurrogateZipBytes(mdal.GetSerName());
       // }

       // [WebMethod]
       // public byte[] GetOneNum(string Batch)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.GetOneNum(Batch));
       // }

       // [WebMethod]
       // public byte[] GetUsedNum(string Batch)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.GetUsedNum(Batch));
       // }
       // [WebMethod]
       // public byte[] IsInser(string tableName, string SNum, string Enum, string Used)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.IsInser(tableName, SNum, Enum, Used));
       // }
       // [WebMethod]
       // public byte[] IsNew(string SNum, string Enum)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.IsNew(SNum, Enum));
       // }
       // [WebMethod]
       // public byte[] SperNum(string batch)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.SperNum(batch));
       // }
       // [WebMethod]
       // public byte[] MaxEnd(string Pkey)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.MaxEnd(Pkey));
       // }

       // [WebMethod]
       // public byte[] GetHeadPer(string batch)
       // {
       //     return GetDataSetSurrogateZipBytes(mdal.GetHeadPer(batch));
       // }

       // [WebMethod]
       // public void Inser(string tableName, string Userd, string SNum, string Enum, long count, string DateUse, string batch, string id)
       // {
       //     mdal.Inser(tableName, Userd, SNum, Enum, count, DateUse, batch, id);
       // }
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
