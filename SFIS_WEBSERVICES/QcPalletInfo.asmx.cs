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
    /// QcPalletInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class QcPalletInfo : System.Web.Services.WebService
    {
      //  BLL.QcPalletInfo _qcpalletinfo = new BLL.QcPalletInfo();
        ///// <summary>
        ///// 获取所有的检验批次信息
        ///// </summary>
        ///// <returns>facId,facname,address</returns>
        //[WebMethod]
        //public byte[] GetQcpalletInfoByLotno(string lotno, int lotstatus )//Entity.QcPalletInfoTable pallet)
        //{
        //    //return GetDataSetSurrogateZipBytes(BLL.QcPalletInfo.GetQcpalletInfoByLotno(pallet));
        //   // return GetDataSetSurrogateZipBytes(_qcpalletinfo.GetQcpalletInfoByLotno(lotno, lotstatus));
           
        //}

        ///// <summary>
        ///// 根据刷入的esn 判断该esn是否在当前批次中
        ///// </summary>
        ///// <param name="lotno"></param>
        ///// <param name="lotstatus"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetEntityBylotno(string lotno, string esn) 
        //{
        //    return GetDataSetSurrogateZipBytes(_qcpalletinfo.GetEntityBylotno(lotno, esn));

        //}


        ///// <summary>
        ///// 判断刷入的栈板号是否在当前在检批次中已经存在：GetPalletExist
        ///// </summary>
        ///// <param name="lotno"></param>
        ///// <param name="lotstatus"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetPalletExist(string palletstring)
        //{
        //    return GetDataSetSurrogateZipBytes(_qcpalletinfo.GetPalletExist(palletstring));
        //}


        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="facinfo"></param>
        //[WebMethod]
        //public void InserQcPalletInfo(Entity.QcPalletInfoTable pallet)
        //{
        //    _qcpalletinfo.InserQcPalletInfo(pallet);
        //}

        ///// <summary>
        ///// CheckFQC
        ///// </summary>
        ///// <param name="dataSet"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] CheckFQC(string palletno,int packtype,string MYGROUP)
        //{
        //    return GetDataSetSurrogateZipBytes(_qcpalletinfo.CheckFQC(palletno, packtype, MYGROUP));
        //}

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
