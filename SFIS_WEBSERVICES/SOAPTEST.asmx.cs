//using System;
//using System.Collections.Generic;
//using System.Web;
//using System.Web.Services;
//using System.IO;
//using System.IO.Compression;
//using System.Data;
//using System.Runtime.Serialization.Formatters.Binary;

//namespace TestWeserver
//{
//    /// <summary>
//    /// SOAPTEST 的摘要说明
//    /// </summary>
//    [WebService(Namespace = "http://tempuri.org/")]
//    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//    [System.ComponentModel.ToolboxItem(false)]
//    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
//    // [System.Web.Script.Services.ScriptService]
//    public class SOAPTEST : System.Web.Services.WebService
//    {

//        [WebMethod(Description = "直接返回 DataSet 对象。")]
//        public DataSet GetNorthwindDataSet()
//        {
//            DataSet ds = new DataSet();
//            ds.Tables.Add(BLL.BllMsSqllib.Instance.ExecuteDataTable("select * from tPartStorehousehad"));
//            return ds;
//        }

//        [WebMethod(Description = "返回 DataSet 对象用 Binary 序列化后的字节数组。")]
//        public byte[] GetDataSetBytes()
//        {
//            DataSet dataSet = GetNorthwindDataSet();
//            BinaryFormatter ser = new BinaryFormatter();
//            MemoryStream ms = new MemoryStream();
//            ser.Serialize(ms, dataSet);
//            byte[] buffer = ms.ToArray();
//            return buffer;
//        }

//        [WebMethod(Description = "返回 DataSetSurrogate 对象用 Binary 序列化后的字节数组。")]
//        public byte[] GetDataSetSurrogateBytes()
//        {
//            DataSet dataSet = GetNorthwindDataSet();
//            DataSetSurrogate dss = new DataSetSurrogate(dataSet);
//            BinaryFormatter ser = new BinaryFormatter();
//            MemoryStream ms = new MemoryStream();
//            ser.Serialize(ms, dss);
//            byte[] buffer = ms.ToArray();
//            return buffer;
//        }

//        [WebMethod(Description = "返回 DataSetSurrogate 对象用 Binary 序列化并 Zip 压缩后的字节数组。")]
//        public byte[] GetDataSetSurrogateZipBytes()
//        {
//            DataSet dataSet = GetNorthwindDataSet();
//            DataSetSurrogate dss = new DataSetSurrogate(dataSet);
//            BinaryFormatter ser = new BinaryFormatter();
//            MemoryStream ms = new MemoryStream();
//            ser.Serialize(ms, dss);
//            byte[] buffer = ms.ToArray();
//            byte[] zipBuffer = Compress(buffer);
//            return zipBuffer;
//        }

//        public byte[] Compress(byte[] data)
//        {
//            try
//            {
//                MemoryStream ms = new MemoryStream();
//                Stream zipStream = null;
//                zipStream = new GZipStream(ms, CompressionMode.Compress, true);
//                zipStream.Write(data, 0, data.Length);
//                zipStream.Close();
//                ms.Position = 0;
//                byte[] compressed_data = new byte[ms.Length];
//                ms.Read(compressed_data, 0, int.Parse(ms.Length.ToString()));
//                return compressed_data;
//            }
//            catch
//            {
//                return null;
//            }
//        }
//    }
//}
