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
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tRepairInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tRepairInfo : System.Web.Services.WebService
    {
        BLL.tRepairInfo mRepairinfo = new BLL.tRepairInfo();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] GetRepairSnInfo(string ESN)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetRepairSnInfo(ESN));
        }
        [WebMethod]
        public byte[] GetRepairMaterialInfo(string ESN)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetRepairMaterialInfo(ESN));
        }
        [WebMethod(Description = "修改需要维修产品的状态")]
        public void UpdateRepairSnStatus(string sStatus, string ROWID)
        {
            mRepairinfo.UpdateRepairSnStatus(sStatus, ROWID);
        }
        [WebMethod]
        public byte[] GetFailErrListCount(string ESN)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetFailErrListCount(ESN));
        }
        [WebMethod]
        public byte[] GetWipAndRouteData(string ESN)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetWipAndRouteData(ESN));
        }
        //[WebMethod]
        //public void UpdateRepairInformationInsertMaterial(string dciRepair,bool Flag)
        //{
        //    mRepairinfo.UpdateRepairInformation(dciRepair);
        //    if (Flag)
        //    {
        //        mRepairinfo.InsertRepairMaterialInfo(dciRepair);
        //    }
        //}
        [WebMethod]
        public string UpdateRepairInformation(string ListDic_Repair)
        {
            return mRepairinfo.UpdateRepairInformation(ListDic_Repair);
        }
        [WebMethod]
        public string UpdateRepairToWip(string CraftId, string ESN, string sStatus, string rowid)
        {
           return mRepairinfo.UpdateRepairToWip(CraftId, ESN, sStatus, rowid);
        }

        [WebMethod]
        public byte[] DowloadRepairInfo(string dicstring, string Flag)  //20130129
        {
            if (Flag == "IN")
            {
                return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetRepairInfoIN(dicstring));
            }
            else
            {
                return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetRepairInfoOUT(dicstring));
            }
        }
         [WebMethod(MessageName = "QueryRepairStatus")]
        public byte[] QueryRepairStatus(string status)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.QueryRepairStatus(status));
        }
        [WebMethod(MessageName = "QueryRepairStatusByDate")]
        public byte[] QueryRepairStatus(string StartDate, string EndDate, string Status)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.QueryRepairStatus(StartDate, EndDate, Status));
        }

        [WebMethod]
        public byte[] GetRepairReport(string StartDate, string EndDate,string Class)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetRepairReport(StartDate, EndDate,Class));
        }
        [WebMethod]
        public byte[] GetDutyInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mRepairinfo.GetDutyInfo());
        }
        [WebMethod]
        public string InsertDutyInfo(string Duty, string DutyDesc)
        {
            return mRepairinfo.InsertDutyInfo(Duty,DutyDesc);
        }
        [WebMethod]
        public string DeleteDutyInfo(string Duty)
        {
            return mRepairinfo.DeleteDutyInfo(Duty);
        }
        [WebMethod]
        public string InsertRepairMaterialInfo(string dicListReMaterial)
        {
            return mRepairinfo.InsertRepairMaterialInfo(dicListReMaterial);
        }
     
    }
}
