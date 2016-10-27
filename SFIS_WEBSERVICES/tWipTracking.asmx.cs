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
using System.Collections.Generic;
using GenericUtil;


namespace TestWeserver
{
    /// <summary>
    /// tWipTracking 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWipTracking : System.Web.Services.WebService
    {
        BLL.tWipTracking mWiptracking = new BLL.tWipTracking();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();       
       
        [WebMethod]
        public byte[] GetQueryWipAllInfo(string ColumnName, string Data)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.Get_WIP_TRACKING(ColumnName, Data));
        }
      
        [WebMethod(MessageName = "Get_WIP_TRACKING")]
        public byte[] Get_WIP_TRACKING(string ColumnName, string Data, string Fields)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.Get_WIP_TRACKING(ColumnName, Data, Fields));
        }
        [WebMethod(MessageName = "GetWipTracking")]
        public byte[] GetWipTracking(string Json,string Fields)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetWipTracking(mst,Fields));
        }
     
        [WebMethod]
        public byte[] GetWipTrackingList(List<string> Model, List<string> WoId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetWipTrackingList(Model, WoId));
        }
        [WebMethod]
        public byte[] GetWipTrackingLineList(List<string> Model, List<string> WoId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetWipTrackingLineList(Model, WoId));
        }
        [WebMethod]
        public byte[] GetPartNumberAndwoIdList()
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetPartNumberAndwoIdList());
        }
        [WebMethod]
        public void UpdateWipCartonTrayPallet(int PackType, string Data, string Data2, string ESN)
        {
            mWiptracking.UpdateWipCartonTrayPallet(PackType, Data, Data2, ESN);
        }
     
        [WebMethod]
        public string UpdateScrapWipTracking(string dicstring)
        {
            return mWiptracking.UpdateScrapWipTracking(dicstring);
        }       

        //[WebMethod(Description = "过站程序")]
        //public string UpdateWipStation(string line, string mygroup, string esn, string userid, string flag)
        //{
        //    return mWiptracking.UpdateWipStation(line, mygroup, esn, userid, flag);
        //}
        //[WebMethod(Description = "过站程序_new")]
        //public string UpdateWipStation_NEW(string line, string mygroup, string esn, string userid, string flag, string routeId, string woid)
        //{
        //    return mWiptracking.UpdateWipStation_New(line, mygroup, esn, userid, flag, routeId, woid);
        //}
        [WebMethod(Description = "根据esn获取数据,输入的可以是除密码外类型任何序号类型")]
        public byte[] GetEsnDataInfo(string sntype, string snval)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetEsnDataInfo(sntype, snval));
        }
        //[WebMethod(Description = "记录产能")]
        //public string StnRec(string lineId, string mygroup, string woId, string esn, int flag)
        //{
        //    return mWiptracking.StnRec(lineId, mygroup, woId, esn, flag);
        //}
        //[WebMethod(Description = "获取该工单或工单+线体的最大卡通箱号的内容")]
        //public byte[] GetMaxBoxNumberBywoId(string woId, string lineId)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetMaxBoxNumberBywoId(woId, lineId));
        //}

        ///// <summary>
        ///// 增加卡通箱记录
        ///// </summary>
        ///// <param name="cartioninfo">卡通箱信息</param>
        ///// <returns></returns>
        //[WebMethod(Description = "添加卡通箱信息,执行成功返回OK,否则返回错误信息")]
        //public string InsertCartonInfo(Entity.tCartonInfo cartioninfo)
        //{
        //    string _StrErr = string.Empty;
        //     mPro.PRO_INSERTCARTONINFO(cartioninfo.cartonId, cartioninfo.esn, cartioninfo.lineId, cartioninfo.woId, cartioninfo.mcartonnumber, cartioninfo.sn, cartioninfo.mac, cartioninfo.computer, out _StrErr);

        //     return _StrErr;
        //    //mWiptracking.InsertCartonInfo(cartioninfo);
        //}

        /// <summary>
        ///  产品过站和记录卡通箱
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <param name="line">产线编号</param>
        /// <param name="mygroup">当前流程</param>
        /// <param name="esn">产品跟踪序列号</param>
        /// <param name="userid">当前用户</param>
        /// <param name="flag">状态</param>
        /// <returns></returns>
        [WebMethod(Description = "添加卡通箱信息,执行过站功能,成功返回OK,否则返回错误信息")]
        public string UpdateWipAndRecCartonBox(string dicwiptrack)//(string cartonId, string mcartionId, string palletnumber, string mpalletnumber, string trayno, string line, string mygroup, string esn, string userid, string flag)
        {
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicwiptrack);
            //return mPro.PRO_UPDATEWIPANDRECCARTONBOX(wiptrack.line, wiptrack.locstation, wiptrack.ESN, wiptrack.userId, wiptrack.errflag, wiptrack.cartonnumber,
            //    wiptrack.mcartonnumbr, wiptrack.palletnumber, wiptrack.mpalletnumber, wiptrack.TrayNO);  
           // return mWiptracking.UpdateWipAndRecCartonBox(wiptrack);//(cartonId, mcartionId, palletnumber, mpalletnumber, trayno, line, mygroup, esn, userid, flag);
            return mPro.PRO_UPDATEWIPANDRECCARTONBOX(dic["LINE"].ToString(), dic["LOCSTATION"].ToString(), dic["ESN"].ToString(), dic["USERID"].ToString(), dic["ERRFLAG"].ToString(), dic["CARTONNUMBER"].ToString(),
                dic["MCARTONNUMBER"].ToString(),
                dic.ContainsKey("PALLETNUMBER")? dic["PALLETNUMBER"].ToString():null,
                dic.ContainsKey("MPALLETNUMBER")?dic["MPALLETNUMBER"].ToString():null,
                dic.ContainsKey("TRAYNO")?dic["TRAYNO"].ToString():null); 
        }
        [WebMethod(Description = "添加卡通箱信息,执行过站功能,成功返回OK,否则返回错误信-20150121")]
        public string UPDATE_CARTON_BOX(string LINE, string MYGROUP, string ESN, string EMP, string CARTONID, string MCARTIONID)
        {
            #region  组存储过程参数
            //List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
            //Entity.ProcedureKey Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "LINE";
            //Pdk.Value = LINE;
            //LsPdk.Add(Pdk);

            //Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "MYGROUP";
            //Pdk.Value = MYGROUP;
            //LsPdk.Add(Pdk);

            //Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "SN";
            //Pdk.Value = ESN;
            //LsPdk.Add(Pdk);

            //Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "EMP";
            //Pdk.Value = EMP;
            //LsPdk.Add(Pdk);

            //Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "FLAG";
            //Pdk.Value = "0";
            //LsPdk.Add(Pdk);

            //Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "CARTONID";
            //Pdk.Value = CARTONID;
            //LsPdk.Add(Pdk);

            //Pdk = new Entity.ProcedureKey();
            //Pdk.Variable = "MCARTIONID";
            //Pdk.Value = MCARTIONID;
            //LsPdk.Add(Pdk);
            #endregion

            return mPro.PRO_UPDATE_CARTON_BOX(LINE,MYGROUP,ESN,EMP,"0",CARTONID,MCARTIONID);// .ExecuteProcedure("PRO_UPDATE_CARTON_BOX", LsPdk);
        }
        [WebMethod(Description = "查询卡通箱的状态:0：在包装; 1：关闭(包装完成); 2：分解 3：合并")]
        public string GetCartonState(string cartonId)
        {
            return mWiptracking.GetCartonState(cartonId);
        }
        /// <summary>
        /// 获取卡通箱需要打印的所有内容
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <param name="Content">需要打印的内容</param>
        /// <returns></returns>
        [WebMethod(Description = "获取卡通箱需要打印的所有内容")]
        public byte[] GetCartonPrintContent(string cartonId, string[] Content)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetCartonPrintContent(cartonId, Content));
        }
        [WebMethod(Description = "添加序列号绑定关系,包含有效性检查")]
        public string InsertWipKeyParts(string diclsWipKeyPart)
        {
            IList<IDictionary<string, object>> lsdic = MapListConverter.JsonToListDictionary(diclsWipKeyPart);
            return mWiptracking.InsertWipKeyParts(lsdic);
        }
        [WebMethod(Description = "添加序列号绑定关系,直接添加无检查功能")]
        public string InsertWipKeyPart(string dicWipKeyPart)
        {
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicWipKeyPart);
            return mWiptracking.InsertWipKeyPart(dic);
        }
        /// <summary>
        /// 获取这条线没有包完的卡通箱列表
        /// </summary>
        /// <param name="lineId">产线编号</param>
        /// <returns></returns>
        [WebMethod(Description = "获取这条线没有包完的卡通箱列表")]
        public byte[] GetNotCloseBoxInfo(string lineId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetNotCloseBoxInfo(lineId));
        }

        /// <summary>
        /// 获取工单的最大的卡通箱编号
        /// </summary>
        /// <param name="woId">工单号</param>
        /// <returns></returns>
        [WebMethod(Description = "获取工单的最大的卡通箱编号")]
        public string GetMaxBoxNumber(string woId, string lineId, string partnumber)
        {
            return mPro.PRO_GETMAXCARTONID(woId, lineId, partnumber);
                
                //mWiptracking.GetMaxBoxNumber(woId, lineId, partnumber);
        }
        [WebMethod(Description = "获取指定的序列号存在的内容")]
        public byte[] GetSnInfo(string serial)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetSnInfo(serial));
        }
        /// <summary>
        /// 获取一个工单在一条产线上的包装信息
        /// </summary>
        /// <param name="woId"></param>
        /// <param name="lineId"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取一个工单在一条产线上的包装信息")]
        public byte[] GetPackCarton(string woId, string lineId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetPackCarton(woId, lineId));
        }

        [WebMethod(Description = "获取整个工单的序列号对应关系")]
        public byte[] GetWoAllSerial(string woId, int strHoue)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetWoAllSerial(woId, strHoue));
        }

        [WebMethod(Description = "获取属于密码的标签类型")]
        public string[] GetPwdColumns(string sntype)
        {
            List<string> lsstr = new List<string>();
            System.Data.DataTable _dt = mWiptracking.GetPwdColumns(sntype).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    lsstr.Add(dr["pwdname"].ToString());
                }
            }
            return lsstr.ToArray();
        }
        /// <summary>
        /// 获取卡通箱的包装内容
        /// </summary>
        /// <param name="cartonId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetCartonContent(string cartonId)
        {
           return mlc.GetDataSetSurrogateZipBytes(mPro.PRO_GETCARTONCONTENT(cartonId));
          //  return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetCartonContent(cartonId));
        }
        //[WebMethod]
        //public byte[] CROSSTAB_WIP(string woId, string Part, int Flag)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWiptracking.CROSSTAB_WIP(woId, Part, Flag));
        //}

        [WebMethod]
        public string CloseCartonBox(string cartonId)
        {
            return mWiptracking.CloseCartonBox(cartonId);
        }
        //[WebMethod]
        //public byte[] GetCartonSnList(string CartonId, int Flag)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetCartonSnList(CartonId, Flag));
        //}
        //[WebMethod]
        //public byte[] GetSnTestMachineInfo(string esn)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetSnTestMachineInfo(esn));
        //}
        [WebMethod]
        public void UpdateFirstCarton(string newcartonn)
        {
            mWiptracking.UpdateFirstCarton(newcartonn);
        }
        [WebMethod]
        public string UpdateWipTrackingWeight(string esn, string weight)
        {
            return mWiptracking.UpdateWipTrackingWeight(esn, weight);
        }
        [WebMethod]
        public byte[] GetMinCartonByWoid(string woid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetMinCartonByWoid(woid));
        }
        [WebMethod(MessageName = "更新WIP信息 0踢出工单")]
        public string Update_Wip_Tracking(string dicstring)
        {
            return mWiptracking.Update_Wip_Tracking(dicstring);
        }
        [WebMethod(MessageName = "UPDATE_WIP_TRACKING")]
        public string Update_Wip_Tracking(string dicstring, List<string> ListFields)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            return mWiptracking.Update_WIP_TRACKING(mst, ListFields);
        }
        [WebMethod(MessageName = "更新QA编号")]
       public string Update_QA(string QAInfo, string UpType, string QAUnit)
       {
           return mWiptracking.Update_QA(QAInfo, UpType, QAUnit);
       }
        [WebMethod]
        public string RollBack_Station(string ESN, string Station)
        {
            return mWiptracking.RollBack_Station(ESN,Station);
        }
        [WebMethod]
        public void check_wip_tracking(string Dic_Wip, string lsdic_key)
        {
            mWiptracking.check_wip_tracking(Dic_Wip, lsdic_key);
        }
        [WebMethod]
        public string UpdateWipTrackingstatus(string esn, string userid, string lotout )
        {
            return mWiptracking.UpdateWipTrackingstatus(esn, userid, lotout);
        }
        [WebMethod]
        public byte[] GetStockInPrint(string StockIn)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWiptracking.GetStockInPrint(StockIn));
        }
    }
}
