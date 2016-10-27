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
using System.Collections.Generic;
using System.Threading;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tPalletInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPalletInfo : System.Web.Services.WebService
    {
        BLL.tPalletInfo _palletinfo = new BLL.tPalletInfo();
        BLL.ProPublicStoredproc PPS = new BLL.ProPublicStoredproc();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public  void InsertPalletInfo(string dicstring)
        {
            _palletinfo.InsertPalletInfo(MapListConverter.JsonToDictionary(dicstring));
        }

        [WebMethod]
        public byte[] GetPalletInfo(string dicstring)
        {
            return mlc.GetDataSetSurrogateZipBytes(_palletinfo.GetPalletInfo(MapListConverter.JsonToDictionary(dicstring)));
        }

        [WebMethod]
        public void UpdatePalletCloseFlag(string dicstring)
        {
            _palletinfo.UpdatePalletCloseFlag(dicstring);
        }
      
        [WebMethod]
        public bool CheckCartonOrPalletClosed(string PALLET, int PackType,out int flag)
        {
            return _palletinfo.CheckCartonOrPalletClosed(PALLET, PackType, out flag);
        }     

        //[WebMethod]
        //public string SP_TEST_STOCKIN(List<string> ListData, string MYGROUP, string EMP, string EC, string LINE, int Total)
        //{
        //    return _palletinfo.SP_TEST_STOCKIN(ListData, MYGROUP, EMP, EC, LINE, Total);
        //}
        //[WebMethod]
        //public void InsertPalletInfoDta(string PALT, string Carton,string woId)
        //{
        //    _palletinfo.InsertPalletInfoDta(PALT, Carton, woId);
        //}
        [WebMethod]
        public void InsertPalletInfohad(string PALT, string PartNo, string Model, string Ver, string woId)
        {
            _palletinfo.InsertPalletInfohad(PALT, PartNo, Model, Ver, woId);
        }
        //[WebMethod]
        //public  string GetProductName(string PartNumber)
        //{
        //    return _palletinfo.GetProductName(PartNumber);
        //}
        [WebMethod]
        public  string[] GetPalletInfohad(string Pallet)
        {
            return _palletinfo.GetPalletInfohad(Pallet);
        }

        /// <summary>
        /// 获取palletnumber信息
        /// </summary>
        /// <returns>facId,facname,address</returns>
        [WebMethod]
        public byte[] GettPalletInfo(string palletnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(_palletinfo.GettPalletInfo(palletnumber));

        }

     
        //[WebMethod]
        //public string UpdatePalletInfoPcMac(string dicstring)
        //{
        //    return _palletinfo.UpdatePalletInfoPcMac(dicstring);
        //}
        //[WebMethod]
        //public string CheckPalletCartonForPcName(string dicstring)
        //{
        //    return _palletinfo.CheckPalletCartonForPcName(dicstring);
        //}
        [WebMethod]
        public byte[] GetPalletNnmberInfoForCarton(string woId, string Pallet)
        {
            return mlc.GetDataSetSurrogateZipBytes(_palletinfo.GetPalletNnmberInfoForCarton(woId, Pallet));
        }
        [WebMethod]
        public byte[] GetPalletAndMpalletInfo(string PalletNo, int Flag)
        {
            return mlc.GetDataSetSurrogateZipBytes(_palletinfo.GetPalletAndMpalletInfo(PalletNo, Flag));
        }
        [WebMethod]
        public string InsertPalletAndMpalletInfo(string PALLET, string MPALLET, string CTN, string MCTN, int QTY, string woId, string Part)
        {
            return _palletinfo.InsertPalletAndMpalletInfo(PALLET,MPALLET,CTN,MCTN,QTY,woId,Part);
        }
        [WebMethod]
        public string Get_Carton_No(string woId, string LineCode)
        {
            return _palletinfo.Get_Carton_No(woId, LineCode);
        }
        [WebMethod]
        public byte[] Get_Pallet_Info_bywo(string woId, int packtype, int closeflag)
        {
            return mlc.GetDataSetSurrogateZipBytes(_palletinfo.Get_Pallet_Info_bywo(woId, packtype, closeflag));
        }
     

        

    }
}
