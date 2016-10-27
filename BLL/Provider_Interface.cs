using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericProvider;
using GenericUtil;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace BLL
{
    public class Provider_Interface
    {
        public Provider_Interface()
        {
        }


        public byte[] GetData(string TableName,string DicString,string fieldlist )
        {         
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicString);
            return GetDataSetSurrogateZipBytes(dp.GetData(TableName, fieldlist, mst, out count));  
        }
        public string AddData(string TableName, string DicString)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicString);
                dp.AddData(TableName, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateData(string TableName, string DicString, List<string> TableKey)
        {
            try
            {

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicString);
                dp.UpdateData(TableName, TableKey.ToArray(), mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
