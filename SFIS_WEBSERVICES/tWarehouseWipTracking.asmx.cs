//using System;
//using System.Collections;
using System.ComponentModel;
//using System.Data;
//using System.Web;
//using System.Web.Services;
//using System.Web.Services.Protocols;

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tWarehouseWipTracking 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWarehouseWipTracking : System.Web.Services.WebService
    {
        BLL.tWarehouseWipTracking WipWh = new BLL.tWarehouseWipTracking();
        BLL.tWipKeyPart tWipKeyparts = new BLL.tWipKeyPart();
        MapListConverter mlc = new MapListConverter();
        
        //[WebMethod]
        //public bool CheckDataInWH(string ESN)
        //{
        //    return WipWh.CheckDataInWH(ESN);
        //}

        //[WebMethod]
        //public byte[] Getlotinfo(int status)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.Getlotinfo(status));

        //}


        //[WebMethod]
        //public byte[] GetlotinfoList(string lotin)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetlotinfoList(lotin));

        //}

        //[WebMethod]
        //public byte[] GetReturnList(string RecType, string RecCode)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetReturnList(RecType, RecCode));
        //}

        ////接收入库
        //[WebMethod]
        //public string StockReceive(string RecType, string RecStyle, string lotcode, string RecCode, string storeId, string locId, string userid)
        //{
        //    return WipWh.StockReceive(RecType, RecStyle, lotcode, RecCode, storeId, locId, userid);

        //}
        //[WebMethod]
        //public string Receiving_Storage(List<Entity.Z_WHS_SAP_BACKFLUSHTable> Lszhsb, string RecType, string storeId, string locId, string userid, string RecCode)
        //{
        //    return WipWh.Receiving_Storage(Lszhsb, RecType, storeId, locId, userid, RecCode);
        //}

        ////出库
        //[WebMethod]
        //public void StockOut(int RecType, string SAPCode, string partnumber, int SAPQty, string CustomerId, string RecStyle, string userid)
        //{
        //    WipWh.StockOut(RecType, SAPCode, partnumber, SAPQty, CustomerId, RecStyle, userid);

        //}

        //[WebMethod]

        //public byte[] StockOutEnd(string sapcode, int RecType, int Qtype)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.StockOutEnd(sapcode, RecType, Qtype));

        //}

        //[WebMethod]
        //public byte[] GetStockList(string partnumber, string cartonnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetStockList(partnumber, cartonnumber));
        //}

        //[WebMethod]
        //public byte[] GetCartonList(string cartonnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetCartonList(cartonnumber));
        //}

        //[WebMethod]
        //public byte[] GetPalletList(string palletnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetPalletList(palletnumber));
        //}

        //[WebMethod]
        ////UpdatetWhWipInfo(string esn, string reccode, int utype)
        //public void UpdatetWhWipInfo(string esn, string reccode, string storehouseId, string locId, int utype)
        //{
        //    WipWh.UpdatetWhWipInfo(esn, reccode, storehouseId, locId, utype);
        //}

        //[WebMethod]
        //public byte[] StockQuery(string sdt, string edt, string palletnumber, int qtype, int sumstate)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.StockQuery(sdt, edt, palletnumber, qtype, sumstate));
        //}
        [WebMethod]
        public byte[] ReworkWipQuery(string Colnum, string Data)
        {
            return mlc.GetDataSetSurrogateZipBytes(WipWh.ReworkWipQuery(Colnum, Data));
        }
        //[WebMethod]
        //public byte[] GetProductAllSerial(string flag, string value)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductAllSerial(flag, value));
        //}

        //[WebMethod]
        //public byte[] GetCartonbysn(string snval)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetCartonbysn(snval));
        //}
        //[WebMethod]
        //public byte[] GetlotinfoByLotin(string lotin)  //准备取消 20140516
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetlotinfoByLotin(lotin));
        //}
        [WebMethod]
        public byte[] Get_Warehouse_number_Info(string lotin)
        {
            return mlc.GetDataSetSurrogateZipBytes(WipWh.Get_Warehouse_number_Info(lotin));
        }
        //[WebMethod]
        //public byte[] GetProductInstockSerialInfo(string flag, string value)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductInstockSerialInfo(flag, value));
        //}
        [WebMethod]
        public byte[] GetProductAllInfo(string flag, string value)
        {
           // return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductAllInfo(flag, value));
            return mlc.GetDataSetSurrogateZipBytes(tWipKeyparts.GetWipKeyParts(flag, value));
        }
        //[WebMethod]
        //public byte[] GetProductNotLocId()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductNotLocId());
        //}
        //[WebMethod]
        //public void UpdateStoreLocId(string rectype, string reccode, string storehouseId, string locId)
        //{
        //    WipWh.UpdateStoreLocId(rectype, reccode, storehouseId, locId);
        //}
        //[WebMethod]
        //public byte[] GetProductInfo(Entity.tWarehouseWipTrackingTable wt)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductInfo(wt));
        //}
       
        ////[WebMethod]
        ////public string CancelSFClotcode(string SAPCode, string partnumber, string sfclotcode)
        ////{
        ////    return WipWh.CancelSFClotcode(SAPCode, partnumber, sfclotcode);
        ////}
        //[WebMethod]
        //public byte[] GetdgvStockList(string partnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetdgvStockList(partnumber));
        //}
        ////[WebMethod]
        ////public byte[] CheckStockList(string lotout, string flag)
        ////{
        ////    return GetDataSetSurrogateZipBytes(WipWh.CheckStockList(lotout, flag));
        ////}
        //[WebMethod]
        //public void DefaultDataPartition(string cartonnumber, string newcarton, string count)
        //{
        //    WipWh.DefaultDataPartition(cartonnumber, newcarton, count);
        //}
        //[WebMethod]
        //public byte[] GetCarinfoByCarton(string cartonnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetCarinfoByCarton(cartonnumber));
        //}
        //[WebMethod]
        //public byte[] GetSAPInfo(string saplotcode)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetSAPInfo(saplotcode));
        //}
        ////[WebMethod]
        ////public string InsertOutPutRecordList(List<Entity.tSapLodeInfo> lssap)
        ////{
        ////    return WipWh.InsertOutPutRecordList(lssap);
        ////}

        ////[WebMethod]
        ////public byte[] ProductOutPick(string serialval, string flag, out string Err)
        ////{
        ////    return GetDataSetSurrogateZipBytes(WipWh.ProductOutPick(serialval, flag, out Err));
        ////}    

        //[WebMethod]
        //public byte[] GetProductOutPickInfo(string keyvalue, string flag)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductOutPickInfo(keyvalue, flag));
        //}
        //[WebMethod]
        //public byte[] GetProductInfoBySN(string snval, string snvalend,string flag)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetProductInfoBySN(snval, snvalend, flag));
        //}

        ////[WebMethod]
        ////public string ProductOut_1(List<Entity.tWarehouseWipTrackingTable> wwip)
        ////{
        ////    return WipWh.ProductOut_1(wwip);
        ////}
        //[WebMethod]
        //public byte[] CountProductOutQTY(string sapcode)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.CountProductOutQTY(sapcode));
        //}
        //[WebMethod]
        //public string UpdatetOutputlotrecordNew(string sapcode)
        //{
        //    return WipWh.UpdatetOutputlotrecordNew(sapcode);
        //}
        [WebMethod]
        public byte[] QueryZ_WIP_TRACKING(string Colnum, string sDATA)
        {
            return mlc.GetDataSetSurrogateZipBytes(WipWh.QueryZ_WIP_TRACKING(Colnum, sDATA));
        }
        //[WebMethod]
        //public byte[] GetQtyByPartnumber(string partnumber, string storehouseid)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.QueryQtyByPartnumber(partnumber, storehouseid));
        //}

        //[WebMethod]
        //public byte[] GetSAP_DN(string Colnum, string DATA)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.GetSAP_DN(Colnum, DATA));
        //}
        //  [WebMethod]
        //public byte[] Getstoreinfo( string InputData)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.Getstoreinfo(InputData));
        //}
		
        //[WebMethod]
        //public string Updatez_whs_tracking_move_status(string palletnumber, string storehouseid)
        //{
        //    return WipWh.Updatez_whs_tracking_move_status(palletnumber, storehouseid);
        //}
        //[WebMethod]
        //public byte[] checkz_whs_trackingbyLocIDSHID( string storehouseid,string locid)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.checkz_whs_trackingbyLocIDSHID(storehouseid, locid));
        //}
        //[WebMethod]
        //public byte[] Getstoreinfobypallet(string Inputdate)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(WipWh.Getstoreinfobypallet(Inputdate));

        //}
        //[WebMethod]
        //public string Updatez_whs_tracking_move_store(string palletnumber, string storenumber, string storehouseid, string locid, string sstatus)
        //{
        //    return WipWh.Updatez_whs_tracking_move_store(palletnumber, storenumber, storehouseid, locid, sstatus);
        //}

        [WebMethod]
        public string Update_Z_WHS_TRACKING_STATUS(string StockNo, string woId, string Partnumber, string Status, string UserId)
        {
            return WipWh.Update_Z_WHS_TRACKING_STATUS(StockNo, woId, Partnumber, Status, UserId);
        }
        [WebMethod]
        public string Update_Z_WHS_TRACKING_STATUS_RWK(string sDATA, string sCMD, string Status, string UserId, string SfcLotOut)
        {
            return WipWh.Update_Z_WHS_TRACKING_STATUS_RWK(sDATA, sCMD, Status, UserId, SfcLotOut);
        }


        //#region PIANDIAN
        //[WebMethod]
        //public byte[] get_Z_WIP_TRACKING(string Colnum, string sDATA, int sFLAG)
        //{
        //    return mlc.GetDataSetZipBytes(WipWh.get_Z_WIP_TRACKING(Colnum, sDATA, sFLAG));
        //}
        //[WebMethod]
        //public string update_Z_WIP_TRACKING(string Colnum, string sDATA)
        //{
        //    return WipWh.update_Z_WIP_TRACKING(Colnum, sDATA);
        //}
        //[WebMethod]
        //public byte[] get_keypart(string esn)
        //{
        //    return mlc.GetDataSetZipBytes(WipWh.get_keypart(esn));
        //}
        //#endregion

         


        
    }

}
