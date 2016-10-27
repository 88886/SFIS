using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace TestWeserver
{
    /// <summary>
    /// tWorkFunctionInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWorkFunctionInfo : System.Web.Services.WebService
    {
        //BLL.tWorkFunctionInfo mWorkfunctioninfo = new BLL.tWorkFunctionInfo();
        [WebMethod]
        public  byte[] GetALLWorkFunctionInfo()
        {
            return GetDataSetSurrogateZipBytes(mWorkfunctioninfo.GetALLWorkFunctionInfo());
        }
        [WebMethod]
        public  void InsertToWorkFunctionInfo(Entity.tWorkFunctionInfoTable wfi)
        {
            mWorkfunctioninfo.InsertToWorkFunctionInfo(wfi);
        }
        [WebMethod]
        public  void UpdateWorkFunctionInfo(Entity.tWorkFunctionInfoTable wfi)
        {
            mWorkfunctioninfo.UpdateWorkFunctionInfo(wfi);
        }
        [WebMethod]
        public  void DeleteWorkFunctionInfo(string wfid)
        {
            mWorkfunctioninfo.DeleteWorkFunctionInfo(wfid);
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

        private byte[] GetDataSetZipBytes(DataSet dataSet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer ser = new XmlSerializer(typeof(DataSet));
                ser.Serialize(ms, dataSet);

                byte[] buffer = ms.ToArray();
                byte[] zipBuffer = Compress(buffer);

                ms.Close();
                ms.Dispose();
                return zipBuffer;
            }
        }
        #endregion
    }
}
