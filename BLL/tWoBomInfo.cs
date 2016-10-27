using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using System.Data.Common;
using SrvComponent;
using SystemObject;

namespace BLL
{
    public partial class tWoBomInfo
    {
       
        public tWoBomInfo()
        {
            
        }
        /// <summary>
        /// 获取已经导入的工单用料信息
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public DataSet GetWoBomInfo(string woId)
        {         
            //string table = "SFCR.T_WO_BOM_INFO";
            //string fieldlist = "woId as 生产工单号,partnumber as 成品料号 ,kpnumber as 组件料号 ,kpdesc as 组件物料描述,qty as 数量,process as 制程段,bomver as BOM版本";
            //int count = 0;
            //IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            //IDictionary<string, object> mst = new Dictionary<string, object>();
            //mst.Add("WOID", woId);
            //return dp.GetData(table, fieldlist,  mst, out count);

            int count = 0;
            string table = "SFCR.T_ERP_WO_BOM_INFO A,MESDB.T_WO_INFO_ERP B";
            string fieldlist = "a.woId as 生产工单号,b.partnumber as 成品料号 ,a.kpnumber as 组件料号 ,a.kpdesc as 组件物料描述,a.qty as 数量,a.process as 制程段,b.bomver as BOM版本,A.loc AS 库位".ToUpper();
            string filter = "A.WOID=B.WOID AND A.WOID={0} ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }

     //   public  DataSet GetWoBomInfo(string woId) 

        /// <summary>
        /// 增加工单用料信息
        /// </summary>
        /// <param name="twbi"></param>
        /// <param name="Err"></param>
        public void InsertWoBomInfo(string dicstring, out string Err)
        {
            try
            {
                
                IDictionary<string,object> dic = MapListConverter.JsonToDictionary(dicstring);
                string table = "SFCR.T_WO_BOM_INFO";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);     
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", dic["WOID"]);
                mst.Add("PARTNUMBER", dic["PARTNUMBER"]);
                mst.Add("KPNUMBER", dic["KPNUMBER"]);
                dp.DeleteData(table, mst);            
                dp.AddData("SFCR.T_WO_BOM_INFO",dic);           
                Err = "OK";
            }
            catch (Exception ex)
            {
                Err = ex.Message;
            
            }
        }


        public string InsertWoBomInfoList(string diclstwbom)
        {
            try
            {
                IList<IDictionary<string, object>> lsdic = MapListConverter.JsonToListDictionary(diclstwbom);               
                string table = "SFCR.T_WO_BOM_INFO";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", lsdic[0]["WOID"]);
                mst.Add("BLOCKED", 0);              
                dp.DeleteData(table, mst);
                string err = string.Empty;                
                foreach (Dictionary<string, object> itemBom in lsdic)
                {
                    InsertWoBomInfo(MapListConverter.DictionaryToJson(itemBom), out err);
                }
                return err;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 新增工单上料打印料表信息
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="Err"></param>
        public string InsertWoBomPrintInfo(string LsDicstring)
        {
         
            string _err = string.Empty;
            IList<IDictionary<string, object>> lsdic = MapListConverter.JsonToListDictionary(LsDicstring);
            if (lsdic.Count > 0)
            {              
                string table = "SFCR.T_MATERIAL_PREPARATION";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("MASTERID", lsdic[0]["MASTERID"]);
                mst.Add("WOID", lsdic[0]["WOID"]);
                dp.DeleteData(table,mst);
               
                InsertWoBomPrintInfo(lsdic, out _err);                 
                return _err;
            }
            else
            {
                _err = "No Data";
                return _err;
                    
            }
            
        }

        public void   InsertWoBomPrintInfo(IList<IDictionary<string,object>> dic, out string Err)
        {           
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                dp.AddListData("SFCR.T_MATERIAL_PREPARATION", dic);

                Err = "OK";
            }
            catch (Exception ex)
            {
                Err = ex.Message;

            }
        }

        public System.Data.DataSet GetMaterialPreparation(string woId, string masterId)
        {           
            int count = 0;
            string table = "SFCR.T_MATERIAL_PREPARATION MP, SFCR.T_SMT_KP_MASTER KM";
            string fieldlist = "MP.WOID,MP.PARTNUMBER,MP.STATIONNO,MP.MASTERID,MP.KPNUMBER,MP.KPDESC,MP.REPLACEGROUP,MP.KPDISTINCT,MP.LOCALTION,MP.BOMVER,MP.SIDE,KM.LINEID AS MACHINEID,KM.MODELNAME,MP.FEEDERTYPE";
            string filter = "KM.MASTERID = MP.MASTERID AND MP.WOID ={0} AND MP.MASTERID = {1}";
      
            IList<OrderKey> order = new List<OrderKey>();
            OrderKey od1 = new OrderKey();
            od1.KeyName = "MP.STATIONNO";
            od1.IsAsc = true;           
            order.Add(od1);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            mst.Add("MASTERID", masterId);
            return TransactionManager.GetData(table, fieldlist, filter, mst, order, null, out count);

        }

        public System.Data.DataSet QueryWoBomInfoData(string WO)
        {        
            string table = "SFCR.T_WO_BOM_INFO";
            string fieldlist = "woId,userId,partnumber,kpnumber,kpdesc,qty,process,bomver,recdate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", WO);
            return dp.GetData(table, fieldlist,  mst, out count);
        }

        public void InserWoBomData(string dicstring)
        {            
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            mst.Add("RECDATE",System.DateTime.Now);
            dp.AddData("SFCR.T_WO_BOM_INFO",   mst);

        }
    }
}
